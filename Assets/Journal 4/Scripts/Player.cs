using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    #region PlayerMovement variables
    public float maxSpeed; //max speed the player can reach
    private float acceleration; //how much speed does the player gain per second
    private float deceleration; //how much speed does the player lose per second
    private Vector3 velocity; //player's speed
    public float accelerationTime; //how long does it take for the player to reach max speed
    public float decelerationTime; //how long does it take for the player to go back to 0 speed
    #endregion

    #region WarpPlayer variables
    public Transform enemyTransform;
    public float warpRatio; //ratio for the player to warp between itself and an object
    #endregion

    #region PlayerRadar variables
    public float radarRadius = 3f; //distance from the player's transform to the limit of its radar
    public int radarSideCount = 8; //sides of the radar
    public Color radarColor; //color of the radar
    #endregion

    #region DetectAsteroids variables
    public float maxRange; //how close to the player does an asteroid need to be to be detected
    public List<Transform> asteroidTransforms;
    #endregion

    #region SpawnPowerups variables
    public GameObject powerupPrefab;
    public float powerUpRadius; //diastance of the power ups from the player
    public int numberOfPowerups; //number of powerups to instantiate
    #endregion

    #region SpawnBombAtOffest variables
    public GameObject bombPrefab;
    public Vector2 bombOffset; //Public Vector to modify how far is the bomb from the Player
    #endregion

    #region SpawnBombTrail variables
    public float bombTrailSpacing; //public float to determine space between bombs
    public int numberOfTrailBombs; //public int to determine number of bombs
    #endregion

    #region SpawnBombOnRandomCorner
    public float cornerDistance; //public float to determine the distance from Player to corner where bomb spawns
    #endregion

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime; //calculate acceleration
        deceleration = maxSpeed / decelerationTime; //calculate deceleration       
    }

    // Update is called once per frame
    void Update()
    {
        #region Movemente
        PlayerMovement(); //always run Player Movement
        WarpPlayer(enemyTransform, warpRatio); //always run WarpPlayer, enemyTransform and warpRatio as argument
        #endregion

        #region Radars
        PlayerRadar(radarRadius, radarSideCount); //always run PlayerRadar, radarRadius and radarSideCount as arguments
        DetectAsteroids(maxRange, asteroidTransforms); //always run DetectAsteroids, maxRange and asteroidTransforms as arguments
        #endregion

        #region PowerUps
        SpawnPowerups(powerUpRadius, numberOfPowerups); //always run SpawnPowerups, with powerUpRadius and numberOfPowerUps as arguments
        #endregion

        #region Bombs
        StartCoroutine(SpawnBombAtOffset(bombOffset)); //always run SpawnBombAtOffset, bombOffset Vector as argument
        SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs); //always run SpawnBombTrail, bombTrailSpacing and numberOfTrailBombs as arguments        
        SpawnBombOnRandomCorner(cornerDistance); //always run SpawnBombOnRandomCorner, cornerDistance as argument
        #endregion
    }
    #region Movement
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
    public void WarpPlayer(Transform target, float ratio) //method to teleport the player a proportional distance between itself and the target
    {
        if (Keyboard.current.wKey.wasPressedThisFrame) //run if w key was pressed
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
    }
    #endregion

    #region Radars
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
        for (int i = 0; i < numberOfSides - 1; i++) //run loop for number of sides minus 1
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
    #endregion

    #region PowerUps
    public void SpawnPowerups(float radius, int inNumberOfPowerups) //method to spawn power ups
    {
        if (Keyboard.current.pKey.wasPressedThisFrame) //run when p key was pressed
        {
            float stepAngle = 360f / inNumberOfPowerups; //divide 360 by number of power ups to know the angle between power ups
            stepAngle *= Mathf.Deg2Rad; //transform step angle to radians
            float currentAngle = stepAngle; //make current angle same value as step angle

            for (int i = 0; i < inNumberOfPowerups; i++) //run a number of times equal to number of power ups
            {
                float xPos = Mathf.Cos(currentAngle) * radius; //make x pos the cosine of current angle by radius
                float yPos = Mathf.Sin(currentAngle) * radius; //make y pos the sine of current angle by radius
                Vector3 newPos = new Vector3(xPos, yPos, 0); //use xPos and yPos as arguments of newPos
                Instantiate(powerupPrefab, transform.position + newPos, Quaternion.identity); //spawn PowerUp using newPos as its offset

                currentAngle += stepAngle; //increment currentAngle by stepAngle
            }
        }
    }
    #endregion

    #region Bombs
    IEnumerator SpawnBombAtOffset(Vector3 inOffset) //Coroutine that spawns a bomb at a distance from the player
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) //run when b Key was pressed
        {
            yield return new WaitForSeconds(3); //wait 3 seconds, then Instantiate
            Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity); //use inOffset to distance bomb from the player
        }
    }
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs) //method to spawn bomb trail at a distance from the player
    {
        if (Keyboard.current.tKey.wasPressedThisFrame) //run when tKey was pressed
        {
            float spacing = inBombSpacing; //float that determines the distance between bombs
            for (int i = 0; i < inNumberOfBombs; i++) //loop uses inNumberOfBombs as limit
            {
                //Use spacing to distance bombs from player and themselves
                Instantiate(bombPrefab, transform.position + new Vector3(0, spacing, 0), Quaternion.identity);
                spacing = spacing + inBombSpacing; //add inBombSpacing to spacing each time the loop runs to keep consistent separation
            }
        }
    }
    public void SpawnBombOnRandomCorner(float inDistance) //method to spawn bomb at a random corner of the Player
    {
        if (Keyboard.current.rKey.wasPressedThisFrame) //run when r Key was pressed
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
    }
    #endregion

    #region Misc
    public Vector2 Normie(Vector2 weirdo) //normalize a vector
    {
        return weirdo.normalized; //normalize given vector
    }
    #endregion
}
