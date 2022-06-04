using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller INSTANCE;

    public TextMeshProUGUI scoreText;
    private readonly IDictionary<string, List<GameObject>> textToEnemyMappings = new Dictionary<string, List<GameObject>>();
    private int _score = 0;

    void Start()
    {
        INSTANCE = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {_score}";
    }

    public void Register(string text, GameObject enemy)
    {
        if (!textToEnemyMappings.ContainsKey(text))
        {
            textToEnemyMappings[text] = new List<GameObject>();
        }
        textToEnemyMappings[text].Add(enemy);
    }

    public void UnRegister(GameObject enemy)
    {
        foreach (var mapping in textToEnemyMappings)
        {
            if (mapping.Value.Contains(enemy))
            {
                mapping.Value.Remove(enemy);

                // No enemy left in this group
                if (mapping.Value.Count == 0)
                {
                    textToEnemyMappings.Remove(mapping.Key);
                }
                return;
            }
        }
    }

    public bool CastSpell(string text)
    {
        if (textToEnemyMappings.ContainsKey(text) && textToEnemyMappings[text].Any())
        {
            foreach (var enemy in textToEnemyMappings[text])
            {
                Destroy(enemy);
                _score++;
            }
            textToEnemyMappings.Remove(text);
            return true;
        }

        return false;
    }
}
