using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<Item> ingredients;
    public Item result;
    public int amount = 1;
    public bool learned = false;
}
