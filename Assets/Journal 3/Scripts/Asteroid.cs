using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    private float xPos;
    private float yPos;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        xPos = Random.Range(transform.position.x, transform.position.x + maxFloatDistance);
        yPos = Random.Range(transform.position.y, transform.position.y + maxFloatDistance);
        target = new Vector3(xPos, yPos, 0);
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }
    public void AsteroidMovement()
    {
        float magnitude = Vector3.Magnitude(target - transform.position);
        if (magnitude < arrivalDistance);
        {
            xPos = Random.Range(transform.position.x, transform.position.x + maxFloatDistance);
            yPos = Random.Range(transform.position.y, transform.position.y + maxFloatDistance);
            target = new Vector3(xPos, yPos, 0);
        }
        transform.position += moveSpeed * Time.deltaTime * target.normalized;
    }
}
