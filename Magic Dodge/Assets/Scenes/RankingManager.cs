using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Essencial para ordenar a lista

// ------------ ESTRUTURAS DE DADOS ------------
// Estas duas classes definem como os dados ser�o organizados.
// Elas precisam ser marcadas como [System.Serializable] para o Unity convert�-las para JSON.

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class RankingData
{
    // A lista que conter� todas as pontua��es.
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}


// ------------ O GERENCIADOR EST�TICO ------------
// Esta � a classe principal que faz todo o trabalho.
// "static" significa que n�o precisamos adicion�-la a um objeto na cena.
public static class RankingManager
{
    private const string RankingKey = "PlayerRanking_v1"; // Usamos uma chave para salvar no PlayerPrefs

    // Fun��o para adicionar uma nova pontua��o
    public static void AddScore(string playerName, int score)
    {
        // 1. Carrega o ranking que j� existe
        RankingData rankingData = GetRanking();

        // 2. Adiciona a nova pontua��o � lista
        rankingData.scores.Add(new ScoreEntry { playerName = playerName, score = score });

        // 3. Ordena a lista da maior pontua��o para a menor
        rankingData.scores = rankingData.scores.OrderByDescending(s => s.score).ToList();

        // 4. Converte o objeto de volta para uma string em formato JSON
        string json = JsonUtility.ToJson(rankingData);
        Debug.Log($"<color=lime>PASSO 2: JSON sendo salvo no PlayerPrefs: {json}</color>");
        // 5. Salva essa string no PlayerPrefs
        PlayerPrefs.SetString(RankingKey, json);
        PlayerPrefs.Save();
    }

    // Fun��o para carregar todas as pontua��es salvas
    public static RankingData GetRanking()
    {
        // Pega a string JSON salva, se ela existir
        string json = PlayerPrefs.GetString(RankingKey, null);

        // Se n�o houver nada salvo, retorna uma lista nova e vazia
        if (string.IsNullOrEmpty(json))
        {
            return new RankingData();
        }

        // Se houver dados, converte a string JSON de volta para nosso objeto RankingData
        return JsonUtility.FromJson<RankingData>(json);
    }
}