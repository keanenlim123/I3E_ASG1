using UnityEngine;

public class BootsBehaviour : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 90f;

    void Update()
    {
        // Rotate the key around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void Collect(PlayerBehaviour player)
    {
        Destroy(gameObject); // Destroy the Boots object
    }
}
