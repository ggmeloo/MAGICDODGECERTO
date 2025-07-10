// PlayerHealth.cs (VERSÃO CORRIGIDA E ADAPTADA AO FLUXO)
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Efeitos e Sons")]
    public GameObject deathEffectPrefab;
    public AudioClip deathSound;

    private bool isDead = false;

    // Garante que o objeto tenha a tag "Player" para colisões funcionarem.
    void Start()
    {
        if (!gameObject.CompareTag("Player"))
        {
            gameObject.tag = "Player";
            Debug.LogWarning("Objeto sem a tag 'Player'. Tag adicionada automaticamente.", this);
        }
    }

    // Apenas para testes, pode ser removido depois.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }
    }

    // Detecta colisão com inimigos (geralmente objetos com RigidBody2D)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    // Detecta colisão com inimigos que são Triggers (não sólidos)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void Die()
    {
        // Impede que a função seja chamada várias vezes.
        if (isDead) return;
        isDead = true;

        Debug.Log("Jogador morreu. Iniciando sequência de morte.");

        // Cria o efeito visual de morte.
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // Toca o som de morte.
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        // --- MUDANÇA IMPORTANTE ---
        // REMOVIDO: A lógica de salvar o score agora fica na tela de GameOver,
        // dentro do script GameOverUI.cs, quando o jogador clica em "Submit".
        // ScoreManager2.instance.SaveHighScore(); // <--- LINHA REMOVIDA

        // Desativa o jogador para que ele suma da tela e pare de interagir.
        gameObject.SetActive(false);

        // Chama a função para carregar a cena de Game Over após 1.5 segundos.
        // Isso dá tempo para os efeitos de som e visual acontecerem.
        Invoke(nameof(LoadGameOverScene), 1.5f);
    }

    private void LoadGameOverScene()
    {
        // Carrega a cena. Certifique-se que o nome "GameOver" está correto
        // e que a cena foi adicionada no Build Settings.
        SceneManager.LoadScene("GameOver");
    }
}