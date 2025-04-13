using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    private string m_NPCName;
    public Sprite Splash;
    public SpriteRenderer InteractionIcon;

    private bool talked_to = false;

    public void MarkVisited() {
        talked_to = true;
    }

    public string NPCName() {
        if (talked_to) {
            return null;
        }
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
            el.InteractionIcon.enabled = (!talked_to && this == el);
        }
    }
    public static void DisableAllInteractionIcons() {
        foreach (var el in GameObject.FindObjectsOfType<DialogueTrigger>()) {
            el.InteractionIcon.enabled = false;
        }
    }
}
