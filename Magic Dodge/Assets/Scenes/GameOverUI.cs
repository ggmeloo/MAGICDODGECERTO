using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Para os textos
using UnityEngine.UI; // Para o bot�o e o InputField

// Responsabilidade: Mostrar o score final e salvar o resultado.
public class GameOverUI : MonoBehaviour
{
    [Header("Refer�ncias da UI")]
    public TextMeshProUGUI scoreText;
    public TMP_InputField nameInputField;
    public Button submitButton;

    private int finalScore;

    void Start()
    {
        // Vamos garantir que scoreText n�o � nulo antes de us�-lo
        if (scoreText == null)
        {
            Debug.LogError("Refer�ncia 'scoreText' n�o est� conectada no Inspector!", this);
            return;
        }
        if (nameInputField == null)
        {
            Debug.LogError("Refer�ncia 'nameInputField' n�o est� conectada no Inspector!", this);
            return;
        }

        if (ScoreManager2.instance != null)
        {
            finalScore = ScoreManager2.instance.currentScore;
        }

        scoreText.text = "PONTUA��O: " + finalScore.ToString("000000");
        nameInputField.Select();
    }

    public void SubmitAndReturnToMenu()
    {
        string playerName = nameInputField.text;

        // N�o faz nada se o nome estiver vazio.
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("O nome est� vazio. N�o foi salvo.");
            return;
        }
        Debug.Log($"<color=cyan>PASSO 1: Tentando salvar no Ranking. Nome='{playerName}', Score='{finalScore}'</color>");
        // Salva o High Score atrav�s do ScoreManager, se necess�rio.
        if (ScoreManager2.instance != null)
        {
            ScoreManager2.instance.SaveHighScore();
        }

       RankingManager.AddScore(playerName, finalScore);

        // Volta para a cena do menu principal para completar o ciclo.
        SceneManager.LoadScene("MenuPrincipal");
    }
}