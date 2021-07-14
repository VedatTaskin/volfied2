using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridBuilding : MonoBehaviour
{
    private int width;
    private int depth;
    private int[,] gridArray;
    private float cellSize;


    // first we build korner of our platform
    public GridBuilding(int width, int depth, float cellSize)   // Constructor
    {
        this.width = width;
        this.depth = depth;
        this.cellSize = cellSize;
        gridArray = new int[width, depth];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                GameObject GO = Instantiate(Resources.Load("CornerGO") as GameObject, new Vector3(x, 0, z) * cellSize, Quaternion.identity);         
                GO.name = "Corner" + x + z;

                //number of squares are less then (-1) the corners,
                if (x<width-1 && z<depth-1)
                {
                    CreateSquare(x, z);
                    Debug.Log(x +""+ z);
                }
            }
        }

    }



    //we make squares according to our corner points
    void CreateSquare(int x, int z)
    {
        GameObject squareGO = Instantiate(Resources.Load("SquareGO") as GameObject,
                         new Vector3(x, 0, z) * cellSize + new Vector3(cellSize, 0, cellSize) * 0.5f ,
                           Quaternion.identity);

        squareGO.transform.localScale = Vector3.one * 0.1f * (cellSize - 0.5f);
        squareGO.name = "Plane" + x + z;
    }
}
