using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public WorldEvent CreateWorldEvent(int yearPassed)
    {
        WorldEvent worldEvent = new AbolishAds();

        switch (yearPassed)
        {
            case 5:
                worldEvent = new AbolishAds();
                break;
            case 10:
                break;
            case 15:
                break;
            case 20:
                break;
            case 25:
                break;
            case 30:
                break;
            case 35:
                break;
            case 40:
                break;
            case 45:
                break;
            default:
                break;
        }

        return worldEvent;
    }
}