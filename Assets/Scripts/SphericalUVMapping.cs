using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SphericalUVMapping : MonoBehaviour 
{

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        CalcSphereMapping(mesh);
    }

    void CalcSphereMapping(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float x = vertices[i].x;
            float y = vertices[i].y;
            float z = vertices[i].z;

            float theta = Mathf.Acos(z / Mathf.Sqrt(x * x + y * y + z * z));
            float phi = Mathf.Atan2(y, x);

            // normalize [0,1] as Mathf.Acos returns [0,Mathf.PI] and Mathf.Atan2 returns [-Mathf.PI, Mathf.PI]
            uvs[i].x = (theta + Mathf.PI) / (2 * Mathf.PI);
            uvs[i].y = (phi + Mathf.PI) / (2 * Mathf.PI);
        }

        mesh.uv = uvs;
    }
}


