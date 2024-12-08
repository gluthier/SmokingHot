using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Experimental.GraphView;
using System;

public class SkillTreeManager : MonoBehaviour
{
    public List<SkillTreeUI> skillTreePanel;
    private GameObject lastActive;
    public GameManager gameManager;

    public TextMeshProUGUI cigSkillName;
    public TextMeshProUGUI cigSkillDesc;
    public TextMeshProUGUI cigSkillCost;
    public List<LineRenderer> cigLinks;
    
    public TextMeshProUGUI pubSkillName;
    public TextMeshProUGUI pubSkillDesc;
    public TextMeshProUGUI pubSkillCost;
    public List<LineRenderer> pubLinks;

    public TextMeshProUGUI popSkillName;
    public TextMeshProUGUI popSkillDesc;
    public TextMeshProUGUI popSkillCost;
    public List<LineRenderer> popLinks;

    public TextMeshProUGUI lobSkillName;
    public TextMeshProUGUI lobSkillDesc;
    public TextMeshProUGUI lobSkillCost;
    public List<LineRenderer> lobLinks;

    public int[] tiers = {1,1,1,1};

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
            case 0: // cig
                cigSkillDesc.text = skill.skillDescription;
                cigSkillName.text = skill.skillName;
                cigSkillCost.text = "Prix: " + skill.cost;
                break;
            case 1: // pub
                pubSkillDesc.text = skill.skillDescription;
                pubSkillName.text = skill.skillName;
                pubSkillCost.text = "Prix: " + skill.cost;
                break;
            case 2: // lobby
                lobSkillDesc.text = skill.skillDescription;
                lobSkillName.text = skill.skillName;
                lobSkillCost.text = "Prix: " + skill.cost;
                break;
            case 4: // rep
                popSkillDesc.text = skill.skillDescription;
                popSkillName.text = skill.skillName;
                popSkillCost.text = "Prix: " + skill.cost;
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
            if(prerequisite.isUnlocked)
            {
                isPrerequisiteUnlocked = true;
                break;
            }
        }

        hasMoney = gameManager.GetPlayerMoney() >= skill.cost;

        Debug.Log(isPrerequisiteUnlocked);
        Debug.Log(hasMoney);
        return isPrerequisiteUnlocked && hasMoney;
    }

    private void ApplySkillEffect(Skill skill)
    {
        List<String> effect = skill.effects;
        effect.Add("Down money " + skill.cost);
        gameManager.HandleSkillEffect(effect, GetCurrentActivePanel());
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
        
        skillTreePanel[index].gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        int index = GetCurrentActivePanel();
        if (!lastActive.GetComponent<Skill>().isUnlocked)
        {
            lastActive.transform.GetChild(0).gameObject.SetActive(false);
        }
        ResetSkillDesc(index);
        lastActive = null;
        gameManager.PlaySimulation();
    }

    private void ResetSkillDesc(int index)
    {
        switch (index)
        {
            case 0: // cig
                cigSkillDesc.text = "";
                cigSkillName.text = "";
                cigSkillCost.text = "";
                break;
            case 1: // pub
                pubSkillDesc.text = "";
                pubSkillName.text = "";
                pubSkillCost.text = "";
                break;
            case 2: // lobby
                lobSkillDesc.text = "";
                lobSkillName.text = "";
                lobSkillCost.text = "";
                break;
            case 4: // rep
                popSkillDesc.text = "";
                popSkillName.text = "";
                popSkillCost.text = "";
                break;
            default:
                break;
        }
    }
}
