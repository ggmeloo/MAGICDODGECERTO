// ===== MainMenuUI.cs (VERSÃO FINAL E CORRIGIDA) =====

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Ranking UI")]
    public TextMeshProUGUI[] rankTexts; // Arraste seus 3 textos do ranking para cá

    [Header("Menu Navigation")]
    public TextMeshProUGUI[] menuOptions; // Arraste os textos "JOGAR" e "SAIR" aqui
    public GameObject cursor;             // Arraste o objeto de texto do cursor ">" aqui
    private int currentOption = 0;

    void Start()
    {
        // Chama as funções para configurar o menu quando a cena começa
        DisplayRanking();
        UpdateCursorPosition();
    }

    void Update()
    {
        // Verifica a entrada do teclado a cada frame
        HandleMenuNavigation();
    }

    // --- SEU CÓDIGO DE NAVEGAÇÃO (ESTÁ PERFEITO, SEM MUDANÇAS) ---
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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) // Adicionei Enter como opção
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

    // --- FUNÇÃO DO RANKING (CORRIGIDA) ---
    void DisplayRanking()
    {
        // CORREÇÃO 1: A classe se chama "RankingData", e a função é "GetRanking()".
        RankingData rankingData = RankingManager.GetRanking();


        Debug.Log($"<color=yellow>PASSO 3: Carregado {rankingData.scores.Count} registros do ranking.</color>");

        // Loop para preencher os textos do ranking
        for (int i = 0; i < rankTexts.Length; i++)
        {
            // Verifica se há pontuação salva para esta posição (ex: 1º, 2º, 3º lugar)
            if (i < rankingData.scores.Count)
            {
                // Se houver, pega a entrada
                ScoreEntry entry = rankingData.scores[i];

                // CORREÇÃO 2: O campo de nome é "playerName", não "name".
                // Formata o texto para exibir a posição, nome e pontuação.
                rankTexts[i].text = $"{i + 1}. {entry.playerName} - {entry.score:D6}";
            }
            else
            {
                // Se não houver pontuação para esta posição, mostra um texto padrão.
                rankTexts[i].text = $"{i + 1}. ---";
            }
        }
    }
}