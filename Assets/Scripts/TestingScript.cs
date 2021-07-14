using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public int width = 6;
    public int depth = 8;
    public float cellSize = 5f;
    // Start is called before the first frame update
    void Start()
    {
        GridBuilding grid = new GridBuilding(width,depth,cellSize);        
    }

}
