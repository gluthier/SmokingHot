using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text companyName;
    public GameManager gameManager;

    void Start()
    {
        companyName.text = gameManager.companyName;
    }
}
