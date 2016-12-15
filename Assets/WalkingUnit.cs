using System;
using UnityEngine;

public class WalkingUnit : MonoBehaviour
{
    public bool TouchingGround;

    public bool IsOnGround()
    {
        return TouchingGround;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "floor")
        {
            TouchingGround = true;
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.tag == "floor")
        {
            TouchingGround = false;
        }
    }
}