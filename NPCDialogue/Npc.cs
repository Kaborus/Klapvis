using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public DialogueGroup[] dialogueGroups;
    public int dialogueId;
    public string npcName;
    public DialogueTrigger trigger;
    bool inRange;

    private void OnMouseDown()
    {
        if (inRange)
        {
            trigger.StartDialogue(dialogueId, npcName);
        }
    }

    public void GetNextDialogue() => dialogueId *= 2;

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
