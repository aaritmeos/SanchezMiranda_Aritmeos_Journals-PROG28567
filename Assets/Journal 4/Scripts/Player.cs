using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public Vector2 bombOffset; //Public Vector to modify how far is the bomb from the Player
    public float bombTrailSpacing; //public float to determine space between bombs
    public int numberOfTrailBombs; //public int to determine number of bombs
    public float cornerDistance; //public float to determine the distance from Player to corner where bomb spawns
    public float warpRatio; //ratio for the player to warp between itself and an object
    public float maxRange; //how close to the player does an asteroid need to be to be detected
    public float maxSpeed; //max speed the player can reach
    public float accelerationTime; //how long does it take for the player to reach max speed
    public float decelerationTime; //how long does it take for the player to go back to 0 speed
    public float radarRadius = 3f; //distance from the player's transform to the limit of its radar
    public int radarSideCount = 8; //sides of the radar
    public Color radarColor; //color of the radar
    private float acceleration; //how much speed does the player gain per second
    private float deceleration; //how much speed does the player lose per second
    private Vector3 velocity; //player's speed

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime; //calculate acceleration
        deceleration = maxSpeed / decelerationTime; //calculate deceleration       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(); //always run Player Movement
        PlayerRadar (radarRadius, radarSideCount); //always run PlayerRadar, radarRadius and radarSideCount as arguments

        if (Keyboard.current.bKey.wasPressedThisFrame) //run SpawnBombAtOffset Coroutine when B is pressed
        {
            StartCoroutine(SpawnBombAtOffset(bombOffset)); //bombOffset Vector as argument
        }
        if (Keyboard.current.tKey.wasPressedThisFrame) //run SpawnBombTrail when T is pressed
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs); //bombTrailSpacing and numberOfTrailBombs as arguments
        }
        if (Keyboard.current.rKey.wasPressedThisFrame) //run SpawnBombOnRandomCorner when R is pressed
        {
            SpawnBombOnRandomCorner(cornerDistance); //cornerDistance as argument
        }
        if (Keyboard.current.wKey.wasPressedThisFrame) //run WarpPlayer when W is pressed
        {
            WarpPlayer(enemyTransform, warpRatio); //enemyTransform and warpRatio as arguments
        }
        DetectAsteroids(maxRange, asteroidTransforms); //always run DetectAsteroids, maxRange and asteroidTransforms as arguments
    }

    IEnumerator SpawnBombAtOffset(Vector3 inOffset) //Coroutine that spawns a bomb at a distance from the player
    {
        yield return new WaitForSeconds(3); //wait 3 seconds, then Instantiate
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity); //use inOffset to distance bomb from the player
    }
    public Vector2 Normie(Vector2 weirdo) //normalize a vector
    {
        return weirdo.normalized;
    }

    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs) //method to spawn bomb trail at a distance from the player
    {
        float spacing = inBombSpacing; //float that determines the distance between bombs
        for (int i = 0; i < inNumberOfBombs; i++) //loop uses inNumberOfBombs as limit
        {
            //Use spacing to distance bombs from player and themselves
            Instantiate(bombPrefab, transform.position + new Vector3(0, spacing, 0), Quaternion.identity);
            spacing = spacing + inBombSpacing; //add inBombSpacing to spacing each time the loop runs to keep consistent separation
        }
    }
    public void SpawnBombOnRandomCorner(float inDistance) //method to spawn bomb at a random corner of the Player
    {
        float corner = Random.Range(0, 4);//get a random number from 0 to 3

        if (corner == 0)//if corner = 0, instantiate bomb at top right corner
        {
            Instantiate(bombPrefab, transform.position + new Vector3(inDistance, inDistance, 0), Quaternion.identity);
        }
        else if (corner == 1)//if corner = 1, instantiate bomb at top left corner
        {
            Instantiate(bombPrefab, transform.position + new Vector3(-inDistance, inDistance, 0), Quaternion.identity);
        }
        else if (corner == 2)//if corner = 0, instantiate bomb at bottom right corner
        {
            Instantiate(bombPrefab, transform.position + new Vector3(inDistance, -inDistance, 0), Quaternion.identity);
        }
        else if (corner == 3)//if corner = 0, instantiate bomb at bottom corner
        {
            Instantiate(bombPrefab, transform.position + new Vector3(-inDistance, -inDistance, 0), Quaternion.identity);
        }
    }
    public void WarpPlayer(Transform target, float ratio) //method to teleport the player a proportional distance between itself and the target
    {
        Vector3 direction = target.position - transform.position; //calculate distance between player and target
        if (ratio <= 1) //move player only if value of ratio is 1 or less
        {
            transform.position += direction * ratio; //formula to move player
        }
        else if (ratio > 1) //if ratio is greater than 1, don't do anything
        {

        }
    }
    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids) //method to check for asteroids nearby the player
    {
        foreach (Transform asteroidTransforms in inAsteroids) //check every asteroid transform in the player's list
        {
            if (Vector3.Distance(transform.position, asteroidTransforms.position) < inMaxRange) //run code if one of the asteroids is close to the player
            {
                Vector2 normalDistance = Normie(asteroidTransforms.position - transform.position) * 2.5f; //calculate and normalize the distance between the asteroid and the player
                Debug.DrawLine(transform.position, transform.position + (Vector3)normalDistance, Color.green); //draw a line from the player to the asteroid
            }
        }
    }
    public void PlayerMovement() //method to move player with arrow keys
    {

        if (Keyboard.current.upArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.up; //move up when up arrow is pressed
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.down; //move down when down arrow is pressed
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.right; //move right when right arrow is pressed
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.left; //move left when left arrow is pressed
        }
        else
        {
            velocity -= deceleration * Time.deltaTime * velocity.normalized; //deccelerate when no key is prossed
        }
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed); //limit max speed
        
        transform.position += velocity * Time.deltaTime; //apply changes to transform
    }
    private void PlayerRadar(float radius, int numberOfSides) //method to detect when the enemy is nearby the player
    {
        float stepAngle = 360.0f / numberOfSides; //get the division to determine length of sides of radar
        List<Vector3> points = new(); //list to store vertices of radar

        stepAngle *= Mathf.Deg2Rad; //transform to radians
        float currentAngle = stepAngle; //make the current angle the step angle

        Vector3 disEnemToPlay = enemyTransform.position - transform.position; //calculate distance from the enemy to the player

        for (int i = 0; i < numberOfSides; i++) //run loop for each side in number of sides
        {
            float xPos = Mathf.Cos(currentAngle) * radius; //get Cosine of current angle and use it as x coordinate
            float yPos = Mathf.Sin(currentAngle) * radius; //get Sine of current angle and use it as y coordinate

            Vector3 newPoint = new Vector3(xPos, yPos, 0); //Assign xPos and yPos to newPoints
            points.Add(newPoint); //add newPoint to points list

            currentAngle += stepAngle; //increase current angle by step anglee
        }
        for (int i = 0;i < numberOfSides - 1; i++) //run loop for number of sides minus 1
        {
            if (disEnemToPlay.magnitude < radius) //if the distance from the enemy to the player is less than the radius
            {
                radarColor = Color.red; //make radar Color red
            }
            else //in any other case
            {
                radarColor = Color.green; //make radar Color green
            }
            Vector3 startPoint = transform.position + points[i]; //Make startPoint the player's position plus i instance in points
            Vector3 endPoint = transform.position + points[i + 1]; //Make endPoint the player's position plus i + 1 instance in points

            Debug.DrawLine(startPoint, endPoint, radarColor); //draw a line from startPoint to endPoint using radarColor
            if (i == numberOfSides - 2) //if i is number of Sides - 2
            {
                startPoint = transform.position + points[i + 1]; //Make startPoint the player's position plus i + 1 instance in points
                endPoint = transform.position + points[0]; //Make endPoint the player's position plus 0 instance in points

                Debug.DrawLine(startPoint, endPoint, radarColor); //draw a line from startPoint to endPoint using radarColor
            }
        }
    }
}
