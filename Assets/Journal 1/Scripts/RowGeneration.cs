using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowGeneration : MonoBehaviour
{
    //integers to be able to move the sqaures
    public int startPointA = 1;
    public int startPointB = -1;
    //variable to grab the text from the input field
    public TMP_InputField squareNumber;
    void Start()
    {}
    void Update()
    {}
    //function to create a row of squares upon hitting the generate button
    public void Generate ()
    {
        //convert input to string
        string squNum = squareNumber.text;
        //variable to convert string to integer
        int num;
        //check if string can be Parsed
        bool result = int.TryParse(squNum, out num);
        //if it can't be Parsed, send invalid message to the console
        if (result == false)
        {
            Debug.Log("invalid");
        }
        //if it can be Parsed, use the new integer as limit for the for loop
        if (result == true)
        {
            //loop to start and stop the generation of squares, uses the int from above as limit
            for (int i = 0; i < num; i++)
            {
                //vectors to create the squares
                Vector2 firstQuad = new Vector2(startPointA, 1);
                Vector2 secondQuad = new Vector2(startPointA, -1);
                Vector2 thirdQuad = new Vector2(startPointB, -1);
                Vector2 fourthQuad = new Vector2(startPointB, 1);
                //draw the lines that create the squares
                Debug.DrawLine(firstQuad, secondQuad, Color.white, 10f);
                Debug.DrawLine(secondQuad, thirdQuad, Color.white, 10f);
                Debug.DrawLine(thirdQuad, fourthQuad, Color.white, 10f);
                Debug.DrawLine(fourthQuad, firstQuad, Color.white, 10f);
                startPointA += 2;
                startPointB += 2;
            }
        }
    }
}
