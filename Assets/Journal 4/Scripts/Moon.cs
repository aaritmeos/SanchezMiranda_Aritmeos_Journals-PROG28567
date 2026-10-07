using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float radius; //distance from planet to the moon
    public float speed; //orbiting speed
    public float circumference; //value toknow at which point of the orbit the moon is
    void Update()
    {
      OrbitalMotion(radius, speed, planetTransform); //always run OrbitalMotion, radius, speed, and planetTransform as arguments
    }
    public void OrbitalMotion(float inRadius, float inSpeed, Transform target) //method to rotate moon around the planet
    {
        circumference += Time.deltaTime * inSpeed; //increase value of circumference each frame
        if (circumference > 360f) //reset circumference if it gets higher than w360
        {
            circumference = 0f;
        }
        float xPos = (Mathf.Cos(circumference) * inRadius) + target.position.x; //make xPos the cosine of circumference by radius plus the planet's x position
        float yPos = (Mathf.Sin(circumference) * inRadius) + target.position.y; //make y pos the sine of circumference by radius plus the planet's y position
        Vector3 newPos = new Vector3(xPos, yPos, 0); //use xPos and yPos as arguments of newPos

        transform.position = newPos; //make newPos the moon's position
    }
}
