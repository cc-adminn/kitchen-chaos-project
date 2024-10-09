using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    PlayerInputActionClass playerInputActionClass;


    public static event EventHandler OnInteract;

    void Awake()
    {
        playerInputActionClass = new PlayerInputActionClass();
        playerInputActionClass.Player.Enable();
        playerInputActionClass.Player.Interact.performed += PlayerInteractions;
    }
    public Vector2 GetMovementVector2Normalized()
    {
      
    Vector2 inputVector = playerInputActionClass.Player.Move.ReadValue<Vector2>();
    inputVector = inputVector.normalized;
    
    return inputVector;
    }


    public void PlayerInteractions(InputAction.CallbackContext context)
    {
     OnInteract?.Invoke(this, EventArgs.Empty);
    }
}
