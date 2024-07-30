#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    //public DialogueTrigger trigger;
    bool gebied;
    public Quest? quest;
    public QuestManager? questManager;

    void OnMouseDown()
    {
        if (gebied)
        {
            if(quest != null) {
                Debug.Log(quest.data.Name);
                //questManager.StartQuest(quest);
            }
            //trigger.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gebied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gebied = false;
    }
}
