using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject spawnedEnemy; // Referensi prefab musuh

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; // Minimum kill untuk menambah spawn
    public int totalKill = 0; // Total kill sepanjang permainan
    private int totalKillWave = 0; // Kill dalam satu wave

    [SerializeField] private float spawnInterval = 3f; // Jeda antar spawn

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Counter total musuh yang sudah di-spawn
    public int defaultSpawnCount = 1; // Jumlah spawn awal
    public int spawnCountMultiplier = 1; // Pengali jumlah spawn
    public int multiplierIncreaseCount = 1; // Penambah multiplier

    public CombatManager combatManager; // Referensi ke CombatManager

    public bool isSpawning = false; // Status spawning

    private void Start()
    {
        // Memulai proses spawning
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning)
            {
                // Hitung jumlah musuh yang akan di-spawn berdasarkan multiplier
                int enemiesToSpawn = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnInterval); // Tunggu sesuai jeda
                }

                totalKillWave = 0; // Reset kill wave
                spawnCountMultiplier++; // Naikkan multiplier
            }
            else
            {
                yield return null; // Tunggu hingga spawning diaktifkan
            }
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            // Spawn musuh di posisi spawner
            Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
            spawnCount++;
        }
        else
        {
            Debug.LogWarning("SpawnedEnemy prefab is not set!");
        }
    }

    public void RegisterKill()
    {
        totalKill++;
        totalKillWave++;

        // Naikkan multiplier jika jumlah kill memenuhi syarat
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            multiplierIncreaseCount++;
            totalKillWave = 0; // Reset kill wave setelah multiplier bertambah
        }
    }
}
