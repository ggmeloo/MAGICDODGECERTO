// ===== MainMenuUI.cs (VERS�O FINAL E CORRIGIDA) =====

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Ranking UI")]
    public TextMeshProUGUI[] rankTexts; // Arraste seus 3 textos do ranking para c�

    [Header("Menu Navigation")]
    public TextMeshProUGUI[] menuOptions; // Arraste os textos "JOGAR" e "SAIR" aqui
    public GameObject cursor;             // Arraste o objeto de texto do cursor ">" aqui
    private int currentOption = 0;

    void Start()
    {
        // Chama as fun��es para configurar o menu quando a cena come�a
        DisplayRanking();
        UpdateCursorPosition();
    }

    void Update()
    {
        // Verifica a entrada do teclado a cada frame
        HandleMenuNavigation();
    }

    // --- SEU C�DIGO DE NAVEGA��O (EST� PERFEITO, SEM MUDAN�AS) ---
    private void HandleMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentOption = (currentOption + 1) % menuOptions.Length;
            UpdateCursorPosition();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentOption--;
            if (currentOption < 0)
            {
                currentOption = menuOptions.Length - 1;
            }
            UpdateCursorPosition();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) // Adicionei Enter como op��o
        {
            SelectOption();
        }
    }

    private void UpdateCursorPosition()
    {
        if (cursor != null && menuOptions.Length > 0)
        {
            Vector3 targetPosition = menuOptions[currentOption].transform.position;
            cursor.transform.position = new Vector3(targetPosition.x - 1.5f, targetPosition.y, targetPosition.z);
        }
    }

    private void SelectOption()
    {
        if (currentOption == 0) StartGame();
        else if (currentOption == 1) QuitGame();
    }

    public void StartGame()
    {
        // Se assegure que ScoreManager2 existe para resetar o score
        if (ScoreManager2.instance != null)
        {
            ScoreManager2.instance.ResetScore();
        }
        SceneManager.LoadScene("Jogo"); // Use o nome correto da sua cena de jogo
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

    // --- FUN��O DO RANKING (CORRIGIDA) ---
    void DisplayRanking()
    {
        // CORRE��O 1: A classe se chama "RankingData", e a fun��o � "GetRanking()".
        RankingData rankingData = RankingManager.GetRanking();


        Debug.Log($"<color=yellow>PASSO 3: Carregado {rankingData.scores.Count} registros do ranking.</color>");

        // Loop para preencher os textos do ranking
        for (int i = 0; i < rankTexts.Length; i++)
        {
            // Verifica se h� pontua��o salva para esta posi��o (ex: 1�, 2�, 3� lugar)
            if (i < rankingData.scores.Count)
            {
                // Se houver, pega a entrada
                ScoreEntry entry = rankingData.scores[i];

                // CORRE��O 2: O campo de nome � "playerName", n�o "name".
                // Formata o texto para exibir a posi��o, nome e pontua��o.
                rankTexts[i].text = $"{i + 1}. {entry.playerName} - {entry.score:D6}";
            }
            else
            {
                // Se n�o houver pontua��o para esta posi��o, mostra um texto padr�o.
                rankTexts[i].text = $"{i + 1}. ---";
            }
        }
    }
}