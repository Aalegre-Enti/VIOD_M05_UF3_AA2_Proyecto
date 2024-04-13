using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Vector3 offset;
    [Range(0f, 1f)]
    public float distance;
    void Update()
    {
        transform.position = cam.transform.position * distance + offset;
    }
}
