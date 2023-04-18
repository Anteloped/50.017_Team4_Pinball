using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    // Implement dynamic bounding volume trees 
    GameObject table;
    GameObject ball;
    Ball ballScript;
    BoundingBox box;

    private List<GameObject> objects;
    private List<List<GameObject>> box_obj;
    private List<BoundingBox> Boxes;
    public Vector3 min;
    public Vector3 max;

    NarrowPhase collision_check;


    public BoundingBox(Vector2 min, Vector2 max)
    {
        this.min = min;
        this.max = max;
    }
    void Start()
    {
        Debug.Log("Bounding Box");

        table = GameObject.FindGameObjectWithTag("Table");
        box = new BoundingBox(new Vector3(), new Vector3());

        objects = GetAllChildren(table);
        Boxes = new List<BoundingBox>();
        //box.Split(Boxes);

        //Debug.Log("Box count");
        //Debug.Log(Boxes.Count);

        //objInBox(objects, Boxes);

        collision_check = table.AddComponent<NarrowPhase>();
    }
    //collision_check.collide(obj,ball,ballScript);
    // Update is called once per frame
    void Update()
    {
        //ball = GameObject.FindGameObjectWithTag("Ball");
        //ballScript = ball.GetComponent<Ball>();

        //for (int i = 0; i <= 3; i++)
        //{
        //    if (ballInsideBox(ballScript, Boxes[i]))
        //    {
        //        foreach (GameObject obj in box_obj[i])
        //        {
        //            collision_check.collide(obj);
        //        }
        //    }
        //}
    }

    public void objInBox(List<GameObject> objects, List<BoundingBox> Boxes)
    {
        foreach (GameObject obj in objects)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (isInsideBox(obj, Boxes[i]))
                {
                    box_obj[i].Add(obj);
                }
                Debug.Log(box_obj[i]);
                Debug.Log(box_obj);
            }
        }
    }

    public bool ballInsideBox(Ball ball, BoundingBox b)
    {
        if (ball.transform.position.x >= b.min.x && ball.transform.position.x >= b.max.x &&
        ball.transform.position.y >= b.min.y && ball.transform.position.y >= b.max.y &&
        ball.transform.position.z >= b.min.z && ball.transform.position.z >= b.max.z)
        {
            return true;
        }
        return false;
    }
    public bool isInsideBox(GameObject o, BoundingBox b)
    {
        if (o.transform.position.x >= b.min.x && o.transform.position.x >= b.max.x &&
        o.transform.position.y >= b.min.y && o.transform.position.y >= b.max.y &&
        o.transform.position.z >= b.min.z && o.transform.position.z >= b.max.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<GameObject> GetAllChildren(GameObject parent)
    {
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {

            Transform childTransform = parent.transform.GetChild(i);
            GameObject childGameObject = childTransform.gameObject;
            objects.Add(childGameObject);
            List<GameObject> childChildren = GetAllChildren(childGameObject);
            objects.AddRange(childChildren);
        }
        return objects;
    }
    public void Split(List<BoundingBox> result)
    {
        GameObject Bound = GameObject.FindGameObjectWithTag("BoundingBox");
        MeshRenderer renderer = Bound.GetComponent<MeshRenderer>();

        // Calculate the bounding box in world space
        min = renderer.bounds.min;
        max = renderer.bounds.max;

        float midX = (min.x + max.x) / 2;
        Vector3 centerLeft = new Vector3(min.x + midX, 0, (max.z + min.z) / 2);
        Vector3 centerRight = new Vector3(max.x - midX, 0, (max.z + min.z) / 2);
        BoundingBox leftBottom = Bound.AddComponent<BoundingBox>();
        leftBottom.min = min;
        leftBottom.max = centerLeft;
        BoundingBox rightBottom = Bound.AddComponent<BoundingBox>();
        rightBottom.min = new Vector3(centerLeft.x, 0, min.z);
        rightBottom.max = new Vector3(centerRight.x, 0, centerLeft.z);
        BoundingBox leftTop = Bound.AddComponent<BoundingBox>();
        leftTop.min = new Vector3(min.x, 0, centerLeft.z);
        leftTop.max = new Vector3(centerLeft.x, 0, max.z);
        BoundingBox rightTop = Bound.AddComponent<BoundingBox>();
        rightTop.min = centerRight;
        rightTop.max = max;

        result.Add(leftTop);
        result.Add(rightTop);
        result.Add(leftBottom);
        result.Add(rightBottom);
    }
}