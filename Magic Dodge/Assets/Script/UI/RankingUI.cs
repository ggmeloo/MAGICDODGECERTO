using UnityEngine;
using TMPro;

public class RankingUI : MonoBehaviour
{
    [Header("Referências da UI do Ranking")]
    public TextMeshProUGUI[] rankTexts; // Arraste seus 3 textos para cá no Inspector

    void Start()
    {
        // Chama a função para exibir o ranking assim que a cena carregar
        DisplayRanking();
    }

    private void DisplayRanking()
    {
        // Carrega os dados salvos do ranking
        RankingData rankingData = RankingManager.GetRanking();

        // Loop pelos 3 campos de texto que temos
        for (int i = 0; i < rankTexts.Length; i++)
        {
            // Verifica se existe uma entrada no ranking para esta posição
            if (i < rankingData.scores.Count)
            {
                // Se houver, formata e exibe o nome e a pontuação
                ScoreEntry entry = rankingData.scores[i];
                rankTexts[i].text = $"{i + 1}. {entry.playerName} - {entry.score:D6}";
            }
            else
            {
                // Se não houver, exibe um texto padrão
                rankTexts[i].text = $"{i + 1}. ---";
            }
        }
    }
}
