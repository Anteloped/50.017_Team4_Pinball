using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballBoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    public GameObject[] walls; // 0 - Up 1 - Down 2 - Left 3 - Right
    public GameObject[] floors;

    private void Start()
    {
        GenerateWalls();
        GenerateFloor();
    }

    private void GenerateWalls()
    {
        // Define the dimensions of the board
        float width = 10.0f;
        float height = 10.0f;
        float depth = 0.5f;

        // Create the walls
        CreateWall(new Vector3(-width / 2, 0, 0), new Vector3(depth, height, depth));   // left wall
        CreateWall(new Vector3(width / 2, 0, 0), new Vector3(depth, height, depth));    // right wall
        CreateWall(new Vector3(0, 0, -height / 2), new Vector3(depth, height, depth)); // back wall
        CreateWall(new Vector3(0, 0, height / 2), new Vector3(depth, height, depth));  // front wall
    }

    private void GenerateFloor()
    {
        // Define the dimensions of the board
        float width = 10.0f;
        float depth = 0.5f;

        // Create the floor
        GameObject floor = Instantiate(floorPrefab, transform);
        floor.transform.localScale = new Vector3(width, depth, width);
    }

    private void CreateWall(Vector3 position, Vector3 scale)
    {
        // Create the wall
        GameObject wall = Instantiate(wallPrefab, transform);
        wall.transform.localPosition = position;
        wall.transform.localScale = scale;
    }
}


