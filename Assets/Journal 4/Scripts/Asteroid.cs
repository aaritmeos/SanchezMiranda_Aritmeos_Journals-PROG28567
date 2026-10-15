using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //public float moveSpeed;
    public Vector3 movement; //variable to check the direction in which the asteroid should move
    private int direction; //variable to select a random vector for movement
    Vector3 bottomfLeft; //variable that holds the bottom and left edges of the screen
    Vector3 topRight; //variable that holds the top and right edges of the screen

    //public float arrivalDistance;
    //public float maxFloatDistance;

    //private float xPos;
    //private float yPos;
    //private Vector3 target;
    void Start()
    {
        //xPos = Random.Range(transform.position.x, transform.position.x + maxFloatDistance);
        //yPos = Random.Range(transform.position.y, transform.position.y + maxFloatDistance);
        //target = new Vector3(xPos, yPos, 0);
        bottomfLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)); //assign bottom and left edges to the variable
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); //assign top and right edges to the variable
        direction = Random.Range(0, 4); //make direction a number from 0 to 3
        //assign a vector3 to movement depending on the random number selected
        if (direction == 0)
        {
            movement = new Vector3(2, 1);
        }
        if (direction == 1)
        {
            movement = new Vector3(-2, 1);
        }
        if (direction == 2)
        {
            movement = new Vector3(1, 2);
        }
        if (direction == 3)
        {
            movement = new Vector3(1, -2);
        }
    }
    void Update()
    {
        AsteroidMovement();
    }
    public void AsteroidMovement()
    {
        //float magnitude = Vector3.Magnitude(target - transform.position);
        //if (magnitude < arrivalDistance == true) ;
        //{
        //    xPos = Random.Range(transform.position.x, transform.position.x + maxFloatDistance);
        //    yPos = Random.Range(transform.position.y, transform.position.y + maxFloatDistance);
        //    target = new Vector3(xPos, yPos, 0);
        //}
        Vector3 newPos = transform.position; //vector to hold the asteroids position to modify it

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position); //vector to see where the asteroid is compared to the camera
        if (screenPos.x < bottomfLeft.x) //if the asteroid passes the left edge
        {
            newPos.x = bottomfLeft.x; //put the asteroid on the left edge
            direction = Random.Range(0, 4);//make direction a number from 0 to 3
            //assign a vector3 to movement depending on the random number selected
            if (direction == 0)
            {
                movement = new Vector3(2, 1);
            }
            if (direction == 1)
            {
                movement = new Vector3(-2, 1);
            }
            if (direction == 2)
            {
                movement = new Vector3(1, 2);
            }
            if (direction == 3)
            {
                movement = new Vector3(1, -2);
            }
        }
        if (screenPos.y < bottomfLeft.y) //if the asteroid passes the lower edge
        {
            newPos.y = bottomfLeft.y; //put the asteroid on the lower edge
            direction = Random.Range(0, 4);//make direction a number from 0 to 3
            //assign a vector3 to movement depending on the random number selected
            if (direction == 0)
            {
                movement = new Vector3(2, 1);
            }
            if (direction == 1)
            {
                movement = new Vector3(-2, 1);
            }
            if (direction == 2)
            {
                movement = new Vector3(1, 2);
            }
            if (direction == 3)
            {
                movement = new Vector3(1, -2);
            }
        }
        if (screenPos.x > Screen.width) //if the asteroid passes the right edge
        {
            newPos.x = topRight.x;//put the asteroid on the right edge
            direction = Random.Range(0, 4);//make direction a number from 0 to 3
            //assign a vector3 to movement depending on the random number selected
            if (direction == 0)
            {
                movement = new Vector3(2, 1);
            }
            if (direction == 1)
            {
                movement = new Vector3(-2, 1);
            }
            if (direction == 2)
            {
                movement = new Vector3(1, 2);
            }
            if (direction == 3)
            {
                movement = new Vector3(1, -2);
            }
        }
        if (screenPos.y > Screen.height) //if the asteroid passes the higher edge
        {
            newPos.y = topRight.y; //put the asteroid on the higher edge
            direction = Random.Range(0, 4);//make direction a number from 0 to 3
            //assign a vector3 to movement depending on the random number selected
            if (direction == 0)
            {
                movement = new Vector3(2, 1);
            }
            if (direction == 1)
            {
                movement = new Vector3(-2, 1);
            }
            if (direction == 2)
            {
                movement = new Vector3(1, 2);
            }
            if (direction == 3)
            {
                movement = new Vector3(1, -2);
            }
        }
        transform.position = newPos += movement.normalized * Time.deltaTime; //make the asteroid's position equal to newPos modified by movemebt.normalized  by Time.deltaTime
    }
}
