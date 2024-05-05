using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CraftRecipe : ScriptableObject {
    public List<Item> ingredients;
    public Item result;
    public bool learned = false;
}
