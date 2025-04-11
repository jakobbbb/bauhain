using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    void Start() {
        if (Instance) {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }
}
