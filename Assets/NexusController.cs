using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour
{
    public bool Evil;
    public GameObject SpawnerPrefab;
    public GameObject Enemy;

    // Use this for initialization
    void Start()
    {
        var unit = Instantiate(SpawnerPrefab,
            transform.position + (Enemy.transform.position - transform.position).normalized * 20,
            Quaternion.Euler(0, 0, 0), transform.parent);
        unit.transform.LookAt(Enemy.transform);
        var controller = unit.GetComponent<NoobUnitSpawner>();
        controller.Nexus = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnemyHit(NoobUnitController noobUnit)
    {
        Destroy(gameObject);
    }
}