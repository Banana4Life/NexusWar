using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class ProjectileController : MonoBehaviour
{
    public float ImpactDamage = 10;

    private void OnCollisionEnter(Collision other)
    {
        var hisHealth = other.collider.gameObject.GetComponent<Health>();
        if (hisHealth)
        {
            hisHealth.Damage(ImpactDamage);
        }
    }
}
