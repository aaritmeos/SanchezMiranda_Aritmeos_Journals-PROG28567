using UnityEngine;

public class Turret : MonoBehaviour
{
    [Tooltip("Measured in Degrees per second.")]
    public float angularSpeed;
    public Transform target;
    
    void Update()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        /*transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);

        float dot = Vector3.Dot(transform.up, directionToTarget);

        if(dot >= 0)
        {
            Debug.Log("In Front");
        }
        else
        {
            Debug.Log("Behind");
        }*/

        #region Turret look-at excercise
        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float directionAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle);

        Debug.Log(deltaAngle);

        float dot = Vector3.Dot(transform.up, directionToTarget);

        if (dot < 0.98f)
        {
            switch (Mathf.Sign(deltaAngle))
            {
                case 1:
                    transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
                    break;
                case -1:
                    transform.Rotate(0, 0, -angularSpeed * Time.deltaTime);
                    break;
            }
        }
        #endregion
    }
}
