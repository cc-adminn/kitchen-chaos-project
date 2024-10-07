using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] PlayerInputActionClass playerInputActionClass;


    void Awake()
    {
        playerInputActionClass = new PlayerInputActionClass();
        playerInputActionClass.Player.Enable();
    }
    public Vector2 MovementVector2Normalized()
    {
      
    Vector2 inputVector = playerInputActionClass.Player.Move.ReadValue<Vector2>();
    inputVector = inputVector.normalized;
    
    return inputVector;
    }
}
