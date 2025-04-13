using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private string m_NPCName;
    public string NPCName() {
        return m_NPCName;
    }
    public void SetNPCName(string n) {
        m_NPCName = n;
    }
}
