using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    List<Vector2> lastMousePos = new List<Vector2>();
    float timer = 0f;
    void Start()
    {       }
    void Update()
    {
       if (Mouse.current.leftButton.isPressed == true)
        {
            lastMousePos.Add((Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
            timer += 0.1f * Time.deltaTime;
            int last = lastMousePos.Count;

            if (timer == 0.1f && Mouse.current.leftButton.isPressed == true)
            {
                timer = 0f;
                Debug.DrawLine(lastMousePos[last], (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())), Color.red);
            }
        }
    }
}
