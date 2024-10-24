using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] KitchenObjectSO kitchenObjectSO;          //through this will get info which scriptable object prefab we have to instantiate

    [SerializeField] Transform spawnPointCounter;

    private KitchenObjects kitchenObjects;
    


    public void Interact(Player player)
    {
        if (kitchenObjects == null)
        {
            Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab, spawnPointCounter);
            kitchenObjTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(this);
        }
        else
        {
            // clear counter already has kitchen object now return this to player
            kitchenObjects.SetKitchenObjectParent(player);
        }
    }



    public Transform GetKitchenObjSpawnPoint()
    {
        return spawnPointCounter;
    }



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









