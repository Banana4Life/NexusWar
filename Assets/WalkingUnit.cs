using System;
using UnityEngine;

public class WalkingUnit : MonoBehaviour
{
	public bool touchingGround = false;

	public bool IsOnGround()
	{
		return touchingGround;
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "floor") {
			touchingGround = true;
		}
	}

	protected void OnCollisionExit(Collision collision)
	{
		if (collision.collider.gameObject.tag == "floor") {
			touchingGround = false;
		}
	}
}

