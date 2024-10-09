using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float playerSpeed;
  [SerializeField] float playerRotation;
  [SerializeField] GameInput gameInput;
  [SerializeField] LayerMask layerMaskForCounters;
  float bodyHeight = 2f;
  float bodyRadius = 0.7f;
   private void Update()
   {
    Vector2 inputVector2FromGameInput = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector2FromGameInput.x, 0, inputVector2FromGameInput.y);
    Vector3 movDirX = new Vector3(movDir.x, 0, 0);
    Vector3 movDirZ = new Vector3(0, 0, movDir.z);
    
    Vector3 endPoint = transform.position + Vector3.up * bodyHeight;                            //position of this gameobject is its base while we move up from base 2 units to reach its height
    bool canMove = !Physics.CapsuleCast(transform.position, endPoint, bodyRadius, movDir, playerSpeed * Time.deltaTime, layerMaskForCounters);
    
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
