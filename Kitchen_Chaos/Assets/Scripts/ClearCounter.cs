using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] KitchenObjectSO kitchenObjectSO;          //through this will get info which scriptable object prefab we have to instantiate
    [SerializeField] Transform spawnPoint;
    private KitchenObjects kitchenObjects;
    [SerializeField] bool testing;
    [SerializeField] ClearCounter secondClearCounter;


    public void Interact(Player player)
    {
        if (kitchenObjects == null)
        {
            Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab, spawnPoint);
            kitchenObjTransform.GetComponent<KitchenObjects>().SetKitchenObjectParent(this);
        }
        else
        {
            Debug.Log(kitchenObjects.GetKItchenObjectParent());
        }
    }



    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.K))
        {
            if (kitchenObjects!= null )
            {
                kitchenObjects.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }



    public Transform GetKitchenObjSpawnPoint()
    {
        return spawnPoint;
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









