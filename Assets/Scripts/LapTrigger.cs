using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public bool isFinishLine;   // tick on the finish line, leave off on the checkpoint

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " touched by: " + other.name);

        // find the player even if the collider is on a child object
        Transform root = other.attachedRigidbody != null
            ? other.attachedRigidbody.transform
            : other.transform.root;

        if (!root.CompareTag("Player"))
        {
            Debug.Log(name + ": ignored, " + root.name + " is not tagged Player");
            return;
        }

        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm == null)
        {
            Debug.LogError(name + ": no GameManager found in the scene!");
            return;
        }

        if (isFinishLine)
        {
            Debug.Log(name + ": FINISH LINE reached");
            gm.ReachFinish();
        }
        else
        {
            Debug.Log(name + ": CHECKPOINT reached");
            gm.ReachCheckpoint();
        }
    }
}