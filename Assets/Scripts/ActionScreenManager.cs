using UnityEngine;
using System.Collections.Generic;

public class ActionScreenManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_ActionScreenParent = null;

    private List<GameObject> m_Screens = new List<GameObject>();

    void Start() {
        for (int i = 0; i < m_ActionScreenParent.transform.childCount; ++i) {
            var child = m_ActionScreenParent.transform.GetChild(i);
            m_Screens.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        m_ActionScreenParent.SetActive(false);
    }

    // Update is called once per frame
    void ShowScreen(string screen_name) {
        foreach (var screen in m_Screens) {
            if (screen.name == screen_name) {
                screen.SetActive(true);
                break;
            }
        }
        m_ActionScreenParent.SetActive(true);
    }

    void HideScreen() {
        foreach (var screen in m_Screens) {
            screen.SetActive(false);
        }
        m_ActionScreenParent.SetActive(false);
    }
}
