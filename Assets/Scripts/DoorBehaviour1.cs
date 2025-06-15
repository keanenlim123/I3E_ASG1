using UnityEngine;

public class DoorBehaviour1 : MonoBehaviour
{
    private bool isOpened = false;
    private AudioSource doorAudioSource;

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (isOpened) return;
        doorAudioSource.Play();

        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y += 270f;
        transform.eulerAngles = doorRotation;

        isOpened = true;
    }
}
