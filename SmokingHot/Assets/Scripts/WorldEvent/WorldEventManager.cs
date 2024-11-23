using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public List<WorldEventSO> worldEvents;

    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        LoadAllWorldEventSO();
    }

    public void CreateWorldEvent()
    {
        int randIdx = Random.Range(0, worldEvents.Count);
        gameManager.PopulateWorldEventUI(worldEvents[randIdx]);
    }

    private void LoadAllWorldEventSO()
    {
        WorldEventSO[] worldEventsSO = Resources.LoadAll<WorldEventSO>(Env.WorldEventSOFolder);
        worldEvents = worldEventsSO.ToList();
    }
}