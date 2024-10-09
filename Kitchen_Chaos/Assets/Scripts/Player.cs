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
   [SerializeField] float speed;
  [SerializeField] GameInput gameInput;
  [SerializeField] LayerMask layerMaskForCounter;
  private Vector3 lastInteractDirection;
   
   void Awake()
   {
      
   }
   
   void Start()
   {
   
   }


   void Update()
   {
    HandleMovement();
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

   }


