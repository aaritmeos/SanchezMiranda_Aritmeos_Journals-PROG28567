using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowGeneration : MonoBehaviour
{
    public int startPointA = 1;
    public int startPointB = -1;
    public TMP_InputField squareNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    //function to create a row of squares upon hitting the generate button
    public void Generate ()
    {
        string squNum = squareNumber.text;
        int num = int.Parse(squNum);
        //loop to start and stop the generation of squares
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
