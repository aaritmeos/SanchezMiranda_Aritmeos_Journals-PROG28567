using UnityEngine;
using UnityEngine.InputSystem;

public class DotProduct : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;
    
    void Update()
    {
        Vector3 redVector = ComputeVectorFromAngle(redAngle);
        Vector3 blueVector = ComputeVectorFromAngle(blueAngle);

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            float dot = ComputeDotProduct(redVector, blueVector)   ;
            Debug.Log("The dot product is " +  dot);
        }
    }
    private Vector2 ComputeVectorFromAngle (float angle)
    {
        float angleInRads = Mathf.Deg2Rad * angle;
        float xCoord = Mathf.Cos(angleInRads);
        float yCoord = Mathf.Sin(angleInRads);

        return new Vector3(xCoord, yCoord);
    }

    private float ComputeDotProduct(Vector3 a, Vector3 b)
    {
        float dot = a.x * b.x + a.y * b.y + a.z * b.z;
        return dot;
    }
}
