using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public List<Room> Rooms = new List<Room>();

    private float m_GameTimeMinutes = 0;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    void Start() {
        var rs = GameObject.FindObjectsByType(typeof(Room), FindObjectsSortMode.None);
        foreach (var el in rs) {
            Rooms.Add(null);
        }
        foreach (var el in rs) {
            Rooms[((Room)el).RoomId] = (Room)el;
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
