using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            //SpawnBombAtOffset(Vector3.down);
            StartCoroutine(SpawnBombAtOffset(Vector3.down));
        }
    }

    IEnumerator SpawnBombAtOffset(Vector3 inOffset)
    {
        yield return new WaitForSeconds(3);
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }
}
