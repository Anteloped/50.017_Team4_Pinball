using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloudGenerator : MonoBehaviour
{
    public GameObject cloudPrefab; // The prefab for the cloud particle
    public float cloudDensity = 0.1f; // The density of clouds
    public float cloudSize = 5f; // The maximum size of clouds
    public float cloudSpeed = 1f; // The speed of clouds
    public float cloudHeight = 10f; // The height at which clouds are generated
    public float cloudSpread = 50f; // The spread of clouds

    private ParticleSystem cloudSystem;

    void Start()
    {
        cloudSystem = GetComponent<ParticleSystem>();
        GenerateClouds();
    }

    void GenerateClouds()
    {
        float spawnRadius = Mathf.Sqrt(cloudDensity) * cloudSpread;
        for (int i = 0; i < cloudDensity * 1000f; i++)
        {
            float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
            float distance = Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f)) * spawnRadius;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
            position.y = cloudHeight;
            GameObject cloud = Instantiate(cloudPrefab, position, Quaternion.identity, transform);
            cloud.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, cloudSize);
            cloud.GetComponent<Rigidbody>().velocity = Vector3.back * cloudSpeed;
        }
    }
}
