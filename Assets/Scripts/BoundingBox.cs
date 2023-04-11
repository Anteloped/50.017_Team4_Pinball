using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    // Implement dynamic bounding volume trees 
    public Vector2 min;
    public Vector2 max;
    public BoundingBox(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
    }

    void Start()
    {
        //get mesh renderer of the object
        GameObject table = GameObject.FindGameObjectWithTag("Table");
        MeshRenderer renderer = table.GetComponent<MeshRenderer>();

        // Calculate the bounding box in world space
        min = renderer.bounds.min;
        max = renderer.bounds.max;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Split(List<BoundingBox> result)
    {
        float midX = (min.x + max.x) / 2;
        Vector2 centerLeft = new Vector2(min.x + midX, (max.y + min.y) / 2);
        Vector2 centerRight = new Vector2(max.x - midX, (max.y + min.y) / 2);

        BoundingBox leftTop = new BoundingBox(min, centerLeft);
        BoundingBox rightTop = new BoundingBox(new Vector2(centerLeft.x, min.y), new Vector2(centerRight.x, centerLeft.y));
        BoundingBox leftBottom = new BoundingBox(new Vector2(min.x, centerLeft.y), new Vector2(centerLeft.x, max.y));
        BoundingBox rightBottom = new BoundingBox(centerRight, max);
        result.Add(leftTop);
        result.Add(rightTop);
        result.Add(leftBottom);
        result.Add(rightBottom);
        }

}
