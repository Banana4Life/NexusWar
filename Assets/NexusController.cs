using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour {

	public bool evil = false;
	public GameObject spawnerPrefab;
	public GameObject enemy;

	// Use this for initialization
	void Start () {
		var unit = Instantiate (spawnerPrefab, transform.position + (enemy.transform.position - transform.position).normalized * 20, Quaternion.Euler (0, 0, 0), transform.parent);
		unit.transform.LookAt (enemy.transform);
		var controller = unit.GetComponent<NoobUnitSpawner> ();
		controller.nexus = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnemyHit(NoobUnitController noobUnit)
	{
		Destroy (gameObject);
	}
}
