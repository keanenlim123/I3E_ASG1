// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Reference to the door's audio source component
    AudioSource doorAudioSource;

    // Flag to track whether the door is currently open
    bool isOpen = false;

    // Called when the script starts
    void Start()
    {
        // Get the AudioSource component attached to the door
        doorAudioSource = GetComponent<AudioSource>();
    }
    // Called externally when the player interacts with the door
    public void Interact()
    {
        // If the door is not already open, open it
        if (!isOpen)
        {
            OpenDoor();
        }
    }

    // Opens the door by rotating it and playing the sound
    void OpenDoor()
    {
        // Set the door's Y rotation to 270 degrees (open position)
        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y = 270f;
        transform.eulerAngles = doorRotation;

        // Play the door open sound
        doorAudioSource.Play();
        isOpen = true;

        // Automatically close door after 3 seconds
        Invoke(nameof(CloseDoor), 3f);
    }
    // Closes the door by rotating it back and playing the sound
    void CloseDoor()
    {
        // Set the door's Y rotation to 0 degrees (closed position)
        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y = 0f;
        transform.eulerAngles = doorRotation;
        
        // Play the door close sound
        doorAudioSource.Play();
        isOpen = false;
    }
}
