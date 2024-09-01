using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public DialogueTrigger trigger;
    bool inRange;
    public Quest quest;
    public Npc nextQuestGiver;

    void OnMouseDown()
    {
        Debug.Log(quest.questStatus);
        if (inRange)
        {
            if (quest != null)
            {
                GameManager.instance.questManager.StartQuest(quest);
            }

            Debug.Log(quest.questStatus);

            trigger.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
