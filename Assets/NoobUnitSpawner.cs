using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobUnitSpawner : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject nexus;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnStuff", 0, 2);
		//Invoke("SpawnStuff", 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnStuff()
	{
		var spawnPad = gameObject.transform.GetChild (0).gameObject;
		var nexusCtrl = nexus.GetComponent<NexusController> ();
		var direction = (nexusCtrl.enemy.transform.position - transform.position).normalized;
		var unit = Instantiate (unitPrefab, spawnPad.transform.position + Vector3.up, Quaternion.Euler (0, 0, 0), transform.parent);
		var ctrl = unit.GetComponent<NoobUnitController> ();
		ctrl.targetNexus = nexusCtrl.enemy;
	}
}
