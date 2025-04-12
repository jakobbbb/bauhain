using UnityEngine;
using UnityEngine.UI;



public class CharacterSelection : MonoBehaviour
{
    public GameObject[] CharacterImages;
    public Animator animator;
    public int activeCharacter=0;
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

    public void SwitchCharacter()
    {
        CharacterImages[activeCharacter].SetActive(false);
        activeCharacter++;
        CharacterImages[activeCharacter].SetActive(true);
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
