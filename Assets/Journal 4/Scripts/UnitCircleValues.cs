using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCircleValues : MonoBehaviour
{
    public List<float> angles = new();
    public Vector3 startPoint = Vector3.zero;
    public float duration = 1f;

    private int currentIndex = 0;
    private float elapsedTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            angles.Add(randomAngle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > duration)
        {
            currentIndex = (currentIndex + 1) % angles.Count;
            elapsedTime = 0f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentIndex = (currentIndex + 1) % angles.Count;
        }
        float angle = angles[currentIndex];
        float angleInRads = angle * Mathf.Rad2Deg;
        float xPos = Mathf.Cos(angleInRads);
        float yPos = Mathf.Sin(angleInRads);

        Vector3 offset = new Vector3(xPos, yPos, 0);

        Debug.DrawLine(startPoint, startPoint + offset, Color.white);
    }
}
