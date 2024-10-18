using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO returnKitchObjSO()
    {
        return kitchenObjectSO;
    }
}
