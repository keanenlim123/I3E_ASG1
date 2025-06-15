using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    [SerializeField]
    int minusValue = 10;

    AudioSource spikesHit;

    void Start()
    {
        spikesHit = GetComponent<AudioSource>();
    }

    public void Collect(PlayerBehaviour player)
    {
        player.MinusPoints(minusValue);

        if (spikesHit != null)
        {
            spikesHit.Play();
        }
    }
}
