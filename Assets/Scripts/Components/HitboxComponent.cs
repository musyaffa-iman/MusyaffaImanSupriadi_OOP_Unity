using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    // Damage method to accept Bullet
    public void Damage(Bullet bullet)
    {
        health.Subtract(bullet.GetDamage());
    }

    // Overloaded Damage method to accept integer
    public void Damage(int amount)
    {
        health.Subtract(amount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            Debug.Log("Hit");
            return;
        }
    }
}
