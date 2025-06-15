using UnityEngine;

public class diamondBehaviour : MonoBehaviour
{
    // Diamond value that will be added to the player's score
    [SerializeField]
    int diamondValue = 10;
    [SerializeField]
    float rotationSpeed = 90f;
    AudioSource collectSound;
    void Start()
    {
        collectSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotate the diamond around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // Method to collect the diamond
    // This method will be called when the player interacts with the diamond
    // It takes a PlayerBehaviour object as a parameter
    // This allows the diamond to modify the player's score
    // The method is public so it can be accessed from other scripts
    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Diamond collected!");
        player.ModifyPoints(diamondValue);

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);
        }
        Destroy(gameObject); // No sound? Destroy immediately
    }
}
