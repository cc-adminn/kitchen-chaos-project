using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{

    //this script is a component of kitchen objects gameobject which will be responsible for having a track of which clear counter its sit on


    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;

    
    public KitchenObjectSO GetKitchObjSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)           // checks if any clear counter previsously attach 
        {
            this.clearCounter.ClearKitchenObject();   //then remove the kitchen object refrence from that counter
        }

        this.clearCounter = clearCounter;         // and then assign clear counter to the newly given clear counter ref

        if(clearCounter.IsKitchenObjectPresent())
        {
            Debug.Log("Counter already has a kitchen object");
        }

        clearCounter.SetKitchenObject(this);     //now assigning this kitchenobject to the new clear counter

        transform.parent = clearCounter.GetKitchenObjSpawnPoint();
        transform.localPosition = Vector3.zero;

        

    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
