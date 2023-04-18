using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TriangleRasterizer : MonoBehaviour
{
    public Mesh mesh;
    public Material material;

    void Start()
    {
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            // Get the three vertices of the triangle
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            // Find the bounding box of the triangle
            float minX = Mathf.Min(v0.x, Mathf.Min(v1.x, v2.x));
            float maxX = Mathf.Max(v0.x, Mathf.Max(v1.x, v2.x));
            float minY = Mathf.Min(v0.y, Mathf.Min(v1.y, v2.y));
            float maxY = Mathf.Max(v0.y, Mathf.Max(v1.y, v2.y));

            // Loop through the pixels in the bounding box
            for (int x = (int)minX; x <= maxX; x++)
            {
                for (int y = (int)minY; y <= maxY; y++)
                {
                    // Find the barycentric coordinates of the pixel
                    Vector3 pixel = new Vector3(x + 0.5f, y + 0.5f, 0);
                    Vector3 barycentric = BarycentricCoordinates(v0, v1, v2, pixel);

                    // If the pixel is inside the triangle, draw it
                    if (IsInsideTriangle(barycentric))
                    {
                        DrawPixel(x, y, material.color);
                    }
                }
            }
        }
    }

    // Calculate the barycentric coordinates of a pixel in a triangle
    Vector3 BarycentricCoordinates(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 pixel)
    {
        Vector3 v0v1 = v1 - v0;
        Vector3 v0v2 = v2 - v0;
        Vector3 v0p = pixel - v0;

        float d00 = Vector3.Dot(v0v1, v0v1);
        float d01 = Vector3.Dot(v0v1, v0v2);
        float d11 = Vector3.Dot(v0v2, v0v2);
        float d20 = Vector3.Dot(v0p, v0v1);
        float d21 = Vector3.Dot(v0p, v0v2);

        float invDenom = 1 / (d00 * d11 - d01 * d01);
        float u = (d11 * d20 - d01 * d21) * invDenom;
        float v = (d00 * d21 - d01 * d20) * invDenom;
        float w = 1 - u - v;

        return new Vector3(u, v, w);
    }


    // Check if a point is inside a triangle
    bool IsInsideTriangle(Vector3 barycentric)
    {
        return barycentric.x >= 0 && barycentric.x <= 1 &&
               barycentric.y >= 0 && barycentric.y <= 1 &&
               barycentric.z >= 0 && barycentric.z <= 1;
    }

    // Rasterize a triangle and return a list of pixels inside the triangle
    public List<Vector3Int> RasterizeTriangle(Vector3 v0, Vector3 v1, Vector3 v2, int width, int height)
    {
        List<Vector3Int> pixels = new List<Vector3Int>();

        // Calculate the bounding box of the triangle
        int minX = Mathf.FloorToInt(Mathf.Min(v0.x, Mathf.Min(v1.x, v2.x)));
        int minY = Mathf.FloorToInt(Mathf.Min(v0.y, Mathf.Min(v1.y, v2.y)));
        int maxX = Mathf.CeilToInt(Mathf.Max(v0.x, Mathf.Max(v1.x, v2.x)));
        int maxY = Mathf.CeilToInt(Mathf.Max(v0.y, Mathf.Max(v1.y, v2.y)));

        // Loop over all pixels in the bounding box
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                // Check if the pixel is inside the triangle
                Vector3 pixel = new Vector3(x, y, 0);
                Vector3 barycentric = BarycentricCoordinates(v0, v1, v2, pixel);
                if (IsInsideTriangle(barycentric))
                {
                    // Add the pixel to the list
                    pixels.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        return pixels;
    }

    // Draw a pixel with the given position, color and barycentric coordinates
    // Draw a pixel with the given position and color
    void DrawPixel(int x, int y, Color color)
    {
        // Create a texture if it doesn't exist
        if (material.mainTexture == null)
        {
            material.mainTexture = new Texture2D(512, 512);
            // Set the color of the pixel in the texture
            Texture2D texture = (Texture2D)material.mainTexture;
            texture.SetPixel(x, y, color);
            texture.Apply();
        }
    }
}