using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class PlanarUVMappingScript : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        CalcPlaneMapping(mesh);
    }

    void CalcPlaneMapping(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        float xmin = float.MaxValue, xmax = float.MinValue, ymin = float.MaxValue, ymax = float.MinValue;

        // find bounding box
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            xmin = Mathf.Min(xmin, vertex.x);
            xmax = Mathf.Max(xmax, vertex.x);
            ymin = Mathf.Min(ymin, vertex.y);
            ymax = Mathf.Max(ymax, vertex.y);
        }

        // calculate UV coordinates
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            float u = (vertex.x - xmin) / (xmax - xmin);
            float v = (vertex.y - ymin) / (ymax - ymin);
            uvs[i] = new Vector2(u, v);
        }

        mesh.uv = uvs;
    }
}
