using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSkillTree", menuName = "SkillTree/SkillTreeData")]
public class SkillTree : ScriptableObject
{
    public string treeName;
    public List<Skill> skills;

}
