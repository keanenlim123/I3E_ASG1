using UnityEngine;

public class RespawnBehaviour : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && teleportTarget != null)
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                other.transform.position = teleportTarget.position;
                cc.enabled = true;
                Debug.Log("Player teleported!");
            }
        }
    }
}
