using UnityEngine;

public class StarBehaviour : MonoBehaviour
{
    // Star value that will be added to the player's score
    [SerializeField]
    int starValue = 50;
    [SerializeField]
    float floatSpeed = 2f; // Speed of the float
    [SerializeField]
    float floatHeight = 0.1f;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    // Method to collect the star
    // This method will be called when the player interacts with the star
    // It takes a PlayerBehaviour object as a parameter
    // This allows the star to modify the player's score
    // The method is public so it can be accessed from other scripts
    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Star collected!");

        // Add the star value to the player's score
        // This is done by calling the ModifyScore method on the player object
        // The star is passed as an argument to the method
        // This allows the player to gain points when they collect the star
        player.ModifyPoints(starValue);

        Destroy(gameObject); // Destroy the star object
    }
}
