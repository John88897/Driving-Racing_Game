using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Movement")]
    public float acceleration = 9f;
    public float maxSpeed = 22f;
    public float turnSpeed = 80f;
    public float reverseSpeed = 8f;

    [Header("Weight / Stability")]
    public float extraGravity = 25f;       // pulls the car down so it doesn't float
    public float sidewaysGrip = 0.9f;      // 1 = no grip, lower = more grip

    [Header("Countdown Settings")]
    public float countdownDelay = 3f;
    public bool canControl = false;

    private Rigidbody rb;
    private float moveInput;
    private float steerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.8f, 0);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        StartCoroutine(EnableControlsAfterDelay());
    }

    IEnumerator EnableControlsAfterDelay()
    {
        canControl = false;
        yield return new WaitForSeconds(countdownDelay);
        canControl = true;
    }

    void Update()
    {
        moveInput = 0f;
        steerInput = 0f;

        if (!canControl) return;

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveInput += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveInput -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) steerInput += 1f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) steerInput -= 1f;
    }

    void FixedUpdate()
    {
        // extra gravity always applies, even during the countdown
        rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);

        if (!canControl) return;

        // flatten the forward direction so hills don't push the car upward
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        // horizontal speed only (ignores falling speed)
        Vector3 vel = rb.linearVelocity;
        float horizontalSpeed = new Vector3(vel.x, 0f, vel.z).magnitude;

        if (moveInput > 0 && horizontalSpeed < maxSpeed)
        {
            rb.AddForce(flatForward * moveInput * acceleration, ForceMode.Acceleration);
        }
        else if (moveInput < 0 && horizontalSpeed < reverseSpeed)
        {
            rb.AddForce(flatForward * moveInput * acceleration * 0.6f, ForceMode.Acceleration);
        }

        // steering
        if (horizontalSpeed > 0.5f)
        {
            float direction = moveInput >= 0 ? 1f : -1f;
            float turn = steerInput * turnSpeed * direction * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }

        // reduce sideways sliding
        Vector3 local = transform.InverseTransformDirection(rb.linearVelocity);
        local.x *= sidewaysGrip;
        rb.linearVelocity = transform.TransformDirection(local);
    }
}