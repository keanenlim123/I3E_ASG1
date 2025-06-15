// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

// <summary>
// Controls the behavior of the diamond collectible.
// The diamond rotates continuously and plays a sound when collected,
// adding points to the player's score.
// </summary>

public class diamondBehaviour : MonoBehaviour
{
    // Diamond value that will be added to the player's score
    [SerializeField]
    int diamondValue = 10;
    // Speed at which the diamond rotates around the Y-axis
    [SerializeField]
    float rotationSpeed = 90f;
    // Audio source used to play the collection sound
    AudioSource collectSound;
    // <summary>
    // Called when the script starts. Initializes the audio source component.
    // </summary>
    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        collectSound = GetComponent<AudioSource>();
    }
    // <summary>
    // Rotates the diamond continuously around the Y-axis.
    // </summary>
    void Update()
    {
        // Rotate the diamond around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // <summary>
    // Called when the player collects the diamond.
    // Adds points to the player's score, plays a sound, and destroys the object.
    // </summary>
    // The player who collects the diamond
    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Diamond collected!");
        // Increase the player's score
        player.ModifyPoints(diamondValue);
        // Play the collection sound if available
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);
        }
        Destroy(gameObject); // Destroy the Diamond object
    }
}
