// ===== MusicManager.cs (Para música contínua entre cenas) =====

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Uma variável estática para manter a referência a este objeto
    private static MusicManager instance;

    void Awake()
    {
        // --- LÓGICA DO SINGLETON ---
        // Singleton é um padrão que garante que exista apenas UMA instância de um objeto.

        // 1. Verifica se já existe uma instância do MusicManager
        if (instance == null)
        {
            // Se não existe, esta se torna a instância principal
            instance = this;
            // E diz para a Unity para NÃO destruir este objeto ao carregar novas cenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se uma instância JÁ EXISTE (ex: você voltou para o menu principal),
            // então esta nova instância é duplicada e deve ser destruída.
            Destroy(gameObject);
        }
    }
}