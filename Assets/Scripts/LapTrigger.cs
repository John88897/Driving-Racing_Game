using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public bool isFinishLine;   // tick on the finish line, leave off on the checkpoint
    public AudioClip sound;     // checkpoint sound (the finish sound is played by GameManager)

    void OnTriggerEnter(Collider other)
    {
        // find the player even if the collider is on a child object
        Transform root = other.attachedRigidbody != null
            ? other.attachedRigidbody.transform
            : other.transform.root;

        if (!root.CompareTag("Player")) return;

        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm == null)
        {
            Debug.LogError(name + ": no GameManager found in the scene!");
            return;
        }

        if (isFinishLine)
        {
            gm.ReachFinish();
        }
        else
        {
            if (sound != null && Camera.main != null)
                AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
            gm.ReachCheckpoint();
        }
    }
}