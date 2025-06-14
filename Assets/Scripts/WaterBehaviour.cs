using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    [SerializeField]
    int minusValue = 20;

    private float damageTimer = 0f;

    public void Collect(PlayerBehaviour player)
    {
        if (!player.HasBoots())
        {
            player.MinusPoints(minusValue);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                Collect(player);
                player.Respawn();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the timer when the player leaves the water
            damageTimer = 0f;
        }
    }
}
