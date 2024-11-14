using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    // Constructor to set initial health to maxHealth
    private void Start()
    {
        health = maxHealth;
    }

    // Getter for health
    public int GetHealth()
    {
        return health;
    }

    // Method to subtract health
    public void Subtract(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            DestroyObject();
        }
    }

    // Method to destroy the object if health is <= 0
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
