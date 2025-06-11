using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    AudioSource doorAudioSource;
    bool isOpen = false;

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y = 270f;
        transform.eulerAngles = doorRotation;

        doorAudioSource.Play();
        isOpen = true;

        // Automatically close door after 3 seconds
        Invoke(nameof(CloseDoor), 3f);
    }

    void CloseDoor()
    {
        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y = 0f;
        transform.eulerAngles = doorRotation;

        doorAudioSource.Play();
        isOpen = false;
    }
}
