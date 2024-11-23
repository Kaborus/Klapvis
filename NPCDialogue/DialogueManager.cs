using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueGroup[] dialogueGroups;

    private void Start()
    {
        LoadDialogueData();
    }

    public void LoadDialogueData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dialogues.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueGroupList dialogueGroupList = JsonUtility.FromJson<DialogueGroupList>(json);
            dialogueGroups = dialogueGroupList.dialogueGroups;
        }
        else
        {
            Debug.LogError("Dialogue file not found");
        }
    }

    public DialogueGroup GetDialogueGroupById(int id)
    {
        return System.Array.Find(dialogueGroups, group => group.id == id);
    }

    public void SetDialogue(int id)
    {
        switch (id)
        {
            case 2:
                GameManager.instance.guide.GetNextDialogue();
                GameManager.instance.farmer.GetNextDialogue();
                break;
        }
    }
}
