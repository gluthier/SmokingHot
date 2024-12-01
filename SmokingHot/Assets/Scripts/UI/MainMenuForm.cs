using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StartButtonClick : MonoBehaviour
{
	public Button startButton;
	public TMP_InputField companyNameInput;
	public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		startButton.onClick.AddListener(TaskOnClick);
		gameManager = FindFirstObjectByType<GameManager>();
    }

    void TaskOnClick()
	{
		if (companyNameInput.text.Trim().Length > 0)
		{
			// Hide this menu and show the game UI
			gameObject.SetActive(false);
			gameManager.enterGame(companyNameInput.text);
		}
		else
		{
			Debug.Log($"The company name input content is empty.", this);
		}
	}
}