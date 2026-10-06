using UnityEngine;

public class Turret : MonoBehaviour
{
    [Tooltip("Measured in Degrees per second.")]
    public float angularSpeed;
    public Transform target;
    
    void Update()
    {
        transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        Vector3 directionToTarget = (target.position - transform.position).normalized;

        float dot = Vector3.Dot(transform.up, directionToTarget);

        if(dot >= 0)
        {
            Debug.Log("In Front");
        }
        else
        {
            Debug.Log("Behind");
        }
    }
}
