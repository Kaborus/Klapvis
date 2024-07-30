using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public Category category;
}

[System.Serializable]
public enum Category
    {
        None,
        Material,
        Melee,
        Range,
        Pickaxe,
        Axe,
        Armor,
        Consumable,
        Arrow,
    }
    