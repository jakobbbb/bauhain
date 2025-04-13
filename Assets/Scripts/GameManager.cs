using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public List<Room> Rooms = new List<Room>();

    private float m_GameTimeMinutes = 0;

    public Transform DJSpot;

    public List<NPCController> NPCPrefabs = new List<NPCController>();
    public BauHainDialogueManager DiaManager;
    public PulsateLights Lights;
    public AudioSource PA;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    void Start() {
    }

    public void Start_SampleScene() {
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
            if (present < 0.9f) {
                ((NPCController)npc).transform.position = 10000.0f * new Vector3(1, 1, 1);
                Destroy(npc);
            } else if (dj == npc.name) {
                ((NPCController)npc).MakeDJ();
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
