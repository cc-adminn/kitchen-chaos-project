using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] KitchenObjectSO kitchenObjectSO;          //through this will get info which scriptable object prefab we have to instantiate
    [SerializeField] Transform spawnPoint;
    private KitchenObjects kitchenObjects;  

    public void Interact()
    {
        if (kitchenObjects == null)
        {
            Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab, spawnPoint);
            kitchenObjTransform.localPosition = Vector3.zero;
            kitchenObjects = kitchenObjTransform.GetComponent<KitchenObjects>();
            kitchenObjects.SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObjects.GetClearCounter());
        }


    }
}
