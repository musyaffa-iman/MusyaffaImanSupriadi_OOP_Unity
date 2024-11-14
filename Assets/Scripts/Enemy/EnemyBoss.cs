using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    [Header("Weapon Stats")]
    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private GameObject bulletContainer;
    private float shootTimer;
    public Transform parentTransform;
    private float speed = 5f;
    private Vector2 direction;

    protected override void Start()
    {
        base.Start();
        
        float spawnSide = Random.value > 0.5f ? -1 : 1;
        direction = new Vector2(spawnSide, 0);      
        transform.position = new Vector2(spawnSide * Camera.main.aspect * Camera.main.orthographicSize, transform.position.y);
    }

    protected override void Update()
    {
        base.Update();  
        transform.Translate(direction * speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > Camera.main.aspect * Camera.main.orthographicSize)
        {
            direction = -direction;
        }
    }
}
