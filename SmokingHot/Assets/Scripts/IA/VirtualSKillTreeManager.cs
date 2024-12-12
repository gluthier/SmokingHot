using System;
using System.Collections.Generic;
using UnityEngine;

public class VirtualSkillTreeManager : MonoBehaviour
{
    public GameManager gameManager;
    public Dictionary<Building.TYPE, List<VirtualSkill>> iaVirtualTree;

    public void Init()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        InitVirtualTree();
    }

    private void InitVirtualTree()
    {
        iaVirtualTree = new Dictionary<Building.TYPE, List<VirtualSkill>>();

        foreach (Transform skillTree in
            FindFirstObjectByType<SkillTreeManager>().transform)
        {
            SkillTreeUI uiSkillTree = skillTree.GetComponent<SkillTreeUI>();
            Building.TYPE skillTreeBuildingType = uiSkillTree.skillTreeBuildingType;

            Transform talentTree =
                uiSkillTree.transform.Find("Background/TalentTree").transform;

            List<VirtualSkill> virtualSkillList = new List<VirtualSkill>();

            foreach (Skill skill in talentTree.GetComponentsInChildren<Skill>())
            {
                // copy skill so it doesn't interfere with player skills
                virtualSkillList.Add(new VirtualSkill(skill));
            }

            iaVirtualTree.Add(skillTreeBuildingType, virtualSkillList);
        }
    }

    private bool CanUnlockVirtualSkill(VirtualSkill skill)
    {
        bool isPrerequisiteUnlocked = false;
        bool hasMoney = false;

        if (skill.isUnlocked) return false;
        if (skill.prerequisites.Count == 0) isPrerequisiteUnlocked = true;
        foreach (VirtualSkill prerequisite in skill.prerequisites)
        {
            if (prerequisite.isUnlocked)
            {
                isPrerequisiteUnlocked = true;
                break;
            }
        }

        hasMoney = gameManager.GetIAMoney() >= skill.cost;

        return isPrerequisiteUnlocked && hasMoney;
    }

    private void ApplyVirtualSkillEffect(VirtualSkill virtualSkill, Building.TYPE buildingType)
    {
        List<String> effect = virtualSkill.effects;
        effect.Add("Down money " + virtualSkill.cost);
        gameManager.HandleSkillEffect(effect, buildingType, false);
    }

    public void HandleIASkillTree(Building.TYPE skillTreeToInvest, CompanyEntity iaCompany)
    {
        List<VirtualSkill> virtualSkills;

        if (iaVirtualTree.TryGetValue(skillTreeToInvest, out virtualSkills))
        {
            foreach (VirtualSkill virtualSkill in virtualSkills)
            {
                if (virtualSkill.isUnlocked)
                    continue;

                if (iaCompany.GetMoney() > 
                    virtualSkill.cost * Env.MinCoefficientCostForIAToInvestSkill)
                {
                    if (CanUnlockVirtualSkill(virtualSkill))
                    {
                        virtualSkill.isUnlocked = true;
                        ApplyVirtualSkillEffect(virtualSkill, skillTreeToInvest);
                        return; // only invest in max one skill
                    }
                }
            }
        }
    }

    public void ResetAllVirtualTrees()
    {
        foreach (List<VirtualSkill> virtualSkills in iaVirtualTree.Values)
        {
            foreach(VirtualSkill virtualSkill in virtualSkills)
            {
                virtualSkill.isUnlocked = false;
            }
        }
    }
}