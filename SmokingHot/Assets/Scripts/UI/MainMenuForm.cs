using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using NUnit.Framework.Constraints;

public class MainMenuForm : MonoBehaviour
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

    public void TaskOnClick()
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
			companyNameInput.gameObject.GetComponent<Image>().color = new Color32(232, 189, 189, 255);
		}
	}
}