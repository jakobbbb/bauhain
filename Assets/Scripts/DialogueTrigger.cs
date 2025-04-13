using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private string m_NPCName;
    public Sprite Splash;
    public SpriteRenderer InteractionIcon;
    public string NPCName() {
        return m_NPCName;
    }
    public void SetNPCName(string n) {
        m_NPCName = n;
    }

    public void EnableInteractionIcon() {
        if (InteractionIcon.enabled) {
            return;
        }
        foreach (var el in GameObject.FindObjectsOfType<DialogueTrigger>()) {
            Debug.Log(el);
            el.InteractionIcon.enabled = (this == el);
        }
    }
    public static void DisableAllInteractionIcons() {
        foreach (var el in GameObject.FindObjectsOfType<DialogueTrigger>()) {
            el.InteractionIcon.enabled = false;
        }
    }
}
