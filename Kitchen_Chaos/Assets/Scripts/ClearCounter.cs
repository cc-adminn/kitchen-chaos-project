using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    private KitchenObjects ktObj;

    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent())     //counter already has kitchen object
            {
                player.GetKitchenObjects().SetKitchenObjectParent(this);
            }
            else         //both dont have kitchen object
            {
                //DO NOTHING
            }
        }
        else      //counter has kitchen object
        {
            if (!player.IsKitchenObjectPresent())    //player has nothing
            {
                GetKitchenObjects().SetKitchenObjectParent(player);
            }
            else    //both have kitchen object
            {
                //DO NOTHING
            }
        }
    }
    


   
}









