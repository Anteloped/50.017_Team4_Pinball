using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * 1.5f, scale.y, scale.z * 1.5f);
    }
}
