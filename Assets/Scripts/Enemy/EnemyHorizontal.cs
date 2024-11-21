using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 5f;
    private Vector2 direction;

    protected override void Start()
    {
        base.Start();
        
        // Random spawn di sisi kiri atau kanan layar
        float spawnSide = Random.value > 0.5f ? -1 : 1;
        direction = new Vector2(spawnSide, 0);
        
        // Atur posisi spawn di kiri atau kanan layar
        transform.position = new Vector2(spawnSide * Camera.main.aspect * Camera.main.orthographicSize, transform.position.y);
    }

    void Update()
    {

        
        // Pindahkan enemy secara horizontal
        transform.Translate(direction * speed * Time.deltaTime);

        // Jika keluar dari layar, balikkan arah
        if (Mathf.Abs(transform.position.x) > Camera.main.aspect * Camera.main.orthographicSize + 5)
        {
            direction = -direction;
        }
    }
}
