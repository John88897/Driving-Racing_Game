using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public float minPitch = 0.8f;
    public float maxPitch = 2f;
    public float maxSpeed = 30f;

    AudioSource engine;
    Rigidbody rb;

    void Start()
    {
        engine = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float t = Mathf.Clamp01(rb.linearVelocity.magnitude / maxSpeed);
        engine.pitch = Mathf.Lerp(minPitch, maxPitch, t);
        engine.volume = Mathf.Lerp(0.3f, 1f, t);
    }
}