using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindricalUVMappingScript : MonoBehaviour
{
    public float radius = 1.0f;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float angle = Mathf.Atan2(vertices[i].z, vertices[i].x);
            float u = angle / (2 * Mathf.PI) + 0.5f;

            uv[i] = new Vector2(u, vertices[i].y / (2 * radius) + 0.5f);
        }

        mesh.uv = uv;
    }
}
