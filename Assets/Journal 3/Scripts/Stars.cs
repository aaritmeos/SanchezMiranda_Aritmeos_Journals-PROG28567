using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private int currentIndex = 0;
    private float elapsedTime = 0f;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        for(int i = 0; i < starTransforms.Count + 1; i++)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > drawingTime)
            {
                currentIndex = (currentIndex + 1) % starTransforms.Count;
                elapsedTime = 0f;
            }
            startPosition = starTransforms[currentIndex].position;
            endPosition = starTransforms[currentIndex + 1].position;
            currentPosition = endPosition - startPosition;
            float speed = currentPosition.magnitude / drawingTime;
            Debug.DrawLine(startPosition, endPosition);
        }
    }
}
