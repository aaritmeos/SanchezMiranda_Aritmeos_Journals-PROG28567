using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    public int posOne = 1;
    public int negOne = -1;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector that stores the state of the scrolling wheel
        Vector2 changeSize = Mouse.current.scroll.ReadValue();

        //modify variables when using the scroll wheel
        if(changeSize.y > 0)
        {
            posOne++;
            negOne--;
        } else if (changeSize.y < 0)
        {
            posOne--;
            negOne++;
        }

        //Four Vectors to add to mousePos
        Vector2 firstQuad = new Vector2(posOne, posOne);
        Vector2 secondQuad = new Vector2(posOne, negOne);
        Vector2 thirdQuad = new Vector2(negOne, negOne);
        Vector2 fourthQuad = new Vector2(negOne, posOne);

        //Turn mouse position into a Vector
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //draw a sqaure always at the mouse position, square obscures when over objects closer to the camera
        Debug.DrawLine((mousePos + firstQuad), (mousePos + secondQuad), Color.white, 0, true);
        Debug.DrawLine((mousePos + secondQuad), (mousePos + thirdQuad), Color.white, 0, true);
        Debug.DrawLine((mousePos + thirdQuad), (mousePos + fourthQuad), Color.white, 0, true);
        Debug.DrawLine((mousePos + fourthQuad), (mousePos + firstQuad), Color.white, 0, true);

        //make a white square spawn at the mouse position if the left button of the mouse was pressed
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.DrawLine((mousePos + firstQuad), (mousePos + secondQuad), Color.white, 5f);
            Debug.DrawLine((mousePos + secondQuad), (mousePos + thirdQuad), Color.white, 5f);
            Debug.DrawLine((mousePos + thirdQuad), (mousePos + fourthQuad), Color.white, 5f);
            Debug.DrawLine((mousePos + fourthQuad), (mousePos + firstQuad), Color.white, 5f);
        }
    }
}
