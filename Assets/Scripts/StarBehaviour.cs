// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

// <summary>
// Controls the behavior of the star collectible.
// The star floats up and down for visual effect, plays a sound when collected,
// and adds points to the player's score.
// </summary>
public class StarBehaviour : MonoBehaviour
{
    // Star value that will be added to the player's score
    [SerializeField]
    int starValue = 50;
    // Speed of the floating animation
    [SerializeField]
    float floatSpeed = 2f; 
    // Height of the floating motion
    [SerializeField]
    float floatHeight = 0.1f;

     // Starting position of the star
    Vector3 startPos;
    // Audio source for the star collection sound
    AudioSource starCollect;

    // <summary>
    // Initializes the starting position and audio source.
    // </summary>
    void Start()
    {
        startPos = transform.position;
        starCollect = GetComponent<AudioSource>();
    }
    // <summary>
    // Creates a floating animation by modifying the Y position over time.
    // </summary>
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    // <summary>
    // Called when the player collects the star.
    // Plays the collection sound, adds score, and destroys the star.
    // </summary>
    // The player who collects the star
    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Star collected!");
        // Play the collection sound at the star's position
        if (starCollect != null)
        {
            // Detach audio source from star so it can finish playing
            AudioSource.PlayClipAtPoint(starCollect.clip, transform.position);
        }

        // Add points to the player's score
        player.ModifyPoints(starValue);

        Destroy(gameObject); // Destroy the star object
    }
}
