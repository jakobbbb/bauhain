using UnityEngine;

public class BauHainDialogueManager : MonoBehaviour {
    [SerializeField]
    private MinimalDialogueRunner m_Runner;

    void Start() {
    }

    void Update() {
        
    }

    public void TalkTo(string character_name) {
        // TODO Stop event loop
        m_Runner.StartDialogue("Intro" + character_name);
        // TODO Restart event loop after dialogue finished
    }
}
