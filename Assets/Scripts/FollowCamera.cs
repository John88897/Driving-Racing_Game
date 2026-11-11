using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;                         // drag N_Muscle Car_10 here
    public Vector3 offset = new Vector3(0, 3.5f, -7f);
    public float positionSmooth = 6f;
    public float rotationSmooth = 4f;

    void LateUpdate()
    {
        if (target == null) return;

        // position behind the car (relative to the car's direction)
        Vector3 desiredPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPos,
                                          positionSmooth * Time.deltaTime);

        // look slightly ahead of the car
        Vector3 lookPoint = target.position + target.forward * 3f + Vector3.up * 1f;
        Quaternion desiredRot = Quaternion.LookRotation(lookPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot,
                                              rotationSmooth * Time.deltaTime);
    }
}