using UnityEngine;
using System;
using System.Collections.Generic;

public class PlateCompleteVisuals : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjSO;
        public GameObject ingredientGameObject;
    }
    [SerializeField] List<KitchenObjectSO_GameObject> ingredientPlateStructList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject ingredient in ingredientPlateStructList)
        {
            if(ingredient.kitchenObjSO == e.kitchenObjectSO)
            {
                ingredient.ingredientGameObject.SetActive(true);
            }
        }
    }
}
