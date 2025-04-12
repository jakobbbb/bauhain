using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1f;
    //public Transform rotateAround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
