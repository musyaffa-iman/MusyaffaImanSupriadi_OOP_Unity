using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private GameObject bulletContainer;
    private float timer;
    [SerializeField] private bool isPlayerWeapon;
    public Transform parentTransform;

    private void Start()
    {
        if (transform.parent != null)
        {
            if (transform.parent.CompareTag("Player"))
            {
                isPlayerWeapon = true;
            }
            else if (transform.parent.CompareTag("Enemy"))
            {
                isPlayerWeapon = false;
            }
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            timer = 0f;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletContainer == null)
        {
            bulletContainer = new GameObject("Bullets");
            objectPool = new ObjectPool<Bullet>(
                CreateBullet,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100
            );
        }

        Bullet bullet = objectPool.Get();
        if (isPlayerWeapon == true)
            bullet.SetDirection(Vector2.up);
        else
            bullet.SetDirection(Vector2.down);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.SetParent(bulletContainer.transform);
        bullet.SetPool(objectPool);
        return bullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
