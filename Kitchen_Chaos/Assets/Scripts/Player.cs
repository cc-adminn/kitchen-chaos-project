using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private ClearCounter selecctedCounter;
  float bodyHeight = 2f;
  float bodyRadius = 0.7f;
  Vector3 lastInteractibleDir;
  [SerializeField] float playerSpeed;
  [SerializeField] float playerRotation;
  [SerializeField] float playerInteractionDist = 3f;
  [SerializeField] GameInput gameInput;
  [SerializeField] LayerMask layerMaskForCounters;
  void Start()
  {
    gameInput.OnInteract += GameInput_OnInteract;
  }
  
   private void Update()
   {
    HandleMovement();
    HandleInteraction();
   }

   void GameInput_OnInteract(object sender, EventArgs eventArgs)              //as the delegate was of type EventArgs so we have to make two paremeter inside that method
   {
      if(selecctedCounter != null)
      {
        selecctedCounter.Interact();
      }
   }

   private void HandleInteraction()
   {
    Vector2 inputVector2FromGameInput = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector2FromGameInput.x, 0, inputVector2FromGameInput.y);

    if (movDir != Vector3.zero)    //we check if the movement input is not zero and has a value we immediately store it in a new varialble called lastInteractDir
    {
      lastInteractibleDir = movDir;  //reason: when we stop movDir becomes zero so even if we are near any object we are not throwing raycast in any direction
    }
   
    
    if (Physics.Raycast(transform.position, lastInteractibleDir, out RaycastHit hitInfo, playerInteractionDist))
      {
            if(hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))   //clear counter is a local variable of method while selectedCounter is a usual private member of the class
            {
                  if (clearCounter != selecctedCounter)
                  {
                    selecctedCounter = clearCounter;
                  }
            }
            else
            {
              selecctedCounter = null;
            }
      }
    else
      {
           selecctedCounter = null;
      }
    Debug.Log(selecctedCounter);
    
   }
   private void HandleMovement()
   {
    Vector2 inputVector2FromGameInput = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector2FromGameInput.x, 0, inputVector2FromGameInput.y);
    Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
    Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
    
    Vector3 endPoint = transform.position + Vector3.up * bodyHeight;                            //position of this gameobject is its base while we move up from base 2 units to reach its height
    bool canMove = !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDir, playerSpeed * Time.deltaTime);
    
    if (!canMove)       //mean we have hit something lets check if we can move on x axis for this we will direction dedicated for x-axis
    {
      canMove = !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDirX, playerSpeed* Time.deltaTime, layerMaskForCounters);

      if (canMove)
      {
        movDir = movDirX;
      }
      else
      {
        canMove = !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDirZ, playerSpeed * Time.deltaTime, layerMaskForCounters);
        if (canMove)
        {
          movDir = movDirZ;
        }
      }


    }


    if (canMove)
    {
      transform.position += movDir * playerSpeed * Time.deltaTime;
    }
    
    
    transform.forward = Vector3.Slerp(transform.forward, movDir , Time.deltaTime * playerRotation);       //this line is for the rotation of the body when its moving and rotating it on the same direction where moving
   }





}
