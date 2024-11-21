using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private bool waveInProgress = false;

    private void Start()
    {
        // Mulai wave system
        StartCoroutine(WaveSystem());
    }

    private IEnumerator WaveSystem()
    {
        while (true)
        {
            if (!waveInProgress)
            {
                yield return new WaitForSeconds(waveInterval); // Tunggu hingga wave berikutnya
                StartNewWave();
            }

            // Periksa apakah semua musuh telah dikalahkan
            if (AllEnemiesDefeated())
            {
                waveInProgress = false; // Akhiri wave jika semua musuh mati
            }

            yield return null; // Lanjutkan ke frame berikutnya
        }
    }

    private void StartNewWave()
    {
        waveInProgress = true;
        waveNumber++;
        totalEnemies = 0;

        Debug.Log($"Wave {waveNumber} started!");

        // Aktifkan spawning pada semua EnemySpawner
        foreach (EnemySpawner spawner in enemySpawners)
        {
            int enemiesToSpawn = CalculateEnemiesToSpawn();
            spawner.spawnCount = enemiesToSpawn;
            spawner.isSpawning = true;
            totalEnemies += enemiesToSpawn;
        }
    }

    private int CalculateEnemiesToSpawn()
    {
        // Hitung jumlah musuh yang akan di-spawn berdasarkan nomor wave
        return waveNumber * 2;
    }

    private bool AllEnemiesDefeated()
    {
        // Periksa apakah semua EnemySpawner telah selesai
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.spawnCount > 0)
            {
                return false;
            }
        }

        return true;
    }
}
