// EnemyProjectile.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet4 : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 4f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // O proj�til � instanciado j� apontando para o jogador, ent�o 'transform.right' � a dire��o correta.
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Proj�til inimigo atingiu o jogador!");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
            Destroy(gameObject);
        }
    }
}