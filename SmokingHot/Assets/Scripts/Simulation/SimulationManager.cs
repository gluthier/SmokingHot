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

    private bool isSimulationOn;
    private int yearPassed;
    private float timePassed;

    private float totalYearSimulated;
    private float gameMinutesLength;
    private List<CompanyEntity> companies;
    private List<ContinentEntity> continents;

    public void Start()
    {
        isSimulationOn = false;
    }

    public void StartSimulation()
    {
        isSimulationOn = true;
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
        HandleSimulatedTime();

        #region DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartSimulation();
        }
        #endregion
    }

    private void HandleSimulatedTime()
    {
        if (!isSimulationOn)
            return;

        timePassed += Time.deltaTime;

        //float yearSimulatedPerRealMinute = min / gameMinutesLength;

        Debug.Log($"{timePassed} -- {yearPassed}");
        if (timePassed >= 60f)
        {
            Debug.Log($"{timePassed} -- {yearPassed}");
            yearPassed += 1;
            timePassed = 0;

            if (yearPassed < totalYearSimulated)
            {
                HandleEndOfSimulatedYear();
            }
            else
            {
                HandleEndOfGame();
            }
        }
    }

    private void HandleEndOfSimulatedYear()
    {

    }

    private void HandleEndOfGame()
    {
        isSimulationOn = false;
    }

    private void ResetSimulation()
    {
        yearPassed = 0;
        timePassed = 0f;

        LoadData(
            GameDataLoader.Load());
    }
}
