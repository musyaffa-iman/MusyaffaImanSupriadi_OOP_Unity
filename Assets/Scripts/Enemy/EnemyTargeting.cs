using UnityEngine;

public class EnemyTargeting : Enemy
{
    public Transform player;
    private float speed = 5f;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        // Find the player by tag and check if it's found
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in the scene.");
        }
    }

    void Update()
    {
        // Move towards the player if it exists
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed; // Apply movement using Rigidbody2D
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving if player is missing
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy this object if it collides with the player
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
