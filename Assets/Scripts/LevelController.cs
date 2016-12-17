using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject CameraObject;
    public GameObject SpawnerPrefab;
    public GameObject AlliedNexus;

    private Camera mainCamera;
    private BuildState buildingState = BuildState.Pre;
    private GameObject pendingSpawner;

	// Use this for initialization
	void Start ()
	{
	    mainCamera = CameraObject.GetComponent<Camera>();
	    if (!mainCamera)
	    {
	        Debug.LogError("No camera found!");
	    }
	}

    bool IsBuildRequested()
    {
        return Input.GetAxisRaw("Fire2") > 0;
    }
	
	void Update ()
	{
	    switch (buildingState)
	    {
	        case BuildState.Pre:
	            if (IsBuildRequested())
	            {
                    pendingSpawner = BuildSpawner();
	                if (pendingSpawner)
	                {
	                    buildingState = BuildState.PositionSelected;
	                }
	            }
	            break;
	        case BuildState.PositionSelected:
	            UpdatePendingSpawner(pendingSpawner);
	            if (!IsBuildRequested())
	            {
	                if (pendingSpawner)
	                {
	                    buildingState = BuildState.RotationSelected;
	                }
	                else
	                {
	                    buildingState = BuildState.Pre;
	                }
	            }
	            break;
	        case BuildState.RotationSelected:
	            if (pendingSpawner)
	            {
	                FinalizeSpawner(pendingSpawner);
	                pendingSpawner = null;
	                buildingState = BuildState.Pre;
	            }

	            break;
	    }
	}

    private Vector3? GetTargetedWorldPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, 1 << 8))
        {
            return hit.point;
        }
        return null;
    }

    protected void UpdatePendingSpawner(GameObject spawner)
    {
        Vector3? target = GetTargetedWorldPosition();
        if (target.HasValue)
        {
            var dir = target.Value - spawner.transform.position;
            dir.y = 0;
            spawner.transform.LookAt(spawner.transform.position + dir);
        }
    }

    protected void FinalizeSpawner(GameObject spawner)
    {
        var controller = spawner.GetComponent<UnitSpawner>();
        if (controller)
        {
            if (controller.IsBlocked())
            {
                Destroy(spawner);
            }
            else
            {
                controller.StartSpawning();
            }
        }
    }

    protected GameObject BuildSpawner()
    {
        Vector3? target = GetTargetedWorldPosition();
        if (target.HasValue)
        {
            var pos = target.Value;
            pos.y = 0;
            var spawner = Instantiate(SpawnerPrefab, transform);
            spawner.transform.position = pos;
            spawner.transform.Translate(Vector3.up * 1.5f);
            var controller = spawner.GetComponent<UnitSpawner>();
            controller.AlliedNexus = AlliedNexus;
            UpdatePendingSpawner(spawner);
            return spawner;
        }
        return null;
    }

    enum BuildState
    {
        Pre,
        PositionSelected,
        RotationSelected
    }
}
