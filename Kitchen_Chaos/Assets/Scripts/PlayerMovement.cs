using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float speed;
  [SerializeField] GameInput gameInput;
   
   void Update()
   {
    Vector2 inputVector = gameInput.MovementVector2Normalized();

    float moveDistance  = speed * Time.deltaTime;

    //we made a new Vector3 and assigned vector2(inputVector2) x value to its x and its y value to vector3's z value
    Vector3 movDir = new Vector3(inputVector.x , 0f, inputVector.y);
    float bodyRadius = .7f;
    bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2 , bodyRadius , movDir , moveDistance );

    if (!canMove)
    {
      //cannot mov toward MovDir

      //attemp only X movement
      Vector3 movDirX = new Vector3(movDir.x, 0 , 0);
      canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2 , bodyRadius , movDirX , moveDistance );
         if (canMove)       
         {
          //can only move in x axis
          movDir = movDirX;
         }
         else
         {
          //cannot move only on x axis

          //attempt only z axis 
          Vector3 movDirZ = new Vector3(0, 0, movDir.z);
          canMove = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2 , bodyRadius, movDir, moveDistance);

          if (canMove)       
         {
          //can only move in z axis
          movDir = movDirZ;
         }
         else
         {
          //we cannot move in any direction
         }
         }
    }

    if (canMove)
    {
      transform.position += movDir *moveDistance ;
    }
    


    
    
    float rotationSpeed = 15f;
    //for player face rotation
    transform.forward = Vector3.Slerp(transform.position, movDir, Time.deltaTime * rotationSpeed);

   }

}
