using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text companyName;
    public TMP_Text moneyText;
    public GameManager gameManager;

    void Start()
    {
        companyName.text = MainManager.Instance.companyName;
        updateMoneyText();
    }

    void Update()
    {

    }

    public void updateMoneyText()
    {
        moneyText.text = $"${gameManager.money}";
    }    
}
