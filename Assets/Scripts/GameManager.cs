using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public List<Room> Rooms = new List<Room>();

    private float m_GameTimeMinutes = 0;

    public Transform DJSpot;

    public bool m_DebugSkipSelection = false;

    public List<NPCController> NPCPrefabs = new List<NPCController>();
    public BauHainDialogueManager DiaManager;
    public PulsateLights Lights;
    public AudioSource PA;

    private SortedDictionary<string, NPCController> m_NPCObjects =
            new SortedDictionary<string, NPCController>();

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    void Start() {
#if !UNITY_EDITOR
        // never skip in builds
        m_DebugSkipSelection = false;
#endif

        if (m_DebugSkipSelection) {
            StartCoroutine(DebugSkipCharacterSelectionCoroutine());
        }
    }

    private IEnumerator DebugSkipCharacterSelectionCoroutine() {
        yield return new WaitForSeconds(0.3f);
        var sel = GameObject.FindAnyObjectByType<CharacterSelection>();
        for (int i = 0; i < 10; ++i) {
            sel.SwitchCharacterAccept();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Start_SampleScene() {
        StopAllCoroutines();
        Debug.Log("Hello??");
        var rs = GameObject.FindObjectsByType(typeof(Room), FindObjectsSortMode.None);
        foreach (var el in rs) {
            Rooms.Add(null);
        }
        foreach (var el in rs) {
            Rooms[((Room)el).RoomId] = (Room)el;
        }
        Lights = FindFirstObjectByType<PulsateLights>();
        DJSpot = GameObject.Find("DJSpot").transform;

        var npcs = GameObject.FindObjectsByType(typeof(NPCController), FindObjectsSortMode.None);
        foreach (var npc in npcs) {
            float present = -1.0f;
            DiaManager.Storage().TryGetValue("$" + npc.name, out present);
            string dj = "";
            DiaManager.Storage().TryGetValue("$DJ", out dj);
            if (present > 0.0f) {
                if (dj == npc.name) {
                    ((NPCController)npc).MakeDJ();
                }
                m_NPCObjects.Add("$" + npc.name, (NPCController)npc);
            } else {
                ((NPCController)npc).transform.position = 10000.0f * new Vector3(1, 1, 1);
                Destroy(((NPCController)npc).gameObject);
            }
        }
        StartCoroutine(CheckForRemovedCharactersCoroutine());
    }

    private IEnumerator CheckForRemovedCharactersCoroutine() {
        while (true) {
            yield return new WaitForSeconds(1.0f);
            foreach (var item in m_NPCObjects) {
                float present = 0.0f;
                DiaManager.Storage().TryGetValue(item.Key, out present);
                if (present < 1.0f && item.Value != null) {
                    Destroy(item.Value.gameObject);
                }
            }
        }
    }

    public void AdvanceTime(float minutes) {
        m_GameTimeMinutes += minutes;
        m_GameTimeMinutes = m_GameTimeMinutes % (60 * 24);
    }


    public string GetFormattedTime() {
        int h = (int)m_GameTimeMinutes / 60;
        int m = (int)m_GameTimeMinutes % 60;
        return h.ToString("D2") + ":" + m.ToString("D2");
    }
}
