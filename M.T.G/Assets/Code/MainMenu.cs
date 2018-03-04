using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Play()
    {
        SceneManager.LoadScene("MainMode");
    }
	public void Mode()
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
