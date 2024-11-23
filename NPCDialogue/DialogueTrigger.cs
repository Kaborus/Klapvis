using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void StartDialogue(int dialogueId, string name)
    {
        FindObjectOfType<DialogueBox>().OpenDialogue(dialogueId, name);
    }
}
