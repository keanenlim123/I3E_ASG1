// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

// <summary>
// Handles the behavior of spike traps in the game.
// When the player touches the spikes, they lose points and a sound is played.
// </summary>
public class SpikesBehaviour : MonoBehaviour
{
    // Number of points to subtract when the player hits the spikes
    [SerializeField]
    int minusValue = 10;

    // Audio source for the spike hit sound effect
    AudioSource spikesHit;
    // <summary>
    // Initializes the audio source component.
    // </summary>
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        spikesHit = GetComponent<AudioSource>();
    }

    // <summary>
    // Applies the spike penalty to the player and plays a sound.
    // </summary>
    // The player that triggered the spike
    public void Collect(PlayerBehaviour player)
    {
        // Deduct points from the player
        player.MinusPoints(minusValue);

        // Play the hit sound if an AudioSource and clip are assigned
        if (spikesHit != null)
        {
            spikesHit.Play();
        }
    }
}
