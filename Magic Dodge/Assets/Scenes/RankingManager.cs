using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Essencial para ordenar a lista

// ------------ ESTRUTURAS DE DADOS ------------
// Estas duas classes definem como os dados serão organizados.
// Elas precisam ser marcadas como [System.Serializable] para o Unity convertê-las para JSON.

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class RankingData
{
    // A lista que conterá todas as pontuações.
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}


// ------------ O GERENCIADOR ESTÁTICO ------------
// Esta é a classe principal que faz todo o trabalho.
// "static" significa que não precisamos adicioná-la a um objeto na cena.
public static class RankingManager
{
    private const string RankingKey = "PlayerRanking_v1"; // Usamos uma chave para salvar no PlayerPrefs

    // Função para adicionar uma nova pontuação
    public static void AddScore(string playerName, int score)
    {
        // 1. Carrega o ranking que já existe
        RankingData rankingData = GetRanking();

        // 2. Adiciona a nova pontuação à lista
        rankingData.scores.Add(new ScoreEntry { playerName = playerName, score = score });

        // 3. Ordena a lista da maior pontuação para a menor
        rankingData.scores = rankingData.scores.OrderByDescending(s => s.score).ToList();

        // 4. Converte o objeto de volta para uma string em formato JSON
        string json = JsonUtility.ToJson(rankingData);
        Debug.Log($"<color=lime>PASSO 2: JSON sendo salvo no PlayerPrefs: {json}</color>");
        // 5. Salva essa string no PlayerPrefs
        PlayerPrefs.SetString(RankingKey, json);
        PlayerPrefs.Save();
    }

    // Função para carregar todas as pontuações salvas
    public static RankingData GetRanking()
    {
        // Pega a string JSON salva, se ela existir
        string json = PlayerPrefs.GetString(RankingKey, null);

        // Se não houver nada salvo, retorna uma lista nova e vazia
        if (string.IsNullOrEmpty(json))
        {
            return new RankingData();
        }

        // Se houver dados, converte a string JSON de volta para nosso objeto RankingData
        return JsonUtility.FromJson<RankingData>(json);
    }
}