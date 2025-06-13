using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    [SerializeField]
    int minusValue = 10;

    public void Collect(PlayerBehaviour player)
    {
        // Only reduce points if player does NOT have boots
        if (!player.HasBoots())
        {
            player.MinusPoints(minusValue);
        }
    }

}
