using UnityEngine;
using Yarn.Unity;

public class BauHainDialogueManager : MonoBehaviour {
    [SerializeField]
    private MinimalDialogueRunner m_Runner;

    private InMemoryVariableStorage m_Vars = null;

    public Canvas DialogueCanvas;

    public InMemoryVariableStorage Storage() {
        if (m_Vars == null) {
            m_Vars = GetComponent<InMemoryVariableStorage>();
        }
        return m_Vars;
    }

    void Start() {
        DialogueCanvas.enabled = false;
    }

    void Update() {
        float clock = -1;
        Storage().TryGetValue<float>("$clock", out clock);
        if (clock >= 10) {
            DialogueCanvas.enabled = true;
            m_Runner.StopDialogue();
            m_Runner.StartDialogue("TheEnd");
        }
    }

    public void TalkTo(string character_name) {
        DialogueCanvas.enabled = true;
        Debug.Log("enabled? " + DialogueCanvas.enabled);
        m_Runner.StopDialogue();
        m_Runner.StartDialogue("Intro" + character_name);
    }
}
