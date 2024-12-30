using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using NUnit.Framework.Constraints;

public class MainMenuForm : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button startButton;
	public TMP_InputField companyNameInput;
	public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		startButton.onClick.AddListener(TaskOnClick);
		gameManager = FindFirstObjectByType<GameManager>();

        SelectNormalDifficulty();
        normalButton.Select();
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

    public void SelectEasyDifficulty()
    {
        SetButtonHighlighted(easyButton);
        gameManager.SetGameDifficulty(GameManager.GameDifficulty.Easy);
    }

    public void SelectNormalDifficulty()
    {
        SetButtonHighlighted(normalButton);
        gameManager.SetGameDifficulty(GameManager.GameDifficulty.Normal);
    }

    public void SelectHardDifficulty()
    {
        SetButtonHighlighted(hardButton);
        gameManager.SetGameDifficulty(GameManager.GameDifficulty.Hard);
    }

    private void SetButtonHighlighted(Button button)
    {
        easyButton.GetComponent<Image>().color = Color.white;
        normalButton.GetComponent<Image>().color = Color.white;
        hardButton.GetComponent<Image>().color = Color.white;

        button.GetComponent<Image>().color = Env.UI_IncreaseColor;
    }
}