// <author> Keanen Lim </author>
// <date> 15/6/2025 </date>
// <Student ID> S10270417C </author>
using UnityEngine;

// <summary>
// Controls the behavior of a door that opens when triggered.
// Plays a sound and rotates the door to simulate it opening.
// </summary>
public class DoorBehaviour1 : MonoBehaviour
{
    // Flag to check if the door has already been opened
    private bool isOpened = false;
    // Audio source to play the door opening sound
    private AudioSource doorAudioSource;

    // <summary>
    // Initializes the audio source component.
    // </summary>

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }
    // <summary>
    // Opens the door by rotating it and playing a sound.
    // Prevents the door from being opened multiple times.
    // </summary>
    public void OpenDoor()
    {
        // Do nothing if the door is already opened
        if (isOpened) return;
        // Play the door opening sound
        doorAudioSource.Play();

        // Rotate the door 270 degrees around the Y-axis
        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y += 270f;
        transform.eulerAngles = doorRotation;
        // Mark the door as opened
        isOpened = true;
    }
}
