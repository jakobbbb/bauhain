using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class BauHainDialogueManager : MonoBehaviour {
    [SerializeField]
    private DialogueRunner m_Runner;

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
            //m_Runner.StopDialogue();
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
        //m_Runner.StopDialogue();
        m_Runner.StartDialogue(prefix + character_name);
    }

    [YarnCommand("trigger_ks")]
    public static void TriggerKS(string ks_name) {
        BauHainDialogueManager b = GameManager.Instance.DiaManager;
        Debug.Log("triggering ks " + ks_name);
        b.m_KSAnimTrans.SetActive(true);
        var ks = GameObject.Find(ks_name);
        ks.GetComponent<Image>().enabled = true;
    }

    [YarnCommand("trigger_as")]
    public static void TriggerAS(string as_name) {
        var assman = GameObject.FindFirstObjectByType<ActionScreenManager>();
        assman.ShowScreen(as_name);
    }

    [YarnCommand("hide_canvas")]
    public static void HideCanvas() {
        BauHainDialogueManager b = GameManager.Instance.DiaManager;
        b.DialogueCanvas.enabled = false;
    }

    public bool IsDialogueRunning() {
        bool in_event_loop = false;
        Storage().TryGetValue("$in_event_loop", out in_event_loop);
        Debug.Log("in ev loop?" + in_event_loop);
        return m_Runner.IsDialogueRunning && !in_event_loop;
    }
}
