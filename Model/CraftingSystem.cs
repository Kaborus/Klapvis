// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CraftingSystem : MonoBehaviour
// {
//     public Inventory playerInventory;

//     public List<CraftRecipe> craftingRecipes;

//     public void CraftItem(string itemName)
//     {
//         CraftingRecipe recipe = GetCraftingRecipe(itemName);
//         if (recipe != null && CheckIngredients(recipe))
//         {
//             foreach (string ingredientName in recipe.ingredients)
//             {
//                 playerInventory.Remove(index);
//             }
//             playerInventory.Add(recipe.result);
//         }
//         else
//         {
//             Debug.Log("Cannot craft item: Insufficient ingredients or recipe not found.");
//         }
//     }

//     public Item CraftItem(CraftRecipe recipe) {
//         return recipe.result;
//     }

//     private CraftRecipe GetCraftingRecipe(string itemName)
//     {
//         return craftingRecipes.Find(recipe => recipe.result.itemName == itemName);
//     }

//     private bool CheckIngredients(CraftRecipe recipe)
//     {
//         foreach (string ingredientName in recipe.ingredients)
//         {
//             if (!playerInventory.HasItem(ingredientName))
//             {
//                 return false;
//             }
//         }
//         return true;
//     }
// }
