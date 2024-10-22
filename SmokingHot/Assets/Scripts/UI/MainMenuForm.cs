using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class StartButtonClick : MonoBehaviour
{
	public Button startButton;
	public TMP_InputField companyNameInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		startButton.onClick.AddListener(TaskOnClick);
    }

	void TaskOnClick(){
		if (companyNameInput.text.Trim().Length > 0)
		{
			MainManager.Instance.companyName = companyNameInput.text;
			Debug.Log($"Company name is: {MainManager.Instance.companyName}", this);
			SceneManager.LoadScene("MainScene");
		}
		else
		{
			Debug.Log($"The company name input content is empty.", this);
		}
	}
}