using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperGenerator : MonoBehaviour
{
    public GameObject bumperPrefab;
    public int numBumpers = 5;
    public float minDistance = 1f;
    public float maxDistance = 2f;
    public float bumperScale = 0.6f;

    private Bounds tableBounds;
    private List<GameObject> bumpers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        tableBounds = GetComponent<Renderer>().bounds;
        print(tableBounds);

        for (int i = 0; i < numBumpers; i++)
        {
            // Generate random position
            Vector3 position = GenerateRandomPosition();
            // Instantiate bumper prefab at position
            GameObject newBumper = Instantiate(bumperPrefab, position, Quaternion.identity);
            // Get the original scale of the bumper prefab
            Vector3 originalScale = newBumper.transform.localScale;
            // Set the new scale of the bumper while preserving its original proportions
            newBumper.transform.localScale = new Vector3(originalScale.x * bumperScale, originalScale.y, originalScale.z * bumperScale);
            bumpers.Add(newBumper);

        }

    }

    // Generate a random position for a bumper
    private Vector3 GenerateRandomPosition()
    {
        //Vector3 position = transform.position;
        Vector3 position = tableBounds.center;
        bool overlap = false;
        // Generate random angle
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

        // Generate random distance
        float distance = UnityEngine.Random.Range(minDistance, maxDistance);

        // Calculate position offset
        position.x += Mathf.Cos(angle) * distance;
        position.z += Mathf.Sin(angle) * distance;

        position.x = Mathf.Clamp(position.x, tableBounds.min.x, tableBounds.max.x);
        position.z = Mathf.Clamp(position.z, tableBounds.min.z, tableBounds.max.z);

        // Check for collisions with existing bumpers
        foreach (GameObject bumper in bumpers)
        {
            if (Vector3.Distance(position, bumper.transform.position) < minDistance)
            {
                overlap = true;
                break;
            }
        }

        // If there is an overlap, generate a new position
        if (overlap)
        {
            position = GenerateRandomPosition();
            overlap = false;
        }

        // Clamp position within table bounds
        position.x = Mathf.Clamp(position.x, tableBounds.min.x + minDistance, tableBounds.max.x - minDistance);
        position.z = Mathf.Clamp(position.z, tableBounds.min.z + minDistance, tableBounds.max.z - minDistance);

        // Return position
        return position;
    }
}
