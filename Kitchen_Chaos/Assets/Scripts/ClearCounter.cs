using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] KitchenObjectSO kitchenObjectSO;          //through this will get info which scriptable object prefab we have to instantiate
    [SerializeField] Transform spawnPoint;
    private KitchenObjects kitchenObjects;
    [SerializeField] bool testing;
    [SerializeField] ClearCounter secondClearCounter;

    public void Interact()
    {
        if (kitchenObjects == null)
        {
            Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab, spawnPoint);
            kitchenObjTransform.GetComponent<KitchenObjects>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObjects.GetClearCounter());
        }
    }


    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.K))
        {
            if (kitchenObjects!= null )
            {
                kitchenObjects.SetClearCounter(secondClearCounter);
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









