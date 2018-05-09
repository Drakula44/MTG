﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainModeMain : MonoBehaviour {

	#region Variables
	public GameObject pausepanel;
    public GameObject endpanel;
    GameObject start;
    Text current;
    Text next;
    Text score;
    Text time;
    public Text endtext;
    List<Transform> circles = new List<Transform>();
    List<Vector2> cordinates = new List<Vector2>();
    public Transform parentcircle;
    public Transform circle ;

    float ttoend = 10f;
    float error = 0.5f;
    float a;
    float maxscale = 1.5f;
    float scale = 0.03f;
    int b;
    int ncurrent;
    int nnext;
    int nofscore = 0;
    int noftouches = 0;
    bool bstart = false;
    bool btime = false;
    bool apart = false;
    bool spawn = false;
    bool needing = true;

	#endregion

	#region UnityMethods
	void Start () {
        start = GameObject.Find("start");
        //pausepanel = GameObject.Find("pausepanel");
        //endpanel = GameObject.Find("endpanel");
        current = GameObject.Find("current").GetComponent<Text>();
        next = GameObject.Find("next").GetComponent<Text>();
        score = GameObject.Find("score").GetComponent<Text>();
        time = GameObject.Find("time").GetComponent<Text>();
        parentcircle = GameObject.Find("parentcircle").GetComponent<Transform>();
        CStart();

    }
	void Update()
	{
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
	#endregion


	#region MyMethods
	public void CStart()
    {

        ncurrent = Random.Range(1, 5);
        current.text = ncurrent.ToString();
        nnext = Random.Range(1, 5);
        next.text = nnext.ToString();
        nofscore = 0;
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
        nofscore++;
        score.text = nofscore.ToString();
        error = 0.5f;
        apart = true;
        spawn = true;
        needing = true;
		Animation();
        //ttoend = ttoend + ((1 / nofscore) * 15);
    }
    public void Animation()
    {
		for (int i = 0; i < cordinates.Count; i++)
		{
			circlePooler.Instance.SpawnFromPool("circle", cordinates[i]);
			spawn = false;
		}

    }
    public void Cordinate()
    {
        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            cordinates.Add(myTouches[i].position);
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("Scene/MainMode");
    }
    public void Pause()
    {
		btime = !btime;
		pausepanel.SetActive(!pausepanel.activeSelf);

    }
    public void GoOn()
    {
        if(btime == false)
        {
            pausepanel.SetActive(!pausepanel.activeSelf);
            btime = true;
        }
    }
    public void Home()
    {
		SceneManager.LoadScene("Scene/MainMenu");
	}
    public void GameOver()
    {
        if(nofscore > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", nofscore);
        }
        endpanel.SetActive(!endpanel.activeSelf);
        endtext.text = "GAME OVER\n score: " + nofscore.ToString() + "\n HIGH SCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        time.text = "0";
        bstart = false;
        btime = false;
    }
	#endregion   
}
