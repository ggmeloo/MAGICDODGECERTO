// ===== MusicManager.cs (Para m�sica cont�nua entre cenas) =====

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Uma vari�vel est�tica para manter a refer�ncia a este objeto
    private static MusicManager instance;

    void Awake()
    {
        // --- L�GICA DO SINGLETON ---
        // Singleton � um padr�o que garante que exista apenas UMA inst�ncia de um objeto.

        // 1. Verifica se j� existe uma inst�ncia do MusicManager
        if (instance == null)
        {
            // Se n�o existe, esta se torna a inst�ncia principal
            instance = this;
            // E diz para a Unity para N�O destruir este objeto ao carregar novas cenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se uma inst�ncia J� EXISTE (ex: voc� voltou para o menu principal),
            // ent�o esta nova inst�ncia � duplicada e deve ser destru�da.
            Destroy(gameObject);
        }
    }
}