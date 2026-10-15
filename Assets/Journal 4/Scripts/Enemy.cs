using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
    
    private void Update()
    {
        EnemyMovement(playerTransform, speed);
    }
    public void EnemyMovement(Transform target, float inSpeed)
    {
        Vector3 direction = target.position - transform.position;
        transform.position += inSpeed * Time.deltaTime * direction.normalized;
    }
}
