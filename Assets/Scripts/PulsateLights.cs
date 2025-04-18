using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class PulsateLights : MonoBehaviour {

    public float BPM;

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

    void Update() {
        float speed = BPM / 60.0f;
        float t = (Time.time * speed) - Mathf.Floor(Time.time * speed);
        float intensity = m_Curve.Evaluate(t);

        int i = 0;
        foreach (var light in m_Lights) {
            light.intensity = m_DefaultIntensities[i++] * intensity;
        }
    }
}
