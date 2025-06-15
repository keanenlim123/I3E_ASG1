using UnityEngine;

public class BootsBehaviour : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 90f;
    private AudioSource bootsAudioSource;
    void Start()
    {
        bootsAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Rotate the boots around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void Collect(PlayerBehaviour player)
    {
        if (bootsAudioSource != null && bootsAudioSource.clip != null)
        {
            // Play the clip at the boots' position
            AudioSource.PlayClipAtPoint(bootsAudioSource.clip, transform.position);
        }

        Destroy(gameObject); // Destroy the Boots object
    }
}
