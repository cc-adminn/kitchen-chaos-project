using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{

    //this script is a component of kitchen objects gameobject which will be responsible for having a track of which clear counter its sit on


    [SerializeField] KitchenObjectSO kitchenObjectSO;


    private IKitchenObjectParent kitchenObjectParent;

    
    public KitchenObjectSO GetKitchObjSO()
    {
        return kitchenObjectSO;
    }



    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        if (this.kitchenObjectParent != null)           // checks if any clear counter previsously attach 
        {
            this.kitchenObjectParent.ClearKitchenObject();   //then remove the kitchen object refrence from that counter
        }

        this.kitchenObjectParent = kitchenObjectParent;         // and then assign clear counter to the newly given clear counter ref

        if(kitchenObjectParent.IsKitchenObjectPresent())
        {
            Debug.Log("KitchenParentObject already has a kitchen object");
        }

        kitchenObjectParent.SetKitchenObject(this);     //now assigning this kitchenobject to the new clear counter

        transform.parent = kitchenObjectParent.GetKitchenObjSpawnPoint();
        transform.localPosition = Vector3.zero;

        
    }



    public IKitchenObjectParent GetKItchenObjectParent()
    {
        return kitchenObjectParent;
    }


}
