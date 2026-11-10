using UnityEngine;
using UnityEngine.InputSystem; // Uses the New Input System package

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Movement")]
    public float acceleration = 25f;
    public float maxSpeed = 35f;
    public float turnSpeed = 90f;
    public float reverseSpeed = 12f;

    private Rigidbody rb;
    private float moveInput;
    private float steerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Forward / Reverse (W/S or Up/Down arrows)
        moveInput = 0f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveInput += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveInput -= 1f;

        // Steering (A/D or Left/Right arrows)
        steerInput = 0f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) steerInput += 1f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) steerInput -= 1f;
    }

    void FixedUpdate()
    {
        if (moveInput > 0)
        {
            if (rb.linearVelocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
            }
        }
        else if (moveInput < 0)
        {
            if (rb.linearVelocity.magnitude < reverseSpeed)
            {
                rb.AddForce(transform.forward * moveInput * acceleration * 0.6f, ForceMode.Acceleration);
            }
        }

        if (rb.linearVelocity.magnitude > 0.5f)
        {
            float direction = moveInput >= 0 ? 1f : -1f;
            float turn = steerInput * turnSpeed * direction * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}