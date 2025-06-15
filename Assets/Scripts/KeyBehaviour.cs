using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 90f;
    private AudioSource keyAudioSource;

    void Start()
    {
        keyAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Rotate the key around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void Collect(PlayerBehaviour player)
    {
        if (keyAudioSource != null && keyAudioSource.clip != null)
        {
            AudioSource.PlayClipAtPoint(keyAudioSource.clip, transform.position);
        }

        Destroy(gameObject); 
    }
}
