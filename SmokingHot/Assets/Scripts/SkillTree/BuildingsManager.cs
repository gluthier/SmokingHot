using UnityEngine;
using System.Collections.Generic;

public class BuildingsManager : MonoBehaviour
{
    public List<Building> myBuildings = new List<Building>();
    public SkillTreeManager skillTreeManager;
    private int[] tiers = { 1, 1, 1 };

    private void Update()
    {
        for (int i = 0; i < tiers.Length; i++)
        {
            if (tiers[i] != skillTreeManager.tiers[i])
            {
                TierUpBuilding(i, skillTreeManager.tiers[i]);
            }
        }
    }

    public void TierUpBuilding(int index, int tier)
    {
        tiers[index] = tier;
        switch (tier)
        {
            case 2:
                myBuildings[index].transform.GetChild(0).gameObject.SetActive(false);
                myBuildings[index].transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                myBuildings[index].transform.GetChild(1).gameObject.SetActive(false);
                myBuildings[index].transform.GetChild(2).gameObject.SetActive(true);
                break;
        }
    }
}
