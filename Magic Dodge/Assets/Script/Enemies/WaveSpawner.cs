// WaveSpawner.cs (VERS�O FINAL E CORRIGIDA)
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    [Header("Configura��o de Progress�o")]
    public PowerStage[] powerProgression;

    [Header("Configura��o das Waves")]
    public Wave[] waves;

    [Header("Configura��o de Pontua��o")]
    public int scorePerWave = 100; // Pontos de b�nus por passar de wave

    [Header("Refer�ncias")]
    public Transform[] spawnPoints;
    public Barra powerBar;

    private int currentWaveIndex = -1;
    private Coroutine currentSpawnCoroutine;

    [System.Serializable]
    public class PowerStage
    {
        [Tooltip("O tipo de poder que ser� ativado.")]
        public PowerType powerType;
        [Tooltip("A bala que o jogador ir� atirar com este poder.")]
        public GameObject bulletPrefab;
        [Tooltip("A cad�ncia de tiro com este poder.")]
        public float fireRate = 2f;
        [Tooltip("(Refer�ncia visual) �cone est�tico para este poder.")]
        public Sprite powerIcon;
    }

    // Esta � a "ficha de cadastro" para uma wave de inimigos.
    // O script precisa disso para saber o que � uma Wave.
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<GameObject> enemyPrefabs;
        [Tooltip("Dura��o da wave em segundos.")]
        public float duration;
        [Tooltip("Inimigos a serem gerados por segundo.")]
        public float spawnRate;
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (waves.Length == 0 || spawnPoints.Length == 0 || powerBar == null)
        {
            Debug.LogError("WaveSpawner n�o configurado corretamente!", this);
            this.enabled = false;
            return;
        }
        StartNewGame();
    }

    void StartNewGame()
    {
        // Reseta o score usando o ScoreManager2
        ScoreManager2.instance?.ResetScore();
        currentWaveIndex = -1;
        OnWaveTimerEnd();
    }

    public void EnemyDefeated()
    {
        // Fun��o placeholder para futuras l�gicas
    }

    public void OnWaveTimerEnd()
    {
        // Adiciona pontos de b�nus pela wave passada
        if (currentWaveIndex >= 0)
        {
            ScoreManager2.instance?.AddScore(scorePerWave);
        }

        if (currentSpawnCoroutine != null)
        {
            StopCoroutine(currentSpawnCoroutine);
        }

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("<color=cyan>FIM DE JOGO - VOC� VENCEU!</color>");
            return;
        }

        BeginWave(currentWaveIndex);
    }

    private void BeginWave(int waveIndex)
    {
        Wave currentWave = waves[waveIndex];
        PowerType powerForThisWave = PowerType.Default;

        int powerIndex = waveIndex - 1;
        if (waveIndex > 0 && powerIndex < powerProgression.Length)
        {
            PowerStage newPower = powerProgression[powerIndex];
            powerForThisWave = newPower.powerType;
            PlayerShooting.instance.SetShootingModeAndBullet(newPower.powerType, newPower.bulletPrefab, newPower.fireRate);
        }
        else
        {
            PlayerShooting.instance.ApplyWaveShootingSetup(ShootingMode.Right, PlayerShooting.instance.defaultBulletPrefab, PlayerShooting.instance.defaultFireRate);
        }

        powerBar.SetupNewWave(waveIndex + 1, currentWave.duration, powerForThisWave);
        currentSpawnCoroutine = StartCoroutine(SpawnEnemiesContinuously(currentWave));
    }

    IEnumerator SpawnEnemiesContinuously(Wave wave)
    {
        if (wave.enemyPrefabs == null || wave.enemyPrefabs.Count == 0)
        {
            yield break;
        }

        float timeToWait = 1f / wave.spawnRate;

        while (true)
        {
            GameObject enemyToSpawn = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Count)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeToWait);
        }
    }
}