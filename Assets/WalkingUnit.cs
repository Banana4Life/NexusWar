using System;
using UnityEngine;

public class WalkingUnit : MonoBehaviour
{
    public bool TouchingGround;

    public bool IsOnGround()
    {
        return TouchingGround;
    }

    protected bool IsFloorObject(GameObject go)
    {
        return go.tag == "floor";
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (IsFloorObject(collision.collider.gameObject))
        {
            TouchingGround = true;
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (IsFloorObject(collision.collider.gameObject))
        {
            TouchingGround = false;
        }
    }
}