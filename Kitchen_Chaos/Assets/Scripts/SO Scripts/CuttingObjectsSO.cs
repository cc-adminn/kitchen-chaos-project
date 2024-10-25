using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu()]
public class CuttingObjectsSO : ScriptableObject
{
    [SerializeField] KitchenObjectSO input;
    [SerializeField] KitchenObjectSO output;
}
