using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarStabilizer : MonoBehaviour
{
    public float maxSpeed = 25f;        // metres per second (25 is about 90 km/h)
    public float downforce = 50f;       // pushes the car down as it gets faster
    public Vector3 centerOfMass = new Vector3(0f, -0.6f, 0f);

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;   // low centre = harder to flip
    }

    void FixedUpdate()
    {
        // cap the speed
        Vector3 v = rb.linearVelocity;
        if (v.magnitude > maxSpeed)
            rb.linearVelocity = v.normalized * maxSpeed;

        // press the car into the ground
        rb.AddForce(-transform.up * downforce * rb.linearVelocity.magnitude);
    }
}