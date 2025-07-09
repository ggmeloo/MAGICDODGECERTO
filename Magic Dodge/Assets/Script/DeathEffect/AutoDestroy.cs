// AutoDestroy.cs (Script Gen�rico para Efeitos)
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Tooltip("Tempo de espera para destruir, caso n�o haja Animator ou ParticleSystem.")]
    public float fallbackDelay = 3f;

    void Start()
    {
        float lifetime = 0f;

        // 1. Tenta usar o ParticleSystem para definir o tempo de vida
        if (TryGetComponent<ParticleSystem>(out var particles))
        {
            // O tempo de vida total � a dura��o da emiss�o + o tempo de vida da part�cula mais longa.
            // Adicionamos um pequeno buffer para garantir que tudo desapareceu.
            lifetime = particles.main.duration + particles.main.startLifetime.constantMax + 0.5f;
        }
        // 2. Se n�o houver part�culas, tenta usar o Animator
        else if (TryGetComponent<Animator>(out var animator))
        {
            // Pega a dura��o do clipe de anima��o que est� tocando no estado atual
            lifetime = animator.GetCurrentAnimatorStateInfo(0).length;
        }
        // 3. Se n�o houver nenhum dos dois, usa o tempo de espera manual
        else
        {
            lifetime = fallbackDelay;
            Debug.LogWarning($"O objeto '{name}' n�o tem ParticleSystem nem Animator. Usando o delay de fallback: {fallbackDelay}s.", this);
        }

        // Agenda a destrui��o do objeto (o efeito) ap�s o tempo de vida calculado
        Destroy(gameObject, lifetime);
    }
}