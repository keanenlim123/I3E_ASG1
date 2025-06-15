// <summary>
// PlayerBehaviour.cs
// This script handles the player's interactions with objects, UI updates, inventory tracking, and respawning logic in the game. 
// </summary>
// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>

using UnityEngine;
using UnityEngine.UI;
using TMPro;

// <summary>
// Controls the player's behavior including interactions with items, doors, water, spikes,
// UI updates, and score tracking.
// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI interactPromptText;

    [SerializeField]
    private Image goals;
    [SerializeField]
    private Image diamond;
    [SerializeField]
    private Image star;
    [SerializeField]
    private TextMeshProUGUI inventory;
    [SerializeField]
    private Image points;

    [SerializeField]
    Image keyIcon;

    [SerializeField]
    Image bootsIcon;

    [SerializeField] private TextMeshProUGUI diamondCounterText;
    [SerializeField] private TextMeshProUGUI starCounterText;

    private int diamondCount = 0;
    private int starCount = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]

    int currentPoints = 0;
    [SerializeField]
    public Transform spawnLocation;
    bool canInteract = false;

    ExitBehaviour currentExit = null;

    diamondBehaviour currentDiamond = null;

    StarBehaviour currentStar = null;
    KeyBehaviour keyCollected = null;

    BootsBehaviour bootsCollected = null;
    bool hasKey = false;

    bool hasBoots = false;
    DoorBehaviour currentDoor = null;

    [SerializeField]
    float interactRange = 2f;
    [SerializeField]
    float rayHeightOffset = 1.0f;
    DoorBehaviour1 currentDoor2 = null;

    [SerializeField]
    private TextMeshProUGUI objectiveText;
    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button
    // <summary>Called every frame to check for interactable objects using raycasting.</summary>
    void Update()
    {
        RaycastHit hit;
        canInteract = false;

        Vector3 rayOrigin = transform.position + Vector3.up * 1.0f;
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, interactRange))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Diamond"))
            {
                canInteract = true;
                currentDiamond = hitObject.GetComponent<diamondBehaviour>();
                currentStar = null;
                keyCollected = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Star"))
            {
                canInteract = true;
                currentStar = hitObject.GetComponent<StarBehaviour>();
                currentDiamond = null;
                keyCollected = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Key"))
            {
                canInteract = true;
                interactPromptText.text = "Press E to pick up Key!";
                interactPromptText.gameObject.SetActive(true);
                keyCollected = hitObject.GetComponent<KeyBehaviour>();
                currentDiamond = null;
                currentStar = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Exit"))
            {
                canInteract = true;
                currentExit = hitObject.GetComponent<ExitBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                bootsCollected = null;
                currentDoor = null;

                interactPromptText.text = "Press E to open the chest!";
                interactPromptText.gameObject.SetActive(true);
            }

            else if (hitObject.CompareTag("Door2"))
            {
                canInteract = true;
                currentDoor2 = hitObject.GetComponent<DoorBehaviour1>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                bootsCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Boots"))
            {
                canInteract = true;
                interactPromptText.text = "Press E to pick up boots!";
                interactPromptText.gameObject.SetActive(true);
                bootsCollected = hitObject.GetComponent<BootsBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                currentDoor = null;
            }
            else if (hitObject.CompareTag("Door"))
            {
                canInteract = true;
                currentDoor = hitObject.GetComponent<DoorBehaviour>();
                currentDiamond = null;
                currentStar = null;
                keyCollected = null;
                bootsCollected = null;
            }
        }
        else
        {
            currentDiamond = null;
            currentStar = null;
            keyCollected = null;
            bootsCollected = null;
            currentDoor = null;
            currentDoor2 = null;
            interactPromptText.gameObject.SetActive(false);

        }

        Debug.DrawRay(rayOrigin, transform.forward * interactRange, Color.green);
    }
    // <summary>Handles triggers such as spikes and water.</summary>
    // The collider that the player has entered.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spikes"))
        {
            SpikesBehaviour spikes = other.GetComponent<SpikesBehaviour>();
            if (spikes != null)
            {
                spikes.Collect(this);
                Respawn();
                Debug.Log("Teleport (Spikes)");
            }
        }

        if (other.CompareTag("Water"))
        {
            WaterBehaviour water = other.GetComponent<WaterBehaviour>();
            if (water != null)
            {
                if (!HasBoots())
                {
                    water.Collect(this);  // Deduct points
                    Respawn();            // Teleport back
                    Debug.Log("Teleport (Water)");
                }
                else
                {
                    Debug.Log("Player has boots - safe in water!");
                }
            }
        }
    }
    // <summary>Handles logic when exiting trigger zones.</summary>
    // The collider exited.
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spikes"))
        {
            Debug.Log("Player exited spikes trigger area.");
        }

        if (other.CompareTag("Water"))
        {
            Debug.Log("Player exited water trigger area.");
        }
    }
    // <summary>Respawns the player at the designated spawn location.</summary>
    public void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (spawnLocation != null)
        {
            transform.position = spawnLocation.position;
            Debug.Log("Teleporting to: " + spawnLocation.position);

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.Sleep(); // Prevents unwanted momentum on respawn
            }

            // Ensures the transform changes are immediately registered by Unity
            Physics.SyncTransforms();
        }
        else
        {
            Debug.LogWarning("Spawn location not assigned!");
        }

        // Optional: Reset health, UI, or other stats here
        // currentHealth = maxHealth;
        // healthText.text = "HEALTH: " + currentHealth.ToString();
        // Debug.Log("Player respawned and health reset.");
    }
    // <summary>Handles interaction logic when the player presses the interact button.</summary>
    void OnInteract()
    {
        if (canInteract)
        {
            if (currentDiamond != null)
            {
                Debug.Log("Interacting with diamond");
                currentDiamond.Collect(this);
                diamondCount++;
                UpdateDiamondUI();
            }
            else if (currentStar != null)
            {
                Debug.Log("Interacting with star");
                currentStar.Collect(this);
                starCount++;
                UpdateStarUI();
            }
            else if (bootsCollected != null)
            {
                Debug.Log("Interacting with boots");
                bootsCollected.Collect(this);
                hasBoots = true;
                bootsIcon.gameObject.SetActive(true);
                objectiveText.text = "- Escape";
            }

            else if (keyCollected != null)
            {
                Debug.Log("Interacting with key");
                keyCollected.Collect(this);
                hasKey = true;
                keyIcon.gameObject.SetActive(true); // Show the UI image
                objectiveText.text = "- Open the Door!";
            }
            else if (currentExit != null)
            {
                Debug.Log("Interacting with chest (Exit)");
                currentExit.SetFinalStats(diamondCount, starCount, currentPoints);
                currentExit.ShowEndScreen();

                // Hide gameplay UI
                goals.gameObject.SetActive(false);
                diamond.gameObject.SetActive(false);
                star.gameObject.SetActive(false);
                inventory.gameObject.SetActive(false);
                points.gameObject.SetActive(false);
            }
            else if (currentDoor2 != null)
            {
                Debug.Log("Interacting with Door2");
                currentDoor2.OpenDoor();
            }
            else if (currentDoor != null)
            {
                if (hasKey)
                {
                    Debug.Log("Interacting with door");
                    currentDoor.Interact();
                    objectiveText.text = "- Find the Boots!";
                }
                else
                {
                    Debug.Log("You need a key to open the door!");
                    interactPromptText.gameObject.SetActive(true);
                    interactPromptText.text = "Door is Locked!";
                    objectiveText.text = "- Find a Key!";
                }
            }
            else
            {
                interactPromptText.gameObject.SetActive(false);
            }
        }
    }

    // Function to check player haveboots
    public bool HasBoots()
    {
        return hasBoots;
    }

    // <summary>Returns true if the player has boots.</summary>
    // <summary>Adds points to the player’s score.</summary>
    // The amount of points to add.
    public void ModifyPoints(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentPoints += amt;
        UpdateScoreUI();
    }
    // <summary>Subtracts points from the player’s score.</summary>
    // The amount of points to subtract.
    public void MinusPoints(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentPoints -= amt;
        UpdateScoreUI();
    }
    // <summary>Updates the UI for the current score.</summary>
    void UpdateScoreUI()
    {
        scoreText.text = "Points: " + currentPoints.ToString() + " / 350";
    }
    // <summary>Updates the UI for collected diamonds.</summary>
    void UpdateDiamondUI()
    {
        diamondCounterText.text = "Diamonds: " + diamondCount + " / 20";
    }
    // <summary>Updates the UI for collected stars.</summary>
    void UpdateStarUI()
    {
        starCounterText.text = "Stars: " + starCount + " /3";
    }
}
