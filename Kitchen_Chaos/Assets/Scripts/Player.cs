using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.iOS;

public class Player : MonoBehaviour
{
  public static Player Instance{get; private set;}                      //this is called property of class which we can set or get  

  public event EventHandler<OnSelectCounterChangedEventArgs> OnSelectedCounterChanged;   //making a new event when we approached a counter

   public class OnSelectCounterChangedEventArgs : EventArgs            //made class to send more data with the EventHandler's Event Args property 
   {
    public ClearCounter selectedCounter;
   }

  [SerializeField] float speed;
  [SerializeField] GameInput gameInput;
  private Vector3 lastInteractDirection;
  [SerializeField] LayerMask layerMaskForCounter;
  private ClearCounter selectedCounter;                                 //now i am using this comparing this counter with the counter that is present at the moment, in handle interaction method
   
   


   void Awake()
   {
      if(Instance != null)
      {
        Debug.LogError("There is more than one player instance");
      }
      Instance = this;
   }
   
   void Start()
   {
    GameInput.OnInteract += GameInput_OnInteraction;                    //listening the eventHandler call form gameInput and assigning it to the method 'Game
   }


   void Update()
   {
    HandleMovement();
    HandleInteractions();
   }

   void GameInput_OnInteraction(object sender, EventArgs eventArgs)    //if player pressed E, then a event happened in game input class that sends a message and we're handling it here
   {
    if (selectedCounter != null)
    {
      selectedCounter.Interact();
    }
   }

   void HandleInteractions()
   {
    Vector2 inputVector = gameInput.MovementVector2Normalized();        //getting input vecctor2 from playerInputSystemClass

    Vector3 movDir = new Vector3(inputVector.x , 0f, inputVector.y);    //we made a new Vector3 and assigned vector2(inputVector
    
    if (movDir != Vector3.zero)                                         //if movDir is changing than we assign movDir value to lastInteractDirection other we using a simple movDir 
    {
      lastInteractDirection = movDir;
    }
    float distanceOfRayCast = 2f;
    if(Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hitInfo, distanceOfRayCast, layerMaskForCounter))
    {
      if (hitInfo.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
      {
        if (clearCounter != selectedCounter)                            //if the clear counter we contacted with is not equal to the selectedCounter
        {
          SetSelectedCounter(selectedCounter);                          //then the selected counter will become the counter mean we will be enabling it, it is now on separate function
        }
        else
        {
          SetSelectedCounter(null);
        }
      }
      else                                                              //if the counter does not have clearcounter component than selectedCounter stil be null;
      {
        SetSelectedCounter(null);
      }
    }

    Debug.Log(selectedCounter);


   }

   void HandleMovement()
   {
    Vector2 inputVector = gameInput.MovementVector2Normalized();        //getting input vecctor2 from playerInputSystemClass

    Vector3 movDir = new Vector3(inputVector.x , 0f, inputVector.y);    //we made a new Vector3 and assigned vector2(inputVector2) x value to its x and its y value to vector3's z value



    float moveDistance  = speed * Time.deltaTime;                       //this the distance player can move in a single frame
    
    float bodyHeight = 2f;
    float bodyRadius = .7f;                                             //estimated thickness of the capsule body
    
    bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * bodyHeight , bodyRadius , movDir , moveDistance );


    if (!canMove)                                                       //if player has collided with something lets see what else we can do
    {
      

                                      
      Vector3 movDirX = new Vector3(movDir.x, 0 , 0);                   //direction to move only in X axis
      Vector3 movDirZ = new Vector3(0, 0, movDir.z);                    //direction to move only in Z axis


      canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * bodyHeight , bodyRadius , movDirX , moveDistance );
         if (canMove)       
         {
          movDir = movDirX;      //the above line checks collision in the direction of x and if there isnt any collision, then it move change direction to specifically in x axis
         }
         else
         {                       //if there is not any collision in x direction then check for collsion in z axis if their is no collision then move to z direction
          canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * bodyHeight , bodyRadius, movDirZ, moveDistance);

          if (canMove)       
         {
          movDir = movDirZ;      //direction changes to specifically z direction, no more possibilities
         }

         }
    }

    if (canMove)                 //if no collision detected move freely
    {
      transform.position += movDir *moveDistance ;
    }
    

                              
    Debug.DrawLine(transform.position, transform.position + Vector3.up * bodyHeight +  movDir * moveDistance, Color.red);  //debug line to check player body in editor
    

    //for player face rotation
    float rotationSpeed = 15f;
    transform.forward = Vector3.Slerp(transform.position, movDir, Time.deltaTime * rotationSpeed);

   }

  private void SetSelectedCounter(ClearCounter selectedCounter)
  {
    this.selectedCounter = selectedCounter;

    OnSelectedCounterChanged?.Invoke(this, new OnSelectCounterChangedEventArgs{selectedCounter = selectedCounter});   //the first selectedCounter variable is of Args while the second one is of Player script
  }
   }


