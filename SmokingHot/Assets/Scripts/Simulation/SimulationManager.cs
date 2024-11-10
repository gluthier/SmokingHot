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

    public enum PopularityLevel
    {
        Hated = -2,
        Disliked = -1,
        Neutral = 0,
        Appreciated = 1,
        Loved = 2
    }

    private bool isSimulationOn;
    private int yearPassed;
    private float timePassed;

    private float totalYearSimulated;
    private float gameMinutesLength;
    private List<ConglomerateEntity> conglomerates;

    private WorldEventManager worldEventManager;


    public void Start()
    {
        isSimulationOn = false;
        worldEventManager = gameObject.AddComponent<WorldEventManager>();
    }

    public void StartSimulation()
    {
        Env.PrintDebug($"StartSimulation");

        isSimulationOn = true;
        ResetSimulation();
    }

    public void LoadData(GameData gameData)
    {
        totalYearSimulated = gameData.totalYearSimulated;
        gameMinutesLength = gameData.gameMinutesLength;

        conglomerates = new List<ConglomerateEntity>();

        foreach (ConglomerateData conglomerateData in gameData.conglomerates)
        {
            conglomerates.Add(new ConglomerateEntity(conglomerateData));
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

        float yearSimulatedPerRealMinute = totalYearSimulated / gameMinutesLength;
        float secondsForAYearSimulated = 60f / yearSimulatedPerRealMinute;

        if (timePassed >= secondsForAYearSimulated)
        {
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
        Env.PrintDebug($"HandleSimulatedTime, yearPassed: {yearPassed}");

        foreach (ConglomerateEntity conglomerate in conglomerates)
        {
            conglomerate.EndFiscalYear();
        }
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
