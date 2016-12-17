using UnityEngine;

public class WalkingUnit : UnitController
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

    protected new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
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