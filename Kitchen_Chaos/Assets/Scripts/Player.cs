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
  [SerializeField] float bodyHeight = 2f;
  [SerializeField] float bodyRadius = 0.7f;
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
    float movDistance = speed * Time.deltaTime;                  //this is a way to calculate distance in a single frame e.g 7*0.01 = 0.07 in single frame and if there are 100 frames running in a second then in one second player will move 0.07 * 100 = 7
    Vector2 inputVector = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);
    transform.position += movDir * movDistance;
    
    
    
    

    

    //for player face rotation
    float rotationSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotationSpeed);

   }

   }


