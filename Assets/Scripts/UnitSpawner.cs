using UnityEngine;

public class UnitSpawner : Building
{
    public float SpawnFrequency = 5;
    public GameObject UnitPrefab;
    public GameObject SpawnPadObject;
    private int blockCounter;

    public void StartSpawning(float firstSpawnDelay = 0)
    {
        InvokeRepeating("SpawnUnit", firstSpawnDelay, SpawnFrequency);
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnUnit");
    }

    protected void SpawnUnit()
    {
        var nexusCtrl = AlliedNexus.GetComponent<NexusController>();
        var unit = Instantiate(UnitPrefab, SpawnPadObject.transform.position + Vector3.up, Quaternion.Euler(0, 0, 0),
            transform.parent);
        var ctrl = unit.GetComponent<UnitController>();
        ctrl.TargetNexus = nexusCtrl.Enemy;
        ctrl.GoTowardsNexus();
    }

    public bool IsBlocked()
    {
        return blockCounter > 0;
    }

    private void UpdateBlockCounter(Collision c, int i)
    {
        if (c.collider.tag != "floor")
        {
            blockCounter += i;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        UpdateBlockCounter(other, 1);
    }

    private void OnCollisionExit(Collision other)
    {
        UpdateBlockCounter(other, -1);
    }
}
