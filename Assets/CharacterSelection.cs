using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] CharacterImages;
    public GameObject[] ObjectsToDeactivate;
    public Animator animator;
    public int activeCharacter=0;
    private int acceptedCounter = 0;
    private bool haveDJ = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < CharacterImages.Length; ++i)
        {
            CharacterImages[i].SetActive(false);
        }
        CharacterImages[activeCharacter].SetActive(true);
    }

    public void AcceptCharacter()
    {
        animator.SetTrigger("Accept");        
    }

    private void SetDJ(string name) {
        if (!haveDJ) {
            var s = GameManager.Instance.DiaManager.Storage();
            haveDJ = true;
            s.SetValue("$DJ", name);
            Debug.Log("DJ is " + name);
        }
    }

    private IEnumerator WaitForEntryOpeningStartingBeginningToFinish() {
        var dm = GameManager.Instance.DiaManager;  // TODO TheBeginning
        while (dm.IsDialogueRunning()) {
            yield return new WaitForSeconds(0.5f);
        }
        animator.SetTrigger("Enter"); // calls StartGame();
    }

    //Start Yarn Spinner Script and Start Game in this function
    public void SetCharacterVariables(bool accept)
    {
        var s = GameManager.Instance.DiaManager.Storage();
        if (accept)
        {
            acceptedCounter++;
            if (acceptedCounter == 10)
            {
                //Activate Yarn Spinner UI Element and Start the Game Text
                Debug.Log("GameSTARRRRRRT");
                animator.SetTrigger("Start");
                var dm = GameManager.Instance.DiaManager;  // TODO TheBeginning
                dm.TalkTo("Beginning", null, "The");
                StartCoroutine(WaitForEntryOpeningStartingBeginningToFinish());
                //Set this animation Trigger when Yarn Spinner is finished with intro and the game should start -> Reveals Bauhain Logo
            }

            Destroy(CharacterImages[activeCharacter]);
            CharacterImages[activeCharacter] = null;
            if (activeCharacter == 0)
            {
                //Set Brayn Value to 1
                s.SetValue("$Brayn", 1);
                SetDJ("Brayn");
            }
            if (activeCharacter == 1)
            {
                //Set Barbara Value to 1
                s.SetValue("$Barbara", 1);
                SetDJ("Barbara");
            }
            if (activeCharacter == 2)
            {
                //Set Mia Value to 1
                s.SetValue("$Mia", 1);
                SetDJ("Mia");
            }
            if (activeCharacter == 3)
            {
                //Set Diana Value to 1
                s.SetValue("$Diana", 1);
                SetDJ("Diana");
            }
            if (activeCharacter == 4)
            {
                //Set kids Value to 1
                s.SetValue("$kids", 1);
            }
            if (activeCharacter == 5)
            {
                //Set Elfie Value to 1
                s.SetValue("$Elfie", 1);
                SetDJ("Elfie");
            }
            if (activeCharacter == 6)
            {
                //Set Elim Value to 1
                s.SetValue("$Elim", 1);
            }
            if (activeCharacter == 7)
            {
                //Set Emma Value to 1
                s.SetValue("$Emma", 1);
                SetDJ("Emma");
            }
            if (activeCharacter == 8)
            {
                //Set Enoby Value to 1
                s.SetValue("$Enoby", 1);
                SetDJ("Enoby");
            }
            if (activeCharacter == 9)
            {
                //Set Fürchti Value to 1
                s.SetValue("$Fürchti", 1);
                SetDJ("Fürchti");
            }
            if (activeCharacter == 10)
            {
                //Set Gayle Value to 1
                s.SetValue("$Gayle", 1);
                SetDJ("Gayle");
            }
            if (activeCharacter == 11)
            {
                //Set God Value to 1
                s.SetValue("$God", 1);
            }
            if (activeCharacter == 12)
            {
                //Set Lizzie Value to 1
                s.SetValue("$Lizzie", 1);
                SetDJ("Lizzie");
            }
            if (activeCharacter == 13)
            {
                //Set Lux Value to 1
                s.SetValue("$Lux", 1);
                SetDJ("Lux");
            }
            if (activeCharacter == 14)
            {
                //Set Mattheo Value to 1
                s.SetValue("$Mattheo", 1);
                SetDJ("Mattheo");
            }
            if (activeCharacter == 15)
            {
                //Set Noah Value to 1
                s.SetValue("$Noah", 1);
                SetDJ("Noah");
            }
            if (activeCharacter == 16)
            {
                //Set Renata Value to 1
                s.SetValue("$Renata", 1);
                SetDJ("Renata");
            }
            if (activeCharacter == 17)
            {
                //Set Samira Value to 1
                s.SetValue("$Samira", 1);
                SetDJ("Samira");
            }
            if (activeCharacter == 18)
            {
                //Set Yves Value to 1
                s.SetValue("$Yves", 1);
                SetDJ("Yves");
            }
            if (activeCharacter == 19)
            {
                //Set Maggie Value to 1
                s.SetValue("$Maggie", 1);
                SetDJ("Maggie");
            }
        }
        }

    public void SwitchCharacterAccept()
    {
        SetCharacterVariables(true);
        if (CharacterImages[activeCharacter] != null)
        {
            CharacterImages[activeCharacter].SetActive(false);
        }
        activeCharacter++;
        if (activeCharacter == CharacterImages.Length)
        {
            activeCharacter = 0;
        }
        if (CharacterImages[activeCharacter] != null)
        {
            CharacterImages[activeCharacter].SetActive(true);
        }
        else { SwitchCharacterAccept(); }
    }
    public void SwitchCharacter()
    {
        if (CharacterImages[activeCharacter] != null)
        {
            CharacterImages[activeCharacter].SetActive(false);
        }
        activeCharacter++;
        //printf("activeCharacterCount", activeCharacter);
        if (activeCharacter == CharacterImages.Length)
        {
            activeCharacter = 0;
        }
        if (CharacterImages[activeCharacter] != null)
        {
            CharacterImages[activeCharacter].SetActive(true);
        }
        else { SwitchCharacter(); }

    }

    public void RejectCharacter()
    {        
        animator.SetTrigger("Reject");
    }

    public static void Loaded(Scene scene, LoadSceneMode mode) {
        GameManager.Instance.Start_SampleScene();
        GameManager.Instance.DiaManager.Start_SampleScene();
    }

    public void StartGame()
    {        
        SceneManager.sceneLoaded += Loaded;
        SceneManager.LoadScene("SampleScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
