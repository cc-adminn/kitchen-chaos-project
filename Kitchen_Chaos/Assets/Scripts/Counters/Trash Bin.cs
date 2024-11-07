using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.IsKitchenObjectPresent())
        {
            KitchenObjects kitchenObject = player.GetKitchenObjects();

            kitchenObject.DestroyItself();
        }
    }
}
