using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;


    public KitchenObjectSO GetKitchObjSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        this.clearCounter = clearCounter;         //this keyword here means the variable of this script not variable of this function
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
