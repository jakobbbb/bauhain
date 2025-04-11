using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private float m_GameTimeMinutes = 0;

    void Start() {
        if (Instance) {
            DestroyImmediate(Instance);
        }
        Instance = this;
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
