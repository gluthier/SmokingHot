using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void CreateWorldEvent()
    {
        WorldEvent votation = new WorldEvent(
            "Votation against tobacco advertisment",
            "There is an ongoing votation against tobacco advertisment. We should spend some money to fight it! If you accept, then the costs will be 10 millions. If you refuse, we might not be able to do some advertisment.",
            WorldEvent.EventType.Political,
            WorldEvent.ImpactFactor.Negative,
            VotationAccept,
            VotationRefuse);

        gameManager.PopulateWorldEventUI(votation);
    }

    public void VotationAccept()
    {
        gameManager.SpendMoney(50);
    }

    public void VotationRefuse()
    {
        // todo!
    }
}