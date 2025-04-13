using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class BauHainDialogueManager : MonoBehaviour {
    [SerializeField]
    private MinimalDialogueRunner m_Runner;

    private InMemoryVariableStorage m_Vars = null;

    public Canvas DialogueCanvas;
    public Image CharacterSplash;

    private bool m_TheEnd = false;

    public InMemoryVariableStorage Storage() {
        if (m_Vars == null) {
            m_Vars = GetComponent<InMemoryVariableStorage>();
        }
        return m_Vars;
    }

    void Start() {
        DialogueCanvas.enabled = false;
        m_Runner.CommandNeedsHandling.AddListener(DialogueIsOver);
    }

    void Update() {
        float clock = -1;
        Storage().TryGetValue<float>("$clock", out clock);
        if (!m_TheEnd && clock >= 10) {
            DialogueCanvas.enabled = true;
            m_Runner.StopDialogue();
            m_Runner.StartDialogue("TheEnd");
            m_TheEnd = true;
        }
    }

    public void TalkTo(string character_name, Sprite sprite) {
        DialogueCanvas.enabled = true;
        CharacterSplash.sprite = sprite;
        Debug.Log("enabled? " + DialogueCanvas.enabled);
        m_Runner.StopDialogue();
        m_Runner.StartDialogue("Intro" + character_name);
    }

    public static void DialogueIsOver(string[] cmd) {
        if (cmd[0] == "dialogue_is_over") {
            BauHainDialogueManager b = GameManager.Instance.DiaManager;
            b.DialogueCanvas.enabled = false;
            //b.m_Runner.StopDialogue();
            //b.m_Runner.StartDialogue("EventLoop");
        } else {
            Debug.Log("Got command " + cmd[0]);
        }
    }
}
