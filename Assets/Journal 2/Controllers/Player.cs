using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public Vector2 bombOffset; //Public Vector to modify how far is the bomb from the Player
    public float bombTrailSpacing; //public float to determine space between bombs
    public int numberOfTrailBombs; //public int to determine number of bombs

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) //run SpawnBombAtOffset Coroutine when B is pressed
        {
            StartCoroutine(SpawnBombAtOffset(bombOffset)); //bombOffset Vector as argument
        }
        if (Keyboard.current.tKey.wasPressedThisFrame) //run SpawnBombTrail when T is pressed
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs); //bombTrailSpacing and numberOfTrailBombs as arguments
        }
    }

    IEnumerator SpawnBombAtOffset(Vector3 inOffset) //Coroutine that spawns a bomb at a distance from the player
    {
        yield return new WaitForSeconds(3); //wait 3 seconds, then Instantiate
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity); //use inOffset to distance bomb from the player
    }

    public void SpawnBombTrail (float inBombSpacing, int inNumberOfBombs) //method to spawn bomb trail at a distance from the player
    {
        float spacing = inBombSpacing; //float that determines the distance between bombs
        for (int i = 0; i < inNumberOfBombs; i++) //loop uses inNumberOfBombs as limit
        {
            //Use spacing to distance bombs from player and themselves
            Instantiate(bombPrefab, transform.position + new Vector3 (0, spacing, 0), Quaternion.identity); 
            spacing = spacing + inBombSpacing; //add inBombSpacing to spacing each time the loop runs to keep consistent separation
        }
    }
}
