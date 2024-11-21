using UnityEngine;

public class EnemyForward : Enemy
{
    private float speed = 5f;

    protected override void Start()
    {
        base.Start();
        
        // Set posisi spawn di atas layar
        transform.position = new Vector2(Random.Range(-Camera.main.aspect * Camera.main.orthographicSize, Camera.main.aspect * Camera.main.orthographicSize), Camera.main.orthographicSize);
    }

    void Update()
    {
        // Pindahkan enemy ke bawah
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Jika keluar dari layar, hancurkan objek
        if (transform.position.y < -Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika bertabrakan dengan player, hancurkan diri sendiri
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
