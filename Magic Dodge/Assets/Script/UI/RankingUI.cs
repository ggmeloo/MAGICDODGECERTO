using UnityEngine;
using TMPro;

public class RankingUI : MonoBehaviour
{
    [Header("Refer�ncias da UI do Ranking")]
    public TextMeshProUGUI[] rankTexts; // Arraste seus 3 textos para c� no Inspector

    void Start()
    {
        // Chama a fun��o para exibir o ranking assim que a cena carregar
        DisplayRanking();
    }

    private void DisplayRanking()
    {
        // Carrega os dados salvos do ranking
        RankingData rankingData = RankingManager.GetRanking();

        // Loop pelos 3 campos de texto que temos
        for (int i = 0; i < rankTexts.Length; i++)
        {
            // Verifica se existe uma entrada no ranking para esta posi��o
            if (i < rankingData.scores.Count)
            {
                // Se houver, formata e exibe o nome e a pontua��o
                ScoreEntry entry = rankingData.scores[i];
                rankTexts[i].text = $"{i + 1}. {entry.playerName} - {entry.score:D6}";
            }
            else
            {
                // Se n�o houver, exibe um texto padr�o
                rankTexts[i].text = $"{i + 1}. ---";
            }
        }
    }
}
