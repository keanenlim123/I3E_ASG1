// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

// <summary>
// Controls the behavior of the water hazard in the game.
// If the player does not have boots, they will take damage and respawn upon contact.
// </summary>

public class WaterBehaviour : MonoBehaviour
{
    // Amount of points to subtract from the player if they fall into water without boots
    [SerializeField]
    int minusValue = 20;
    // Timer for potential future use (e.g., damage over time)
    private float damageTimer = 0f;
    // Audio source that plays an electric hit sound when the player is damaged
    AudioSource electricHit;
    // <summary>
    // Initializes the audio source component.
    // </summary>
    void Start()
    {
        electricHit = GetComponent<AudioSource>();
    }

    // <summary>
    // Applies the water penalty to the player (if they don't have boots).
    // </summary>
    // The player who entered the water
    public void Collect(PlayerBehaviour player)
    {
        // Check if player has boots
        if (!player.HasBoots())
        {
            // Play damage sound
            electricHit.Play();
            // Deduct points from the player
            player.MinusPoints(minusValue);
        }
    }
    // <summary>
    // Called when another collider enters this trigger.
    // Checks if it's the player and applies penalty if they lack boots.
    // </summary>
    // The collider that entered the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                // Apply damage or effects based on boots status
                Collect(player);
                // Respawn the player only if they don't have boots
                if (!player.HasBoots())
                {
                    player.Respawn();
                }
            }
        }
    }
    // <summary>
    // Called when the player leaves the water.
    // Resets the damage timer (if used for time-based effects).
    // </summary>
    // The collider that exited the trigger
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the timer when the player leaves the water
            damageTimer = 0f;
        }
    }
}
