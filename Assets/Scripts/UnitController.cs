using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public GameObject TargetNexus;
    public float Speed = 10;
    public readonly HashSet<GameObject> Targets = new HashSet<GameObject>();
    private NavMeshAgent navAgent;

    public void GoTowardsNexus()
    {
        var target = TargetNexus.transform.position;
        if (!navAgent)
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
        }
        if (navAgent.destination == target)
        {
            navAgent.Resume();
        }
        else
        {
            navAgent.SetDestination(TargetNexus.transform.position);
        }
    }

    public void StopMoving()
    {
        navAgent.Stop();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        var nexusController = collision.collider.gameObject.GetComponent<NexusController>();
        if (nexusController)
        {
            nexusController.EnemyHit(this);
        }

        var unit = collision.collider.gameObject.GetComponent<UnitController>();
        if (unit)
        {
            unit.OnHitByUnit(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        var unit = target.GetComponent<UnitController>();
        if (unit)
        {
            if (unit.TargetNexus != TargetNexus)
            {
                NewTargetInRange(target);
            }
            return;
        }
        var building = target.GetComponent<Building>();
        if (building)
        {
            if (building.AlliedNexus == TargetNexus)
            {
                NewTargetInRange(target);
            }
        }
    }

    private void NewTargetInRange(GameObject target)
    {
        Targets.Add(target);
        OnTargetEnterRange(target);
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.gameObject;
        if (Targets.Contains(target))
        {
            Targets.Remove(target);
            OnTargetLeaveRange(target);
        }
    }

    protected void OnTargetEnterRange(GameObject target)
    {}

    protected void OnTargetLeaveRange(GameObject target)
    {}

    public void OnHitByUnit(UnitController unit)
    {}
}
