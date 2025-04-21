using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeIsRunningAAAAAAAA : MonoBehaviour {
    private List<GameObject> m_Elements = new List<GameObject>();

    void Start() {
        var c = transform.GetChild(0);
        var n = c.childCount;
        for (int i = 0; i < n; ++i) {
            m_Elements.Add(c.GetChild(i).gameObject);
        }
        StartCoroutine(UpdateTime());
        UpdateClock();
    }

    private void UpdateClock() {
        var stor = GameManager.Instance.DiaManager.Storage();
        float clock = -1;
        stor.TryGetValue("$clock", out clock);
        if (clock >= 0) {
            for (int i = 0; i < m_Elements.Count; ++i) {
                m_Elements[i].SetActive(i == (int)clock);
            }
        }
    }

    private IEnumerator UpdateTime() {
        while (true) {
            yield return new WaitForSeconds(2.5f);
            UpdateClock();
        }
    }
}
