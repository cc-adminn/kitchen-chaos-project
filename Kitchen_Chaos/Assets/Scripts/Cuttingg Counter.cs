using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttinggCounter : BaseCounter
{

    [SerializeField] CuttingRecipeSO[] cuttingObjectsSOs;

    [SerializeField] int cuttingProgress;



    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarUpdate;    // declaring event

    public class OnProgressBarChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }    // making class for sending extra data with the event



    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent() && HasValidRecepie(player.GetKitchenObjects().GetKitchObjSO()))     //player has kitchen object, that can be cutted
            {
                player.GetKitchenObjects().SetKitchenObjectParent(this);

                cuttingProgress = 0;     // if player drops, kitchen object


                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObjects().GetKitchObjSO());

                OnProgressBarUpdate?.Invoke(this, new OnProgressBarChangedEventArgs   //firing event for progress Ui Update
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCutForCutting 
                });

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
    }  // this interaction is the basic interaction to place kitchen objects on the counter


    public override void InteractAlternate(Player player)
    {
        if (IsKitchenObjectPresent() && HasValidRecepie(GetKitchenObjects().GetKitchObjSO()))    // if any kitchen object is present that can be cutted
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObjects().GetKitchObjSO());

            cuttingProgress++;

            OnProgressBarUpdate?.Invoke(this, new OnProgressBarChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCutForCutting
            });   //firing event for progress Ui Update

            if (cuttingProgress >= cuttingRecipeSO.maxCutForCutting)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObjects().GetKitchObjSO());  //call the function we made to get the output for the recepie

                GetKitchenObjects().DestroyItself();  //destroy previous kitchenObject after if we destroy it first we dont have data for input of Recepie

                KitchenObjects.SpawnKitchenObjectOnParent(outputKitchenObjectSO, this);  //now spawn the output( SLICES )
            }
        }
    } // this interaction is used to cut kitchen objects present on cutting counter


    public KitchenObjectSO GetOutputForInput (KitchenObjectSO inputSO) 
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    } // this method return us the output from the recipe SO 


    private bool HasValidRecepie(KitchenObjectSO inputSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(inputSO);
        return cuttingRecipeSO != null;
    }  // this method return true if it has the recipe of the input SO


    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO kitchenObjectSOInput)
    {
        foreach (var recipe in cuttingObjectsSOs)
        {
            if (recipe.input == kitchenObjectSOInput)
            {
                return recipe;
            }
        }
        return null;
    }    // this method returns us the recipe SO from the kitchen Object Input it has
}
