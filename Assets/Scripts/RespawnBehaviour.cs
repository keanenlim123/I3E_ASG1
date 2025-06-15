// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

public class RespawnBehaviour : MonoBehaviour
{
    // Target location where the player will be teleported to
    [SerializeField] private Transform teleportTarget;
    // <summary>
    // Called when another collider enters the trigger attached to this GameObject.
    // Checks if the object is the player, and if so, teleports them to the target position.
    // </summary>
    // The collider that entered the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player and a teleport target is set
        if (other.CompareTag("Player") && teleportTarget != null)
        {
            // Get the CharacterController component from the player
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                // Temporarily disable CharacterController to avoid teleporting issues
                cc.enabled = false;
                // Move the player to the teleport target position
                other.transform.position = teleportTarget.position;
                // Re-enable the CharacterController after teleporting
                cc.enabled = true;
                // Debug message to confirm teleportation
                Debug.Log("Player teleported!");
            }
        }
    }
}
