using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    AudioSource doorAudioSource;

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }
    public void Interact()
    {
        Vector3 doorRotation = transform.eulerAngles;
        if (doorRotation.y == 0f)
        {
            doorAudioSource.Play();
            doorRotation.y += 270f;
            transform.eulerAngles = doorRotation;
        }
        else if (doorRotation.y == 270f)
        {
            doorRotation.y = 0f;
            transform.eulerAngles = doorRotation;
        }
    }
}
