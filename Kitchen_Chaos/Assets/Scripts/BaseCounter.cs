using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{

    private KitchenObjects kitchenObjects;
    //Transform spawnPointCounter;


    public virtual void Interact(Player player)
    {

    }

    //public Transform GetKitchenObjSpawnPoint()
    //{
    //    return spawnPointCounter;
    //}



    public void SetKitchenObject(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;
    }



    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObjects;
    }



    public bool IsKitchenObjectPresent()
    {
        return kitchenObjects != null;
    }



    public void ClearKitchenObject()
    {
        kitchenObjects = null;
    }
}
