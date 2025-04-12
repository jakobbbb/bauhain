using UnityEngine;
using UnityEngine.UI;
using Yarn;



public class CharacterSelection : MonoBehaviour
{
    public GameObject[] CharacterImages;
    public GameObject[] ObjectsToDeactivate;
    public Animator animator;
    public int activeCharacter=0;
    private int acceptedCounter = 0;
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

    public void SetCharacterVariables(bool accept)
    {
        if (accept)
        {
            acceptedCounter++;
            if (acceptedCounter == 10)
            {
                //Activate Yarn Spinner UI Element and Start the Game Text
                Debug.Log("GameSTARRRRRRT");
                animator.SetTrigger("Start");
            }

            Destroy(CharacterImages[activeCharacter]);
            CharacterImages[activeCharacter] = null;
            if (activeCharacter == 0)
            {
                //Set Brayn Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 1)
            {
                //Set Barbara Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 2)
            {
                //Set Mia Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 3)
            {
                //Set Diana Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 4)
            {
                //Set kids Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 5)
            {
                //Set Elfie Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 6)
            {
                //Set Elim Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 7)
            {
                //Set Emma Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 8)
            {
                //Set Enoby Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 9)
            {
                //Set Fürchti Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 10)
            {
                //Set Gayle Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 11)
            {
                //Set God Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 12)
            {
                //Set Lizzie Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 13)
            {
                //Set Lux Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 14)
            {
                //Set Mattheo Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 15)
            {
                //Set Noah Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 16)
            {
                //Set Renata Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 17)
            {
                //Set Samira Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
            }
            if (activeCharacter == 18)
            {
                //Set Yves Value to 1
                if (acceptedCounter == 1)
                {
                    //Set Yarn Spinner DJ Variable to Name of current Character
                }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
