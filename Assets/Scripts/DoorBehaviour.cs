using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    AudioSource doorAudioSource;
    bool isOpen = false;
    float closeTimer = 0f;
    float autoCloseDelay = 3f;

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOpen)
        {
            closeTimer += Time.deltaTime;

            if (closeTimer >= autoCloseDelay)
            {
                // Close the door
                Vector3 doorRotation = transform.eulerAngles;
                doorRotation.y = 0f;
                transform.eulerAngles = doorRotation;
                isOpen = false;
                closeTimer = 0f;
            }
        }
    }

    public void Interact()
    {
        if (!isOpen)
        {
            // Open the door
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y = 270f;
            transform.eulerAngles = doorRotation;
            doorAudioSource.Play();
            isOpen = true;
            closeTimer = 0f;
        }
    }
}
