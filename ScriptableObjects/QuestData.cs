using UnityEngine;

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    public int Id;
    public string Name;
    [TextArea]
    public string Description;
}