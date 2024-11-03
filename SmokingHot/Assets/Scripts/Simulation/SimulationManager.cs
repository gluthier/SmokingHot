using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public enum AgeBracket
    {
        Kid,
        Teenager,
        YoungAdult,
        Adult,
        Senior
    }

    public enum SmokerType
    {
        NonSmoker,
        Smoker
    }

    private float timePassed;

    private float totalYearSimulated;
    private float gameMinutesLength;
    private List<CompanyEntity> companies;
    private List<ContinentEntity> continents;

    public void StartSimulation()
    {
        ResetSimulation();
    }

    public void LoadData(GameData gameData)
    {
        totalYearSimulated = gameData.totalYearSimulated;
        gameMinutesLength = gameData.gameMinutesLength;

        companies = new List<CompanyEntity>();
        continents = new List<ContinentEntity>();

        // Compute random market shares
        List<int> rands = new List<int>();
        float randTotal = 0;

        Random.InitState(3000);
        foreach (ContinentData _ in gameData.continents)
        {
            int pick = Random.Range(1, 6);
            rands.Add(pick);
            randTotal += pick;
        }

        int idx = 0;
        foreach (ContinentData continentData in gameData.continents)
        {
            float marketSharePercentage = rands[idx] / randTotal;
            idx++;

            continents.Add(new ContinentEntity(continentData));
            companies.Add(new CompanyEntity(marketSharePercentage, continentData));
        }
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        #region DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetSimulation();
        }
        #endregion
    }

    public void ResetSimulation()
    {
        timePassed = 0f;

        LoadData(
            GameDataLoader.Load());
    }
}
