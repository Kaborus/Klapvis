using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolItem : IEquippedItem
{
    public float toolTimer = 0f;
    public virtual void Use(PlayerController playerController)
    {
        System.Console.WriteLine("No tool");
    }

    public void UseTool(string tag)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(tag))
            {
                toolTimer += Time.deltaTime;

                if (toolTimer >= 1)
                {
                    hit.collider.gameObject.GetComponent<ResourceNode>().GiveResource();
                    toolTimer = 0f;
                }
            }
        }
    }
}
