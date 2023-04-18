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
    private List<GameObject> boxes;
    public GameObject boundingBox1;
    public GameObject boundingBox2;
    public GameObject boundingBox3;
    public GameObject boundingBox4;
    NarrowPhase collision_check;
    
    void Start()
    {
        table = GameObject.FindGameObjectWithTag("Table");
        //box = table.AddComponent<BoundingBox>();
        objects = GetAllChildren(table);
        boxes = new List<GameObject>();
        boxes.Add(boundingBox1);
        boxes.Add(boundingBox2);
        boxes.Add(boundingBox3);
        boxes.Add(boundingBox4);
        
        box_obj=objInBox(objects, boxes);
        collision_check = table.GetComponent<NarrowPhase>();
    }
    // Update is called once per frame
    void Update()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null) {
            ballScript = ball.GetComponent<Ball>();

            for (int i = 0; i <= 3; i++)
            {
                if (ballInsideBox(ballScript, boxes[i]))
                {
                    foreach (GameObject obj in box_obj[i])
                    {
                        collision_check.collide(obj);
                    }
                }
            }
        }
    }

    public List<List<GameObject>> objInBox(List<GameObject> objects, List<GameObject> boxes)
    {
        List<List<GameObject>> box_obj = new List<List<GameObject>>();
        foreach (GameObject obj in objects)
        {
            for (int i = 0; i <= 3; i++)
            {
                box_obj.Add(new List<GameObject>());
                if (isInsideBox(obj, boxes[i]))
                {
                    box_obj[i].Add(obj);
                }
            }
        }
        return box_obj;
    }

    public bool ballInsideBox(Ball ball, GameObject b)
    {
        MeshRenderer renderer = b.GetComponent<MeshRenderer>();
        if (ball.transform.position.x >= renderer.bounds.min.x && ball.transform.position.x >=renderer.bounds.max.x &&
        ball.transform.position.y >= renderer.bounds.min.y && ball.transform.position.y >= renderer.bounds.max.y &&
        ball.transform.position.z >= renderer.bounds.min.z && ball.transform.position.z >=renderer.bounds.max.z)
        {
            return true;
        }
        return false;
    }
    public bool isInsideBox(GameObject o, GameObject b)
    {
        MeshRenderer renderer = b.GetComponent<MeshRenderer>();
        if (o.transform.position.x >= renderer.bounds.min.x && o.transform.position.x >=renderer.bounds.max.x &&
        o.transform.position.y >= renderer.bounds.min.y && o.transform.position.y >= renderer.bounds.max.y &&
        o.transform.position.z >= renderer.bounds.min.z && o.transform.position.z >=renderer.bounds.max.z)
        {
            return true;
        }
        return false;
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
   
}