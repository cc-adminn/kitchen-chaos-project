using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] MeatCookRecipeSO[] meatCookRecipeSO;
    float fryingTimer;


    private void Update()
    {
        MeatCookRecipeSO meatCookRecipeSO = GetMeatRecipeWithInput(GetKitchenObjects().GetKitchObjSO());
        fryingTimer += Time.deltaTime;

        if (fryingTimer > meatCookRecipeSO.maxCutForCutting)
        {
            KitchenObjects kitchenObjects = GetKitchenObjects();
            kitchenObjects.DestroyItself();
            //Fried
            fryingTimer = 0f;
            KitchenObjects.SpawnKitchenObjectOnParent(meatCookRecipeSO.output, this);

        }
        Debug.Log(fryingTimer);
    }


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
                //CAN DO SWAP HERE
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


    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputSO)
    {
        MeatCookRecipeSO meatRecipeSO = GetMeatRecipeWithInput(inputSO);
        if (meatRecipeSO != null)
        {
            return meatRecipeSO.output;
        }
        else
        {
            return null;
        }
    } // this method return us the output from the recipe SO 


    private bool HasValidRecepie(KitchenObjectSO inputSO)
    {
        MeatCookRecipeSO meatRecipeSO = GetMeatRecipeWithInput(inputSO);
        return meatRecipeSO != null;
    }  // this method return true if it has the recipe of the input SO


    private MeatCookRecipeSO GetMeatRecipeWithInput(KitchenObjectSO kitchenObjectSOInput)
    {
        foreach (var recipe in meatCookRecipeSO)
        {
            if (recipe.input == kitchenObjectSOInput)
            {
                return recipe;
            }
        }
        return null;
    }    // this method returns us the recipe SO from the kitchen Object Input it has








}


