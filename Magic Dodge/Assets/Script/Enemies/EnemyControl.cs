using UnityEngine;

public class Enemy : MonoBehaviour
{
    // O inimigo ter� uma refer�ncia ao manager que o criou.
    private WaveManager waveManager;

    // Suponha que seu inimigo tenha vida.
    public float health = 10f;

    /// <summary>
    /// Esta fun��o ser� chamada pelo WaveManager no momento em que o inimigo � criado.
    /// </summary>
    public void SetWaveManager(WaveManager manager)
    {
        waveManager = manager;
    }

    // Exemplo de uma fun��o para tomar dano
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ANTES de se destruir, notifica o WaveManager!
        // A verifica��o 'waveManager != null' � uma boa pr�tica.
        if (waveManager != null)
        {
            waveManager.EnemyDefeated();
        }

        // Adicione aqui efeitos de morte, som, etc.
        Destroy(gameObject);
    }

    // Se seu proj�til do jogador tiver um script...
    // Ele pode chamar esta fun��o ao colidir com o inimigo.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Supondo que a bala d� 10 de dano.
            TakeDamage(10);
            Destroy(collision.gameObject); // Destruir a bala ap�s o impacto
        }
    }
}