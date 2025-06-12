using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    [SerializeField]
    int minusValue = 20;
    public void Collect(PlayerBehaviour player)
    {
        player.MinusPoints(minusValue);
    }
}
