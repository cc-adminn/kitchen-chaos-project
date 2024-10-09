using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] float playerSpeed;
  [SerializeField] float playerRotation;
  [SerializeField] GameInput gameInput;
   private void Update()
   {
    Vector2 inputVector2FromGameInput = gameInput.GetMovementVector2Normalized();
    Vector3 movDir = new Vector3(inputVector2FromGameInput.x, 0, inputVector2FromGameInput.y);
    
    transform.position += movDir * playerSpeed * Time.deltaTime;
    
    transform.forward = Vector3.Slerp(transform.forward, movDir , Time.deltaTime * playerRotation);       //this line is for the rotation of the body when its moving and rotating it on the same direction where moving
   }
}
