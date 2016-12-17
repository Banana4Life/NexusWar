using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float ImpactDamage = 10;

    private void OnCollisionEnter(Collision other)
    {
        var health = other.collider.gameObject.GetComponent<Health>();
    }
}
