using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Para os textos
using UnityEngine.UI; // Para o botão e o InputField

// Responsabilidade: Mostrar o score final e salvar o resultado.
public class GameOverUI : MonoBehaviour
{
    [Header("Referências da UI")]
    public TextMeshProUGUI scoreText;
    public TMP_InputField nameInputField;
    public Button submitButton;

    private int finalScore;

    void Start()
    {
        // Vamos garantir que scoreText não é nulo antes de usá-lo
        if (scoreText == null)
        {
            Debug.LogError("Referência 'scoreText' não está conectada no Inspector!", this);
            return;
        }
        if (nameInputField == null)
        {
            Debug.LogError("Referência 'nameInputField' não está conectada no Inspector!", this);
            return;
        }

        if (ScoreManager2.instance != null)
        {
            finalScore = ScoreManager2.instance.currentScore;
        }

        scoreText.text = "PONTUAÇÃO: " + finalScore.ToString("000000");
        nameInputField.Select();
    }

    public void SubmitAndReturnToMenu()
    {
        string playerName = nameInputField.text;

        // Não faz nada se o nome estiver vazio.
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("O nome está vazio. Não foi salvo.");
            return;
        }
        Debug.Log($"<color=cyan>PASSO 1: Tentando salvar no Ranking. Nome='{playerName}', Score='{finalScore}'</color>");
        // Salva o High Score através do ScoreManager, se necessário.
        if (ScoreManager2.instance != null)
        {
            ScoreManager2.instance.SaveHighScore();
        }

       RankingManager.AddScore(playerName, finalScore);

        // Volta para a cena do menu principal para completar o ciclo.
        SceneManager.LoadScene("MenuPrincipal");
    }
}