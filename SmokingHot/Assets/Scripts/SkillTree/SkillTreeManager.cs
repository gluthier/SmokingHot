using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public List<SkillTree> skillTrees;
    public List<GameObject> panels;
    private GameObject lastActive;

    public TextMeshProUGUI cigSkillName;
    public TextMeshProUGUI cigSkillDesc;
    
    public TextMeshProUGUI pubSkillName;
    public TextMeshProUGUI pubSkillDesc;
    
    public TextMeshProUGUI popSkillName;
    public TextMeshProUGUI popSkillDesc;
    
    public TextMeshProUGUI lobSkillName;
    public TextMeshProUGUI lobSkillDesc;

    public bool potDeVin = false; //TODO more

    public int[] tiers = {1,1,1,1};

    void Start()
    {
       
    }
    
    public void UnlockSkill()
    {
        Skill skill = node.GetComponent<Skill>();
        if(CanUnlockSkill(skill))
        {
            skill.isUnlocked = true;

            if (skill.tierUp)
            {
                tiers[index]++;
            }

            ApplySkillEffect(skill);
        }
    }

    public void SetSkillActive(GameObject node)
    {
        node.transform.GetChild(0).gameObject.SetActive(true);

        if (lastActive != null && lastActive != node) {
            lastActive.transform.GetChild(0).gameObject.SetActive(false);
            lastActive = node;            
        }

        Skill skill = node.GetComponent<Skill>();
        int index = GetCurrentActivePanel();
        SetSkillDesc(skill, index);

    }

    private void SetSkillDesc(Skill skill, int index)
    {
        switch (index)
        {
            case 0: // cig
                cigSkillDesc.text = skill.skillDescription;
                cigSkillName.text = skill.skillName;
                break;
            case 1: // pub
                pubSkillDesc.text = skill.skillDescription;
                pubSkillName.text = skill.skillName;
                break;
            case 2: // lobby
                lobSkillDesc.text = skill.skillDescription;
                lobSkillName.text = skill.skillName;
                break;
            case 4: // rep
                popSkillDesc.text = skill.skillDescription;
                popSkillName.text = skill.skillName;
                break;
            default:
                break;
        }
    }
    private bool CanUnlockSkill(Skill skill)
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
        string[] skillParts = skill.effect.Split(" ");

        switch (skillParts[0])
        {
            case "Unlock":
                potDeVin = true;
                break;
            case "Upgrade":
                // stat += skillParts[1]
            // Add other cases for different effects as needed
            default:
                Debug.Log($"{skill.skillName} effect applied.");
                break;
        }
    }

    private int GetCurrentActivePanel()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if(panels[i].activeSelf) return i;
        }

        return -1;
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
        
        panels[index].SetActive(true);
    }
}
