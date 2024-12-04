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
}
