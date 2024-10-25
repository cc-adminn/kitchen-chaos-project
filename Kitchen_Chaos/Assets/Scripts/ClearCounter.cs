using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    [SerializeField] Player player;

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
                // can do swapping here but we are not allowing it, because in Kitchen object script SetKitchenObjectParent we have said set parent if it dont have kitchen object before
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









