using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using NUnit.Framework.Constraints;

public class SkillTreeManager : MonoBehaviour
{
    public List<SkillTreeUI> skillTreePanel;
    private GameObject lastActive;
    public GameManager gameManager;

    public TextMeshProUGUI pubSkillName;
    public TextMeshProUGUI pubSkillDesc;
    public TextMeshProUGUI pubSkillCost;
    public List<LineRenderer> pubLinks;

    public TextMeshProUGUI popSkillName;
    public TextMeshProUGUI popSkillDesc;
    public TextMeshProUGUI popSkillCost;
    public List<LineRenderer> popLinks;

    public TextMeshProUGUI cigSkillName;
    public TextMeshProUGUI cigSkillDesc;
    public TextMeshProUGUI cigSkillCost;
    public List<LineRenderer> cigLinks;

    public int[] tiers = {1,1,1};

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
    }

    public void UnlockSkill()
    {
        Skill skill = lastActive.GetComponent<Skill>();
        if(CanUnlockSkill(skill))
        {
            skill.isUnlocked = true;
            lastActive.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            int index = GetCurrentActivePanel();

            Link link = null;

            /*if(index == 0) { link = getTheLink(lastActive, getLinks(cigLinks)); }
            else if(index == 1) { link = getTheLink(lastActive, getLinks(pubLinks)); }
            else if(index == 2) { link = getTheLink(lastActive, getLinks(lobLinks)); }
            else if(index == 3) { link = getTheLink(lastActive, getLinks(popLinks)); }

            Color newColor = Color.green;

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(newColor, 0.0f), new GradientColorKey(newColor, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );

            link.GetComponent<LineRenderer>().colorGradient = gradient;*/

            if (skill.tierUp)
            {
                tiers[GetCurrentActivePanel()]++;
            }

            ApplySkillEffect(skill);
        }
    }

    private List<Link> getLinks(List<LineRenderer> lines)
    {
        List<Link> links = new List<Link>();
        foreach(LineRenderer l in lines)
        {
            links.Add(l.GetComponent<Link>());
        }

        return links;
    }
    public void SetSkillActive(GameObject node)
    {
        node.transform.GetChild(0).gameObject.SetActive(true);

        if (lastActive != null && lastActive != node && !lastActive.GetComponent<Skill>().isUnlocked) {
            lastActive.transform.GetChild(0).gameObject.SetActive(false);                     
        }

        lastActive = node;

        Skill skill = node.GetComponent<Skill>();
        int index = GetCurrentActivePanel();
        SetSkillDesc(skill, index);
    }

    private Link getTheLink(GameObject node, List<Link> links)
    {
        Link toReturn = null;
        foreach(Link l in links)
        {
            if(l.end == node.GetComponent<Skill>() && l.start.isUnlocked && !l.getActivated())
            {
                toReturn = l;
            }
        }
        return toReturn;
    }

    private void SetSkillDesc(Skill skill, int index)
    {
        switch (index)
        {
            case 0: // publicity
                pubSkillDesc.text = skill.skillDescription;
                pubSkillName.text = skill.skillName;
                pubSkillCost.text = "Prix: " + skill.cost;
                break;
            case 1: // popularity
                popSkillDesc.text = skill.skillDescription;
                popSkillName.text = skill.skillName;
                popSkillCost.text = "Prix: " + skill.cost;
                break;
            case 2: // manufacturing
                cigSkillDesc.text = skill.skillDescription;
                cigSkillName.text = skill.skillName;
                cigSkillCost.text = "Prix: " + skill.cost;
                break;
            default:
                break;
        }
    }

    private bool CanUnlockSkill(Skill skill)
    {
        bool isPrerequisiteUnlocked = false;
        bool hasMoney = false;

        if (skill.isUnlocked) return false;
        if (skill.prerequisites.Count == 0) isPrerequisiteUnlocked = true;
        foreach (Skill prerequisite in skill.prerequisites)
        {
            if (prerequisite.isUnlocked)
            {
                isPrerequisiteUnlocked = true;
                break;
            }
        }

        hasMoney = gameManager.GetPlayerMoney() >= skill.cost;

        return isPrerequisiteUnlocked && hasMoney;
    }

    private void ApplySkillEffect(Skill skill)
    {
        List<String> effect = skill.effects;
        effect.Add("Down money " + skill.cost);
        Building.TYPE buildingType =
            GetBuildingTypeFromIndex(GetCurrentActivePanel());

        gameManager.HandleSkillEffect(effect, buildingType, true);

    }

    public int GetCurrentActivePanel()
    {
        for (int i = 0; i < skillTreePanel.Count; i++)
        {
            if(skillTreePanel[i].gameObject.activeSelf) return i;
        }

        return -1;
    }

    public void ShowPanel(GameObject o)
    {
        Building b = o.GetComponent<Building>();

        int index = 0;

        switch (b.type)
        {
            case Building.TYPE.PUBLICITY:
                index = 0;
                break;
            case Building.TYPE.POPULARITY:
                index = 1;
                break;
            case Building.TYPE.MANUFACTURING:
                index = 2;
                break;

        }
        
        skillTreePanel[index].gameObject.SetActive(true);
    }

    public Building.TYPE GetBuildingTypeFromIndex(int index)
    {
        switch (index)
        {
            default:
            case 0:
                return Building.TYPE.PUBLICITY;
            case 1:
                return Building.TYPE.POPULARITY;
            case 2:
                return Building.TYPE.MANUFACTURING;
        }
    }

    public void ResetSkillTreeManager()
    {
        foreach (Transform skillTree in transform)
        {
            foreach (Skill skill in skillTree.GetComponentsInChildren<Skill>())
            {
                skill.isUnlocked = false;

                GameObject border = skill.transform.GetChild(0).gameObject;

                border.gameObject.GetComponent<UnityEngine.UI.Image>().color =
                    new Color32(96, 96, 96, 255);
                border.SetActive(false);
            }
        }

        CloseAllPanels();
    }

    public void ClosePanel()
    {
        int index = GetCurrentActivePanel();
        ResetSkillDesc(index);
        CloseAllPanels();
        lastActive = null;
        gameManager.PlaySimulation();
    }

    private void CloseAllPanels()
    {
        foreach (Transform skillTree in transform)
        {
            skillTree.gameObject.SetActive(false);
        }
    }

    private void ResetSkillDesc(int index)
    {
        switch (index)
        {
            case 0: // publicity
                pubSkillDesc.text = "";
                pubSkillName.text = "";
                pubSkillCost.text = "";
                break;
            case 1: // popularity
                popSkillDesc.text = "";
                popSkillName.text = "";
                popSkillCost.text = "";
                break;
            case 2: 
                // manufacturing
                cigSkillDesc.text = "";
                cigSkillName.text = "";
                cigSkillCost.text = "";
                break;
            default:
                break;
        }
    }
}
