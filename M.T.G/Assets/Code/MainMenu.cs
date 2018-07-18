using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	private void Start()
	{
		Debug.Log("ajde pliz");
	}
	public void Play()
    {
        SceneManager.LoadScene("MainMode");
    }
	public void Point()
	{
		SceneManager.LoadScene("PointMode");
	}
	public void Crack()
	{
		SceneManager.LoadScene("CrackMode");
	}
    public void Exit()
    {
        Application.Quit();
    }
}
