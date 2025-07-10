using UnityEngine;
using UnityEngine.SceneManagement;

// Responsabilidade: Controlar os bot�es do menu principal.
public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Garante que o ScoreManager exista antes de us�-lo.
        if (ScoreManager2.instance != null)
        {
            // Zera a pontua��o da partida anterior antes de come�ar uma nova.
            ScoreManager2.instance.ResetScore();
        }

        // Carrega a cena principal do jogo.
        SceneManager.LoadScene("Jogo"); // Certifique-se que "Jogo" � o nome exato do arquivo da cena.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}