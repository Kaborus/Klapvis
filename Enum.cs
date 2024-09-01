public enum Character
{
    None,
    Human,
    Goblin,
    Dwarf,
    Giant
}

public enum ItemCategory
{
    None,
    Material,
    MeleeWeapon,
    Bow,
    Gun,
    Pickaxe,
    Axe,
    HeadGear,
    BodyGear,
    HandGear,
    LegGear,
    FootGear,
    Consumable,
    Arrow,
    Bullet
}

public enum EquippedItem
{
    None,
    Melee,
    Bow,
    Gun,
    Pickaxe,
    Axe,
    Consumable
}

public enum SlotType
{
    Normal,
    //Armor,
    HeadGear,
    BodyGear,
    HandGear,
    LegGear,
    FootGear,
}

public enum MobBehaviour
{
    Passive,
    Neutral,
    Aggressive
}

public enum MobState
{
    Wander,
    Attack,
    Chase,
    Retreat
}

public enum MobAttack
{
    None,
    Melee,
    Range
}

public enum QuestStatus
{
    Inactive,
    Active,
    Completed
}
