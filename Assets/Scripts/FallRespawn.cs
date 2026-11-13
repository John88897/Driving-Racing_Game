using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallRespawn : MonoBehaviour
{
    public float fallLimit = -20f;   // below this height the car respawns

    Rigidbody rb;
    Vector3 startPos;
    Quaternion startRot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position + Vector3.up * 1f;
        startRot = transform.rotation;
    }

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.SetPositionAndRotation(startPos, startRot);
        }
    }
}