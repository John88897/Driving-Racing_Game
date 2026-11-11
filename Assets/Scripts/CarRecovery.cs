using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarRecovery : MonoBehaviour
{
    public float maxTiltAngle = 50f;   // beyond this the car counts as flipped
    public float flipTime = 1.5f;      // seconds flipped before auto-reset
    public float liftHeight = 1.5f;    // lifts the car so it doesn't clip the ground

    Rigidbody rb;
    float flippedTimer;

    void Start() { rb = GetComponent<Rigidbody>(); }

    void Update()
    {
        // manual reset with R
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetUpright();
            return;
        }

        // auto reset when tilted too far for too long
        float tilt = Vector3.Angle(transform.up, Vector3.up);
        if (tilt > maxTiltAngle) flippedTimer += Time.deltaTime;
        else flippedTimer = 0f;

        if (flippedTimer >= flipTime) ResetUpright();
    }

    void ResetUpright()
    {
        flippedTimer = 0f;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // keep the direction the car was facing, but level it out
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        if (flatForward.sqrMagnitude < 0.01f) flatForward = Vector3.forward;

        rb.position += Vector3.up * liftHeight;
        rb.rotation = Quaternion.LookRotation(flatForward.normalized, Vector3.up);
    }
}