// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

// <summary>
// This script controls the behavior of the key collectible in the game.
// It rotates the key and plays a sound upon collection.
// </summary>
public class KeyBehaviour : MonoBehaviour
{
    // Speed at which the key rotates (degrees per second)
    [SerializeField]
    float rotationSpeed = 90f;

    // Audio source component to play the collection sound
    private AudioSource keyAudioSource;

    // <summary>
    // Initializes the audio source component on start.
    // </summary>
    void Start()
    {
        // Get the AudioSource component attached to the key GameObject
        keyAudioSource = GetComponent<AudioSource>();
    }
    // <summary>
    // Rotates the key continuously around the Y-axis.
    // </summary>
    void Update()
    {
        // Rotate the key around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // <summary>
    // Called when the player collects the key.
    // Plays the collection sound and destroys the key object.
    // </summary>
    // The player who collects the key
    public void Collect(PlayerBehaviour player)
    {
        if (keyAudioSource != null && keyAudioSource.clip != null)
        {
            // Play the collection sound at the key's position if audio source and clip are set
            AudioSource.PlayClipAtPoint(keyAudioSource.clip, transform.position);
        }
        // Remove the key from the scene
        Destroy(gameObject);
    }
}
