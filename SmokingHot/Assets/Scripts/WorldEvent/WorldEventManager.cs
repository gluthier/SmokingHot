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

    public WorldEvent CreateWorldEvent(GameManager.GameState gameState)
    {
        WorldEvent worldEvent = new DebugEmptyEvent();

        switch (gameState.year)
        {
            case 5:
                worldEvent = new AbolishAds();
                break;
            case 10:
                worldEvent = new InvestSnacks();
                break;
            case 15:
                worldEvent = new SponsoringFestival();
                break;
            case 20:
                worldEvent = new VotationIncreasePrice();
                break;
            case 25:
                worldEvent = new PopStarDied();
                break;
            case 30:
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
            case 35:
                worldEvent = new ScientificStudy();
                break;
            case 40:
                worldEvent = new SocialInvestment();
                break;
            case 45:
                worldEvent = new MarketInvestment();
                break;
            default:
                worldEvent = new DebugEmptyEvent();
                break;
        }

        return worldEvent;
    }
}