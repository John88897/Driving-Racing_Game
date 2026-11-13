using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public bool isFinishLine;   // tick on the finish line, leave off on the checkpoint

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null || !other.attachedRigidbody.CompareTag("Player"))
            return;

        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm == null) return;

        if (isFinishLine) gm.ReachFinish();
        else gm.ReachCheckpoint();
    }
}