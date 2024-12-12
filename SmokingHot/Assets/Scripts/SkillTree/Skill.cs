using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Skill : MonoBehaviour
{
    public string skillName;
    public string skillDescription;
    public Sprite icon;
    public int cost;
    public List<Skill> prerequisites;
    public bool isUnlocked;
    public List<string> effects;
    public bool tierUp;

    public Skill(string skillName, string skillDesc, int skillCost)
    {
        this.skillName = skillName;
        this.skillDescription = skillDesc;
        this.cost = skillCost;
    }
}

public class VirtualSkill
{
    public string skillName;
    public string skillDescription;
    public int cost;
    public List<VirtualSkill> prerequisites;
    public bool isUnlocked;
    public List<string> effects;
    public bool tierUp;

    public VirtualSkill(Skill skillToCopy)
    {
        skillName = skillToCopy.skillName;
        skillDescription = skillToCopy.skillDescription;
        cost = skillToCopy.cost;
        isUnlocked = skillToCopy.isUnlocked;
        effects = skillToCopy.effects;
        tierUp = skillToCopy.tierUp;

        prerequisites = new List<VirtualSkill>();
        foreach (Skill prerequisite in skillToCopy.prerequisites)
        {
            prerequisites.Add(new VirtualSkill(prerequisite));
        }
    }
}