using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerLife : MonoBehaviour
{
    public Vector3 respawn;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Kill()
    {
        rb.velocity = Vector3.zero;
        transform.position = respawn;
    }
    public void CheckPoint(Transform obj)
    {
        CheckPoint(obj.position);
    }
    public void CheckPoint(Vector3 pos)
    {
        respawn = pos;
    }
}
