using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] KitchenObjectSO kitchenObjectSO;          //through this will get info which scriptable object prefab we have to instantiate
    [SerializeField] Transform spawnPoint;

    public void Interact()
    {
        Debug.Log("player interacted");
        Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab, spawnPoint);
        kitchenObjTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjTransform.GetComponent<KitchenObjects>().returnKitchObjSO().objectName);   //through this line of code we can figure out the scriptable object attach with kitchen object prefab
    }
}
