using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	private Text signInButtonText;
	private Text authStatus;
	public GameObject comming;

	private void Start()
	{
		PlayGamesClientConfiguration config = new
			PlayGamesClientConfiguration.Builder()
			.Build();

		// Enable debugging output (recommended)
		PlayGamesPlatform.DebugLogEnabled = true;

		// Initialize and activate the platform
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		signInButtonText =
			GameObject.Find("signInButton").GetComponentInChildren<Text>();
		authStatus = GameObject.Find("authStatus").GetComponent<Text>();
		PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);

	}
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
	public void SignInCallback(bool success)
	{
		if (success)
		{
			Debug.Log("(Lollygagger) Signed in!");

			// Change sign-in button text
			signInButtonText.text = "Sign out";

			// Show the user's name
			authStatus.text = "Signed in as: " + Social.localUser.userName;
		}
		else
		{
			Debug.Log("(Lollygagger) Sign-in failed...");

			// Show failure message
			signInButtonText.text = "Sign in";
			authStatus.text = "Sign-in failed";
		}
	}
	public void SignIn()
	{
		if (!PlayGamesPlatform.Instance.localUser.authenticated)
		{
			// Sign in with Play Game Services, showing the consent dialog
			// by setting the second parameter to isSilent=false.
			PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
		}
		else
		{
			// Sign out of play games
			PlayGamesPlatform.Instance.SignOut();

			// Reset UI
			signInButtonText.text = "Sign In";
			authStatus.text = "";
		}
	}
	public void CommingSoon()
	{
		comming.SetActive(!comming.activeSelf);
	}
	public void ShowLeaderboards()
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated)
		{
			PlayGamesPlatform.Instance.ShowLeaderboardUI();
		}
		else
		{
			Debug.Log("Cannot show leaderboard: not authenticated");
		}
	}	
	public void Exit()
    {
        Application.Quit();
    }
}
