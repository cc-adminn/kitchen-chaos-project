using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] MeatCookRecipeSO[] meatCookRecipeSOs;
    [SerializeField] BurningRecipeSO[] burningRecipeSOs;

    MeatCookRecipeSO meatCookingRecipeSO;
    BurningRecipeSO burningRecipeSO;
    float fryingTimer;
    float burningTime;

    public enum FryingState
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public FryingState fryingState;





    /// <summary>
               ///BODY STATRS HERE
    /// </summary>
    private void Start()
    {
        fryingState = FryingState.Idle;
    }

    private void Update()
    {
        if (IsKitchenObjectPresent())
        {


            switch (fryingState)
            {
                case FryingState.Idle:       //idle state logic
                    fryingTimer = 0f;
                    burningTime = 0f;
                    break;

                case FryingState.Frying:   //frying state logic

                        fryingTimer += Time.deltaTime;

                        if (fryingTimer > meatCookingRecipeSO.maxCutForCutting)
                        {
                            GetKitchenObjects().DestroyItself();
                            //Fried

                            KitchenObjects.SpawnKitchenObjectOnParent(meatCookingRecipeSO.output, this);
   
                            fryingState = FryingState.Fried;
                        }
                    break;


                case FryingState.Fried:    //fried state logic

                    burningRecipeSO = GetBurningRecipeWithInput(GetKitchenObjects().GetKitchObjSO());
                    Debug.Log(burningRecipeSO.name);
                    burningTime += Time.deltaTime;

                    if (burningTime > burningRecipeSO.maxTimeForBurning)
                    {

                        GetKitchenObjects().DestroyItself();
                        //Fried

                        KitchenObjects.SpawnKitchenObjectOnParent(burningRecipeSO.output, this);

                        fryingState = FryingState.Burned;
                    }
                    break;


                case FryingState.Burned:   //burned state

                    break;
            }

        }
        Debug.Log(fryingTimer);
    }


    public override void Interact(Player player)
    {
        if (!IsKitchenObjectPresent())  //counter has nothing 
        {
            if (player.IsKitchenObjectPresent() && HasValidRecepie(player.GetKitchenObjects().GetKitchObjSO()))     //player has kitchen object, that can be cutted
            {
                fryingState = FryingState.Frying;
                player.GetKitchenObjects().SetKitchenObjectParent(this);

                meatCookingRecipeSO = GetMeatRecipeWithInput(GetKitchenObjects().GetKitchObjSO());  //when interact we get the recipe

                fryingTimer = 0f;
                fryingState = FryingState.Frying;
                
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
                fryingState = FryingState.Idle;
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
        foreach (var recipe in meatCookRecipeSOs)
        {
            if (recipe.input == kitchenObjectSOInput)
            {
                return recipe;
            }
        }
        return null;
    }    // this method returns us the recipe SO from the kitchen Object Input it has

    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO kitchenObjectSOInput)
    {
        foreach (var recipes in burningRecipeSOs)
        {
            if (recipes.input == kitchenObjectSOInput)
            {
                return recipes;
            }
        }
        return null;
    }






}


