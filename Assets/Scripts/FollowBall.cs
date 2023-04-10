using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FollowBall : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;
    //public GameObject toggler;
    public Toggle toggler;
    private Toggle m_Toggle;
    bool follow = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    [SerializeField] string cameraName = "Main Camera";


    void Start()
    {
        //GameObject cameraObject = GameObject.Find(cameraName);
        m_Toggle = toggler;//.GetComponent<Toggle>();
        if (m_Toggle != null)
        {
            m_Toggle.isOn = false;
            m_Toggle.onValueChanged.AddListener(ToggleValueChanged);
            //UnityEngine.Debug.Log("Toggle listener added.");
        }
        else
        {
            //UnityEngine.Debug.LogError("Toggle component not found.");
        }
    }

    void ToggleValueChanged(bool isOn)
    {
        follow = isOn;
        UnityEngine.Debug.Log("Toggle value changed to: " + isOn);
    }

    void FixedUpdate()
    {
        // UnityEngine.Debug.Log("Fixedupdate called" + follow);
        if (follow && target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);
            UnityEngine.Debug.Log("Camera position: " + transform.position + ", camera rotation: " + transform.rotation);
        }
        // Reset the camera position and rotation to its original state
        //else
        //{
        //    GameObject cameraObject = GameObject.Find(cameraName);
        //    if (cameraObject != null)
        //    {
        //        cameraObject.transform.position = originalPosition;
        //        cameraObject.transform.rotation = originalRotation;
        //    }
        //}
    }
}
