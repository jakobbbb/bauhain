using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public int a = 1;
    public GameObject ObjectToActivate;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    

public void Activate()
{
    ObjectToActivate.SetActive(true);
}
public void Deactivate()
{
    ObjectToActivate.SetActive(false);
}

// Update is called once per frame
void Update()
    {
        
    }
}
