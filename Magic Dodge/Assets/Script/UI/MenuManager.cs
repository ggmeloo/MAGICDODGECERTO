using UnityEngine;
using UnityEngine.SceneManagement;

// Responsabilidade: Controlar os botões do menu principal.
public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Garante que o ScoreManager exista antes de usá-lo.
        if (ScoreManager2.instance != null)
        {
            // Zera a pontuação da partida anterior antes de começar uma nova.
            ScoreManager2.instance.ResetScore();
        }

        // Carrega a cena principal do jogo.
        SceneManager.LoadScene("Jogo"); // Certifique-se que "Jogo" é o nome exato do arquivo da cena.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}