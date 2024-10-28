using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttinggCounter : BaseCounter
{

    [SerializeField] CuttingObjectsSO[] cuttingObjectsSOs;

    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent() && HasValidRecepie(player.GetKitchenObjects().GetKitchObjSO()))     //player has kitchen object, that can be cutted
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


    public override void InteractAlternate(Player player)
    {
        if (IsKitchenObjectPresent() && HasValidRecepie(GetKitchenObjects().GetKitchObjSO()))    // if any kitchen object is present that can be cutted
        {
            

            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObjects().GetKitchObjSO());  //call the function we made to get the output for the recepie
            Debug.Log(outputKitchenObjectSO);
            GetKitchenObjects().DestroyItself();  //destroy previous kitchenObject after if we destroy it first we dont have data for input of Recepie
            KitchenObjects.SpawnKitchenObjectOnParent(outputKitchenObjectSO, this);  //now spawn the output( SLICES )
            
        }
    }


    public KitchenObjectSO GetOutputForInput (KitchenObjectSO inputKitchenObjectSO) 
    {
        foreach (CuttingObjectsSO cuttingRecepieSO in cuttingObjectsSOs)   // we are looping through every recepie
        {
            if (cuttingRecepieSO.input == inputKitchenObjectSO)            // and finding the one which has same SO as player carrying
            {
                Debug.Log("right recipe found");
                return cuttingRecepieSO.output;                           // when found return output of that recepie  
            }
        }
        Debug.Log("no matching recepie found");
        return null;
    }

    private bool HasValidRecepie(KitchenObjectSO inputSO)
    {
        foreach (var cuttingRecepieSO in cuttingObjectsSOs)
        {
            if (cuttingRecepieSO.input == inputSO)
            {
                return true;
            }
        }
        Debug.Log("not a valid Kitchen Object");
        return false;
    }
}
