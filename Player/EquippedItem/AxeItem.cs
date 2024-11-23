using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeItem : ToolItem
{
    public override void Use(PlayerController playerController)
    {
        UseTool("AxeRequired");
    }
}
