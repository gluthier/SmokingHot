using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class WorldEventManager : MonoBehaviour
{
    private GameManager gameManager;
    private List<WorldEvent> worldEvents;
    private int idx;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        worldEvents = new List<WorldEvent>
        {
            new VotationAbolishAds(),
            new InvestSnacks(),
            new SponsoringFestival(),
            new VotationIncreasePrice(),
            new PopStarDied(),
            new ScientificStudy(),
            new SocialInvestment(),
            new MarketInvestment()
        };

        ResetOrderEvents();
    }

    public void ResetOrderEvents()
    {
        System.Random rng = new System.Random();
        worldEvents = worldEvents.OrderBy(_ => rng.Next()).ToList();
        idx = 0;
    }

    public WorldEvent CreateWorldEvent(GameManager.GameState gameState)
    {
        WorldEvent worldEvent = new NoEvent();

        switch (gameState.year)
        {
            case 5:
            case 10:
            case 15:
            case 20:
            case 25:
            case 30:
            case 35:
            case 40:
                worldEvent = worldEvents[idx];
                idx++;
                break;

            case 45:
                if (gameState.cigarettePackProduced.toxicity == CigarettePackEntity.ToxicityLevel.VeryBad)
                {
                    worldEvent = new FineToxicity();
                }
                else if (gameState.cigarettePackProduced.addiction == CigarettePackEntity.AddictionLevel.VeryAddictive)
                {
                    worldEvent = new FineAddiction();
                }
                else
                {
                    worldEvent = new UnethicalBonus();
                }
                break;

            default:
                worldEvent = new NoEvent();
                break;
        }

        return worldEvent;
    }
}