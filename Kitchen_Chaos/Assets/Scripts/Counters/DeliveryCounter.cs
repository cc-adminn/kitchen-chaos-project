using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.IsKitchenObjectPresent())
        {
            if (player.GetKitchenObjects().TryGetPlateOBject(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

                player.GetKitchenObjects().DestroyItself();
            }
        }
        
    }
}
