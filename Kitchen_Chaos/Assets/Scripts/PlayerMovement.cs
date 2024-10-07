using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float speed;
  [SerializeField] GameInput gameInput;
   
   void Update()
   {
    Vector2 inputVector = gameInput.MovementVector2Normalized();

    //we made a new Vector3 and assigned vector2(inputVector2) x value to its x and its y value to vector3's z value
    Vector3 movDir = new Vector3(inputVector.x , 0f, inputVector.y);
    transform.position += movDir *speed * Time.deltaTime;
    
    float rotationSpeed = 15f;
    //for player face rotation
    transform.forward = Vector3.Slerp(transform.position, movDir, Time.deltaTime * rotationSpeed);

   }

}
