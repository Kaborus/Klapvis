using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Mob
{
    public Kind kind;
}

[System.Serializable]
public enum Kind { None, Insect, Reptile, Amphibian, Mammal, Avian }
