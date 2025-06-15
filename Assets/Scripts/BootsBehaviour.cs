// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;


// <summary>
// This script controls the behavior of the boots collectible in the game.
// The boots rotate visually and play a sound when collected by the player.
// </summary>
public class BootsBehaviour : MonoBehaviour
{
    // Rotation speed of the boots in degrees per second
    [SerializeField]
    float rotationSpeed = 90f;

    // AudioSource component attached to the boots GameObject
    private AudioSource bootsAudioSource;

    // <summary>
    // Called when the script starts.
    // Initializes the AudioSource component.
    // </summary>
    void Start()
    {
        // Get the AudioSource attached to the boots
        bootsAudioSource = GetComponent<AudioSource>();
    }
    // <summary>
    // Rotates the boots around the Y-axis for a spinning visual effect.
    // </summary>
    void Update()
    {
        // Rotate the boots around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    // <summary>
    // Called when the player collects the boots.
    // Plays a sound effect and destroys the boots GameObject.
    // </summary>
    // The player who collects the boots
    public void Collect(PlayerBehaviour player)
    {
        // If an audio clip is assigned, play it at the boots' position
        if (bootsAudioSource != null && bootsAudioSource.clip != null)
        {
            // Play the clip at the boots' position
            AudioSource.PlayClipAtPoint(bootsAudioSource.clip, transform.position);
        }

        Destroy(gameObject); // Destroy the Boots object
    }
}
