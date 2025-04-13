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

    private GameObject m_KSAnimTrans = null;

    public InMemoryVariableStorage Storage() {
        if (m_Vars == null) {
            m_Vars = GetComponent<InMemoryVariableStorage>();
        }
        return m_Vars;
    }

    void Start() {
        DialogueCanvas.enabled = false;
        m_Runner.CommandNeedsHandling.AddListener(DialogueIsOver);
        DontDestroyOnLoad(this);
    }

    // AAAAAAAAA
    public void Start_SampleScene() {
        m_KSAnimTrans = GameObject.Find("KS Animator Transition");
        m_KSAnimTrans.SetActive(false);
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

    public void TalkTo(string character_name, Sprite sprite, string prefix = "Intro") {
        if (character_name == "kids") {
            character_name = "Kids";  // aAAAAAAAAAAAAaaaaaaaaaaaAAAaaAAAAAAAaAAAAa
        }
        DialogueCanvas.enabled = true;
        CharacterSplash.sprite = sprite;
        var im = CharacterSplash.GetComponent<Image>();
        im.enabled = (sprite != null);

        Debug.Log("enabled? " + DialogueCanvas.enabled);
        m_Runner.StopDialogue();
        m_Runner.StartDialogue(prefix + character_name);
    }

    public static void DialogueIsOver(string[] cmd) {
        BauHainDialogueManager b = GameManager.Instance.DiaManager;
        if (cmd[0] == "dialogue_is_over") {
            b.DialogueCanvas.enabled = false;
            //b.m_Runner.StopDialogue();
            //b.m_Runner.StartDialogue("EventLoop");
        } else if (cmd[0] == "trigger_ks") {
            string ks_name = cmd[1];
            b.m_KSAnimTrans.SetActive(true);
            var ks = GameObject.Find(ks_name);
            ks.GetComponent<Image>().enabled = true;
        } else if (cmd[0] == "trigger_as") {
            var assman = GameObject.FindFirstObjectByType<ActionScreenManager>();
            assman.ShowScreen(cmd[1]);
        }
    }
    public bool IsDialogueRunning() {
        return DialogueCanvas.enabled;
    }
}
