using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    private Dictionary<string, Quest> nameToQuestDict = new Dictionary<string, Quest>();

    private void Awake()
    {
        foreach (Quest quest in quests)
        {
            AddQuest(quest);
        }
    }

    private void AddQuest(Quest quest)
    {
        if (!nameToQuestDict.ContainsKey(quest.data.questName))
        {
            nameToQuestDict.Add(quest.data.questName, quest);
        }
    }

    public Quest GetQuestByName(string key)
    {
        if (nameToQuestDict.ContainsKey(key))
        {
            return nameToQuestDict[key];
        }
        return null;
    }

    public void StartQuest(Quest quest)
    {
        if (nameToQuestDict.ContainsKey(quest.data.questName))
        {
            nameToQuestDict[quest.data.questName].questStatus = QuestStatus.Completed;
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (nameToQuestDict.ContainsKey(quest.data.questName))
        {
            nameToQuestDict[quest.data.questName].questStatus = QuestStatus.Completed;
        }
    }
}
