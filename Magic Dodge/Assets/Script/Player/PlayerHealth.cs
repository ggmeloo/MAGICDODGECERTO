// PlayerHealth.cs (VERS�O FINAL E CORRIGIDA)
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Efeitos e Sons")]
    public GameObject deathEffectPrefab;
    public AudioClip deathSound;

    private bool isDead = false;

    void Start()
    {
        if (!gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Player";
        }
    }

    // Teste de morte com a tecla 'K'
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    // Colis�o com inimigos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Jogador morreu. Iniciando sequ�ncia de morte.");

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        // Salva o High Score usando o ScoreManager2
        if (ScoreManager2.instance != null)
        {
            ScoreManager2.instance.SaveHighScore();
        }
        else
        {
            Debug.LogWarning("Inst�ncia do ScoreManager2 n�o encontrada. N�o foi poss�vel salvar o score.");
        }

        gameObject.SetActive(false);
        Invoke(nameof(LoadGameOverScene), 1.5f);
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }
}