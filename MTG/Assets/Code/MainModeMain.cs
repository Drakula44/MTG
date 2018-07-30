using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;

public class MainModeMain : MonoBehaviour {

	int ncurrent, nnext, nscore,noftouches;
	bool btime = false, bstart = false, apart = false;
	float error = 0.5f,ttoend = 10f;
	Text next, current, score, time; 
		public Text endtext;
	GameObject start;
	public GameObject endpanel;
	List<Vector2> cordinates = new List<Vector2>();

	private void Start()
	{
		start = GameObject.Find("Play");
		current = GameObject.Find("Current").GetComponent<Text>();
		next = GameObject.Find("Next").GetComponent<Text>();
		score = GameObject.Find("Score").GetComponent<Text>();
		time = GameObject.Find("Time").GetComponent<Text>();
		//endtext = GameObject.Find("endtext").GetComponent<Text>();
		//BStart();
	}
	private void Update()
	{
		Animation();
		noftouches = Input.touchCount;
		if (bstart = true && btime == true)
		{
			ttoend -= Time.deltaTime;
			time.text = ttoend.ToString("F2") + "sec";

		}
		if (ncurrent == noftouches && noftouches != 0 && apart == false)
		{
			Cordinate();
			NextNumber();
		}
		if (ncurrent != noftouches && noftouches != 0 && btime == true)
		{
			error -= Time.deltaTime;
		}
		if ((ttoend <= 0 || error <= 0) && btime == true)
		{
			GameOver();
		}
		if (noftouches == 0)
		{
			apart = false;
		}
	}
	public void BStart()
	{
		ncurrent = Random.Range(1, 5);
		current.text = ncurrent.ToString();
		nnext = Random.Range(1, 5);
		next.text = nnext.ToString();
		nscore = 0;
		bstart = true;
		btime = true;
		Object.Destroy(start);
	}
	public void NextNumber()
	{
		ncurrent = nnext;
		nnext = Random.Range(1, 5);
		current.text = ncurrent.ToString();
		next.text = nnext.ToString();
		ttoend += 0.2f;
		nscore++;
		score.text = nscore.ToString();
		error = 0.5f;
		apart = true;
		Animation();
	}
	public void Animation()
	{
		for (int i = 0; i < cordinates.Count; i++)
		{
			circlePooler.Instance.SpawnFromPool("circle", cordinates[i]);
		}
		cordinates = new List<Vector2>();
	}
	public void Cordinate()
	{
		Touch[] myTouches = Input.touches;
		for (int i = 0; i < Input.touchCount; i++)
		{
			cordinates.Add(myTouches[i].position);
		}
	}
	public void Home()
	{
		SceneManager.LoadScene("Scene/MainMenu");
	}
	public void Replay()
	{
		SceneManager.LoadScene("Scene/MainMode");
	}
	public void GameOver()
	{
		if (nscore > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt("HighScore", nscore);
			/*if (PlayGamesPlatform.Instance.localUser.authenticated)
			{
				// Note: make sure to add 'using GooglePlayGames'
				PlayGamesPlatform.Instance.ReportScore(nscore, "PlayGamesPlatform.Instance.ReportScore",
					(bool success) =>
					{
						Debug.Log("(Lollygagger) Leaderboard update success: " + success);
					});
			}*/
		}
		endpanel.SetActive(!endpanel.activeSelf);
		endtext.text = "GAME OVER\n score: " + nscore.ToString() + "\n HIGH SCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
		time.text = "0";
		bstart = false;
		btime = false;
	}
}
