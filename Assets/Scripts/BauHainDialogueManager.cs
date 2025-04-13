using UnityEngine;
using Yarn.Unity;

public class BauHainDialogueManager : MonoBehaviour {
    [SerializeField]
    private MinimalDialogueRunner m_Runner;

    private InMemoryVariableStorage m_Vars = null;

    public InMemoryVariableStorage Storage {
        get => m_Vars;
    }

    void Start() {
    }

    void Update() {
        if (m_Vars == null) {
            m_Vars = GetComponent<InMemoryVariableStorage>();
        }

        float clock = -1;
        m_Vars.TryGetValue<float>("$clock", out clock);
        if (clock >= 10) {
            m_Runner.StopDialogue();
            m_Runner.StartDialogue("TheEnd");
        }
    }

    public void TalkTo(string character_name) {
        m_Runner.StopDialogue();
        m_Runner.StartDialogue("Intro" + character_name);
    }

    public void OnDialogueFinished() {
        m_Runner.StartDialogue("EventLoop");
    }
}
