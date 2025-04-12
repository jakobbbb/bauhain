using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class PulsateLights : MonoBehaviour {

    private List<Light2D> m_Lights = new List<Light2D>();
    private List<float> m_DefaultIntensities = new List<float>();

    [SerializeField]
    private AnimationCurve m_Curve;

    void Start() {
        foreach (var light in GetComponentsInChildren<Light2D>()) {
            m_Lights.Add(light);
            m_DefaultIntensities.Add(light.intensity);
        }
        Debug.Log("Got " + m_Lights.Count + " lights.");
    }

    // Update is called once per frame
    void Update() {
        float speed = 2.4f;
        float t = (Time.time * speed) - Mathf.Floor(Time.time * speed);
        float intensity = m_Curve.Evaluate(t);

        int i = 0;
        foreach (var light in m_Lights) {
            light.intensity = m_DefaultIntensities[i++] * intensity;
        }
    }
}
