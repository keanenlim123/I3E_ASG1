using UnityEngine;

public class DoorBehaviour1 : MonoBehaviour
{
    private bool isOpened = false;

    public void OpenDoor()
    {
        if (isOpened) return;

        Vector3 doorRotation = transform.eulerAngles;
        doorRotation.y += 270f;
        transform.eulerAngles = doorRotation;

        isOpened = true;
    }
}
