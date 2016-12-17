using UnityEngine;

public class Health : MonoBehaviour
{
    public float StartHealth = 10;
    public float MaxHealth = 10;

    private float health;

    public float GetHealth()
    {
        return health;
    }

    public void Damage(float points)
    {
        health = Mathf.Max(0, health - points);
    }

    public void Heal(float points)
    {
        health = Mathf.Min(MaxHealth, health + points);
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
