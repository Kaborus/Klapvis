using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance => _instance;

    private List<Quest> activeQuests = new List<Quest>();

    public List<Quest> allQuests = new List<Quest>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void Start() {
        foreach (var item in allQuests)
        {
            Debug.Log(item.data.Description);
        }
    }

    // Voeg een nieuwe quest toe aan de actieve quests
    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        Debug.Log($"Quest '{quest.data.Name}' toegevoegd.");
    }

    // Markeer een quest als voltooid
    public void CompleteQuest(Quest quest)
    {
        quest.finished = true;
        Debug.Log($"Quest '{quest.data.Name}' voltooid!");
        // Voeg hier beloningen, voortgangsupdates, enzovoort toe.
    }
}