using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
    public float angularSpeed; //speed at which the Enemy rotates
    
    private void Update()
    {
        EnemyMovement(playerTransform, speed);
        LookAtPlayer();
    }
    public void EnemyMovement(Transform target, float inSpeed)
    {
        Vector3 direction = target.position - transform.position;
        transform.position += inSpeed * Time.deltaTime * direction.normalized;
    }
    public void LookAtPlayer()
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized; //calculate the direction to the player
        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg; //calculate the angle above the  Enemy and turn it to degrees
        float directionAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg; //calculate the angle of the player's position and turn it to degrees
        float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle); //get the delta of upAngle and directionAngle

        float dot = Vector3.Dot(transform.up, directionToPlayer); //get the dot product of the position above the Enemy and the players position

        if (dot < 0.9f) //if dot is less than 0.9
        {
            switch (Mathf.Sign(deltaAngle)) //return -1 if deltaAngle is negative, or 1 if it's positive or 0
            {
                case 1: //if deltaAngle is positive or 0
                    transform.Rotate(0, 0, angularSpeed  * Time.deltaTime); //rotate the Enemy counterclockwise towards the player
                    break;
                case -1: //if deltaAngle is negative
                    transform.Rotate(0, 0, -angularSpeed * Time.deltaTime); // rotate the Enemy clockwise towards the player
                    break;
            }
        }
    }
}
