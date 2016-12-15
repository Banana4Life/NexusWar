using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobUnitController : WalkingUnit {

	public GameObject targetNexus;
	public float speed = 10;
	
	// Update is called once per frame
	void Update () {
		var rigidBody = GetComponent<Rigidbody> ();
		if (IsOnGround ()) {
			//var targetNexusController = targetNexus.GetComponent<NexusController> ();
			var direction = (targetNexus.transform.position - transform.position).normalized;
			rigidBody.velocity = direction * speed * Time.deltaTime;

			var angle = Mathf.Atan2 (direction.x, direction.z);
			transform.eulerAngles = Vector3.up * angle;
		} else {
			rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
		}

	}

	new void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter (collision);
		var nexusController = collision.collider.gameObject.GetComponent<NexusController> ();
		if (nexusController) {
			nexusController.EnemyHit (this);
		}

		var unit = collision.collider.gameObject.GetComponent<NoobUnitController> ();
		if (unit) {
			unit.HitByUnit (this);
		}
	}

	public void HitByUnit(NoobUnitController unit)
	{
		Destroy (gameObject);
	}
}
