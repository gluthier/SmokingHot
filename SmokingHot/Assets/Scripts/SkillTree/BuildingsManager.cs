using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingsManager : MonoBehaviour
{
    public List<Building> myBuildings = new List<Building>();

    private void Update()
    {
    }
    //public void ChangeBuildPrefab(Skill skill, string buildingName)
    //{
    //    if (skill == null || string.IsNullOrEmpty(skill.skillName))
    //    {
    //        Debug.LogWarning("Invalid skill provided.");
    //        return;
    //    }
    //    GameObject newPrefab = LoadPrefab($"{buildingName}_Building");
    //    if (newPrefab)
    //    {
    //        StartCoroutine(ChangeBuildingCoroutine(newPrefab));
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"Prefab for {buildingName}_Building not found.");
    //    }
    //}

    //private GameObject LoadPrefab(string prefabName)
    //{
    //    /*if (!nextBulding.TryGetValue(prefabName, out GameObject prefab))
    //    {
    //        prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");
    //        if (prefab != null)
    //            nextBulding[prefabName] = prefab;
    //    }*/
    //    return nextBulding;
    //}

    //private IEnumerator ChangeBuildingCoroutine(GameObject newPrefab)
    //{
    //    // Optional: Fade out current building
    //    //yield return FadeOut(currentBuilding);

    //    Destroy(currentBuilding);
    //    currentBuilding = Instantiate(newPrefab, transform.position, transform.rotation);

    //    // Optional: Fade in new building
    //    //yield return FadeIn(currentBuilding);

    //    // Notify others that the building has changed
    //    NotifyBuildingChanged(currentBuilding);
    //}

    //public delegate void BuildingChangedHandler(GameObject newBuilding);
    //public event BuildingChangedHandler OnBuildingChanged;

    //private void NotifyBuildingChanged(GameObject newBuilding)
    //{
    //    OnBuildingChanged?.Invoke(newBuilding);
    //}
}
