using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField]
    bool hasKey = false;
    [SerializeField]
    float rotationSpeed = 90f;

    void Update()
    {
        // Rotate the key around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void Collect(PlayerBehaviour player)
    {
        hasKey = true;
        Destroy(gameObject); // Destroy the key object
    }
}
