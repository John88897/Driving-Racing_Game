using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoliceChase : MonoBehaviour
{
    public Transform target;          // drag N_Muscle Car_10 here
    public float speed = 12f;
    public float acceleration = 20f;
    public float turnSpeed = 2f;
    public float startDelay = 3f;
    public float extraGravity = 30f;

    Rigidbody rb;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.8f, 0f);
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        if (target == null)
        {
            // try to find the player automatically
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) target = p.transform;
        }

        if (target == null) Debug.LogError("PoliceChase: Target is empty and no object has the Player tag!");
        else Debug.Log("PoliceChase: chasing " + target.name);
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

        // push forward (horizontal), cap horizontal speed
        Vector3 vel = rb.linearVelocity;
        float horizontalSpeed = new Vector3(vel.x, 0f, vel.z).magnitude;
        if (horizontalSpeed < speed)
        {
            Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            rb.AddForce(flatForward * acceleration, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null) gm.Lose("BUSTED! The police caught you.");
        }
    }
}