using System;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [SerializeField] Transform[] points, cubes;

    [SerializeField] int[] pointsData;

    [SerializeField] int width = 4, lenght = 4;

    [SerializeField] Transform player;

    [SerializeField] Material defaultCubeMaterial, coveredAreaMaterial, tempMaterial;

    Vector2 playerCurrentPoint;

    List<int> sequence;

    void Start()
    {
        InitialProcesses();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movementVector = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W))
            movementVector.y++;
        else if (Input.GetKeyDown(KeyCode.S))
            movementVector.y--;
        else if (Input.GetKeyDown(KeyCode.A))
            movementVector.x--;
        else if (Input.GetKeyDown(KeyCode.D))
            movementVector.x++;

        if (playerCurrentPoint.x + movementVector.x < 0)
            movementVector.x = 0;
        else if (playerCurrentPoint.x + movementVector.x > width)
            movementVector.x = 0;

        if (playerCurrentPoint.y + movementVector.y < 0)
            movementVector.y = 0;
        else if (playerCurrentPoint.y + movementVector.y > lenght)
            movementVector.y = 0;

        if (pointsData[(int)((playerCurrentPoint + movementVector).x + (playerCurrentPoint + movementVector).y * (width + 1))] == 2)
            movementVector = Vector2.zero;
        else
        {

        }

        playerCurrentPoint += movementVector;
        int index = (int)(playerCurrentPoint.x + playerCurrentPoint.y * (width + 1));
        player.position = points[index].position;

        if (pointsData[index] == 0)
        {
            pointsData[index] = 2;
            points[index].gameObject.GetComponent<MeshRenderer>().material = tempMaterial;

            sequence.Add(index);
        }
        else if (pointsData[index] == 1)
        {
            PaintIntervalArea();

            for (; sequence.Count > 0;)
            {
                points[sequence[0]].gameObject.GetComponent<MeshRenderer>().material = coveredAreaMaterial;
                sequence.RemoveAt(0);
            }
        }
        else
        {
            if(index == sequence[sequence.Count - 2])
            {
                points[sequence[sequence.Count - 1]].gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                pointsData[sequence[sequence.Count - 1]] = 0;
                sequence.RemoveAt(sequence.Count - 1);
            }
        }
    }

    private void PaintIntervalArea()
    {
        //Do paint
    }

    private void InitialProcesses()
    {
        //Paint to Default color.
        foreach(Transform c in cubes)
        {
            c.gameObject.GetComponent<MeshRenderer>().material = defaultCubeMaterial;
        }

        pointsData = new int[points.Length];

        for(int i = 0; i <= lenght; i++)
        {
            for(int j = 0; j <= width; j++)
            {
                if (i == 0 || j == 0 || i == lenght || j == width)
                {
                    pointsData[j + i * (width + 1)] = 1;
                    points[j + i * (width + 1)].gameObject.GetComponent<MeshRenderer>().material = coveredAreaMaterial;
                }
            }
        }

        //Place player to initial position
        player.position = points[0].position;
        playerCurrentPoint = Vector2.zero;

        sequence = new List<int>();
    }
}
