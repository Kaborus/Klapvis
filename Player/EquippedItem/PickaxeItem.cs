using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeItem : ToolItem
{
    public override void Use(PlayerController playerController)
    {
        UseTool("PickaxeRequired");
    }
}
