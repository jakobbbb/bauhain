using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private string m_NPCName;
    public Sprite Splash;
    public string NPCName() {
        return m_NPCName;
    }
    public void SetNPCName(string n) {
        m_NPCName = n;
    }
}
