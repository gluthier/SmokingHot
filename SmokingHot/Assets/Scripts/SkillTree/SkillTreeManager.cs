using UnityEngine;
using System.Collections.Generic;

public class SkillTreeManager : MonoBehaviour
{
    public List<SkillTree> skillTrees;
    public List<GameObject> panels;
    private GameObject lastActive;

    public GameObject cigSkillName;
    public GameObject cigSkillDesc;
    
    public GameObject pubSkillName;
    public GameObject pubSkillDesc;
    
    public GameObject popSkillName;
    public GameObject popSkillDesc;
    
    public GameObject lobSkillName;
    public GameObject lobSkillDesc;

    void Start()
    {
    }
    
    public void UnlockSkill(SkillTreeManager skillTree, Skill skill)
    {
        if(CanUnlockSkill(skillTree, skill))
        {
            skill.isUnlocked = true;
            ApplySkillEffect(skill);
        }
    }

    public void SetSkillActive(GameObject node)
    {
        node.transform.GetChild(0).gameObject.SetActive(true);

        if (lastActive != null && lastActive != node) {
            lastActive.transform.GetChild(0).gameObject.SetActive(false);
            lastActive = node;

            Skill skill = node.GetComponent<Skill>();
        }
    }

    private bool CanUnlockSkill(SkillTreeManager skillTree, Skill skill)
    {
        if (skill.isUnlocked) return false;
        foreach (Skill prerequisite in skill.prerequisites)
        {
            if (!prerequisite.isUnlocked) return false;
        }
        //TODO check money
        return true;
    }

    private void ApplySkillEffect(Skill skill)
    {
        switch (skill.effect)
        {
            /*case "ChangeBuildPrefab":
                FindObjectOfType<BuildingsManager>().ChangeBuildPrefab(skill, "buildingName");
                break;*/
            // Add other cases for different effects as needed
            default:
                Debug.Log($"{skill.skillName} effect applied.");
                break;
        }
    }

    public void ShowPanel(GameObject o)
    {
        Building b = o.GetComponent<Building>();

        int index = 0;

        switch (b.type)
        {
            case Building.TYPE.CIGARETTE:
                index = 0;
                break;
            case Building.TYPE.PUBLICITY:
                index = 1;
                break;
            case Building.TYPE.LOBBYING:
                index = 2;
                break;
            case Building.TYPE.REPUTATION:
                index = 3;
                break;

        }
        Debug.Log(panels[index].name);
        panels[index].SetActive(true);
    }
}
