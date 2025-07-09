// ScoreUI.cs (MODIFICADO)
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [Header("Componentes da UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    [Header("Pop-up de Pontos")]
    public GameObject pointsPopupPrefab;
    public Transform popupCanvas;

    void Start()
    {
        // Se inscreve nos eventos do ScoreManager2
        if (ScoreManager2.instance != null) // <--- MUDAN�A AQUI
        {
            ScoreManager2.instance.OnScoreChanged += UpdateScoreText; // <--- MUDAN�A AQUI
            ScoreManager2.instance.OnPointsGained += CreatePointsPopup; // <--- MUDAN�A AQUI
        }

        // Atualiza os textos iniciais
        UpdateScoreText(ScoreManager2.instance.currentScore); // <--- MUDAN�A AQUI
        if (highScoreText != null)
        {
            highScoreText.text = $"HI: {ScoreManager2.instance.highScore}"; // <--- MUDAN�A AQUI
        }
    }

    void OnDestroy()
    {
        // Se desinscreve para evitar erros
        if (ScoreManager2.instance != null) // <--- MUDAN�A AQUI
        {
            ScoreManager2.instance.OnScoreChanged -= UpdateScoreText; // <--- MUDAN�A AQUI
            ScoreManager2.instance.OnPointsGained -= CreatePointsPopup; // <--- MUDAN�A AQUI
        }
    }

    // O resto do script (UpdateScoreText, CreatePointsPopup) continua o mesmo...
    private void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = newScore.ToString("D6");
        }
    }

    private void CreatePointsPopup(int points, Vector3 worldPosition)
    {
        if (pointsPopupPrefab == null || popupCanvas == null)
        {
            Debug.LogError("Prefab do Pop-up ou Canvas n�o configurado no ScoreUI!", this);
            return;
        }

        // Instancia o pop-up no Canvas. � CRUCIAL que o segundo argumento seja o Transform do Canvas.
        GameObject popupInstance = Instantiate(pointsPopupPrefab, popupCanvas);

        // --- IN�CIO DA CORRE��O ---
        // For�a a escala para (1, 1, 1) para garantir que seja vis�vel.
        popupInstance.transform.localScale = Vector3.one;
        // --- FIM DA CORRE��O ---

        // Converte a posi��o do mundo (onde o inimigo morreu) para a posi��o na tela.
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        popupInstance.transform.position = screenPosition;

        // Define o texto do pop-up
        TextMeshProUGUI popupText = popupInstance.GetComponent<TextMeshProUGUI>();
        if (popupText != null)
        {
            popupText.text = $"+{points}";
        }
        else
        {
            Debug.LogWarning("O prefab do Pop-up n�o tem um componente TextMeshProUGUI!", popupInstance);
        }
    }

}