using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    [SerializeField]
    int minusValue = 10;
    
    public void Collect(PlayerBehaviour player)
    {
        player.MinusPoints(minusValue);
    }
}
