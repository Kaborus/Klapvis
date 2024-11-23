using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public int id;
    public string npcName;
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    DialogueGroup currentMessages;
    private int activeMessage = 0;
    public static bool isActive = false;

    private void Start()
    {
        ToggleBox();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            NextMessage();
        }

        ToggleBox();
    }

    public void OpenDialogue(int dialogueGroupId, string name)
    {
        ToggleBox();
        id = dialogueGroupId;
        DialogueGroup group = GameManager.instance.dialogueManager.GetDialogueGroupById(dialogueGroupId);
        if (group != null)
        {
            currentMessages = group;
            this.npcName = name;

            activeMessage = 0;
            isActive = true;
            DisplayMessage();
        }
        else
        {
            Debug.LogWarning("DialogueGroup not found for dialogueGroupId: " + dialogueGroupId);
        }
    }

    private void DisplayMessage()
    {
        GameManager.instance.player.movement.EnableMovement(false);
        Dialogue[] messageToDisplay = currentMessages.dialogues;

        messageText.text = messageToDisplay[activeMessage].text;

        actorName.text = npcName;
        actorImage.sprite = GameManager.instance.spriteManager.GetSpriteById(messageToDisplay[activeMessage].id);
    }

    private void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.dialogues.Length)
        {
            DisplayMessage();
        }
        else
        {
            GameManager.instance.player.movement.EnableMovement(true);
            isActive = false;
            ToggleBox();

            GameManager.instance.dialogueManager.SetDialogue(id);
        }
    }

    private void ToggleBox()
    {
        if (isActive)
        {
            backgroundBox.transform.localScale = new Vector3(0.4677083f, 0.4677083f, 0.4677083f);
        }
        else
        {
            backgroundBox.transform.localScale = Vector3.zero;
        }
    }
}
