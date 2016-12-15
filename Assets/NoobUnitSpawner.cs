using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoobUnitSpawner : MonoBehaviour
{
    public GameObject UnitPrefab;
    public GameObject Nexus;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnStuff", 0, 2);
        //Invoke("SpawnStuff", 2);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnStuff()
    {
        var spawnPad = gameObject.transform.GetChild(0).gameObject;
        var nexusCtrl = Nexus.GetComponent<NexusController>();
        var direction = (nexusCtrl.Enemy.transform.position - transform.position).normalized;
        var unit = Instantiate(UnitPrefab, spawnPad.transform.position + Vector3.up, Quaternion.Euler(0, 0, 0),
            transform.parent);
        var ctrl = unit.GetComponent<NoobUnitController>();
        ctrl.TargetNexus = nexusCtrl.Enemy;
    }
}