using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text companyName;

    void Start()
    {
        companyName.text = MainManager.Instance.companyName;
    }
}
