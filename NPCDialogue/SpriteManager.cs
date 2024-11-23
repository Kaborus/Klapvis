using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;
    private Dictionary<int, Sprite> intToSpriteDict = new Dictionary<int, Sprite>();

    private void Awake()
    {
        int number = 0;
        foreach (Sprite sprite in sprites)
        {
            AddSprite(number++, sprite);
        }
    }

    private void AddSprite(int number, Sprite sprite)
    {
        if (!intToSpriteDict.ContainsKey(number))
        {
            intToSpriteDict.Add(number, sprite);
        }
    }

    public Sprite GetSpriteById(int key)
    {
        if (intToSpriteDict.ContainsKey(key))
        {
            return intToSpriteDict[key];
        }
        return null;
    }
}
