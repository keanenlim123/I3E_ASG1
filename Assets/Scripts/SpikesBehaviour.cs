using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    [SerializeField]
    int minusValue = 5;
    
    public void Collect(PlayerBehaviour player)
    {
        player.MinusPoints(minusValue);
    }
}
