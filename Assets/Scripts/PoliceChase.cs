using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoliceChase : MonoBehaviour
{
    public Transform target;          // drag N_Muscle Car_10 here
    public float speed = 12f;
    public float acceleration = 8f;
    public float turnSpeed = 2f;
    public float startDelay = 3f;
    public float extraGravity = 30f;
    public float stuckTime = 2f;      // seconds of no movement before unsticking

    Rigidbody rb;
    float timer;
    float lastGroundedTime;
    Vector3 groundNormal = Vector3.up;
    float stuckTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.8f, 0f);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    // counts as "on the ground" while touching a surface that faces upward
    void OnCollisionStay(Collision c)
    {
        foreach (var contact in c.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                lastGroundedTime = Time.time;
                groundNormal = contact.normal;
                return;
            }
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);

        timer += Time.fixedDeltaTime;
        if (timer < startDelay || target == null) return;

        // turn toward the player
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.01f) return;
        Quaternion look = Quaternion.LookRotation(dir);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, look, turnSpeed * Time.fixedDeltaTime));

        // grounded if it touched the ground in the last 0.3 seconds
        bool grounded = Time.time - lastGroundedTime < 0.3f;

        if (grounded)
        {
            Vector3 moveDir = Vector3.ProjectOnPlane(transform.forward, groundNormal).normalized;
            Vector3 desired = moveDir * speed;
            rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, desired,
                                                    acceleration * Time.fixedDeltaTime);
        }

        // unstick: if it barely moves while chasing, lift it and push it back
        if (rb.linearVelocity.magnitude < 1f) stuckTimer += Time.fixedDeltaTime;
        else stuckTimer = 0f;

        if (stuckTimer > stuckTime)
        {
            stuckTimer = 0f;
            rb.position += Vector3.up * 1.5f - transform.forward * 1.5f;
            rb.linearVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
            FindFirstObjectByType<GameManager>().Lose("BUSTED! The police caught you.");
    }
}