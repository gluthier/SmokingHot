using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public uint money = 1000;
    private uint cigaretteShopCost = 100;
    
    public void buyCigaretteShop()
    {
        if (money >= cigaretteShopCost)
        {
            money -= cigaretteShopCost;
            gameUI.updateMoneyText();
        }
        else
        {
            Debug.Log($"{money} not enough to buy shop ({cigaretteShopCost})");
        }
    }
}
