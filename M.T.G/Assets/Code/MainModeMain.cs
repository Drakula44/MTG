using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainModeMain : MonoBehaviour
{

	#region Variables
	public GameObject pausepanel;//za aktiviranje panela pauze
	public GameObject endpanel;//za aktiviranje panela za kraj
	GameObject start;//za pokretanje igre
	Text current;//trenutan broj prstiju text
	Text next;//sledeci broj prstiju text
	Text score;//trenutan skor text
	Text time;//trenutno vreme do kraja
	public Text endtext;//ispis na kraju igre
	List<Transform> circles = new List<Transform>();//skladeste za krugove 
	List<Vector2> cordinates = new List<Vector2>();//skladiste za kordinate prstiju
	public Transform parentcircle;//mesto za krugove
	public Transform circle;//originalni krug
	public static Queue<Transform> uubotrebi;
	public static Queue<Transform> vanupotrebe;

	float ttoend = 10f;//vreme do kraja
	float error = 0.5f;//maksimalana greska
	float a;
	float maxscale = 1.5f;//maksimalan scale
	float scale = 0.03f;//stepen povecavaja kruga
	int b;
	int ncurrent;//trenutan brodj prstiju
	int nnext;//sledeci broj prstiju
	int nofscore = 0;//trenutan score
	int noftouches = 0;// trenutan broj dodira

	bool bstart = false;//da li je igra pocela
	bool btime = false;//da li vreme tece
	bool apart = false;//da li su prsti odvoji od ekrana od zadnjeg ta;nog didrura
	bool spawn = false;//da li je potrebno da se stvaraju krugovi
	bool needing = true;//da li je potrebno da se povecavaju krugovi

	#endregion

	#region UnityMethods
	void Start()
	{
		start = GameObject.Find("start");                                           //
		current = GameObject.Find("current").GetComponent<Text>();                  //
		next = GameObject.Find("next").GetComponent<Text>();                        //
		score = GameObject.Find("score").GetComponent<Text>();                      //pronalazenje potrebnih objekata
		time = GameObject.Find("time").GetComponent<Text>();                        //
		parentcircle = GameObject.Find("parentcircle").GetComponent<Transform>();   //
	}
	void Update()
	{
        //Animation();
		noftouches = Input.touchCount;// uzima broj prstiju
		if (bstart = true && btime == true)// if za vreme 
		{
			ttoend -= Time.deltaTime;//smanjivanje vremena
			time.text = ttoend.ToString("F2") + "sec";//ispisivaje vremena

		}
		if (ncurrent == noftouches && noftouches != 0 && apart == false)//da li je dobijen poen
		{
			Cordinate();
			NextNumber();
		}
		if (ncurrent != noftouches && noftouches != 0 && btime == true)//racuna gresku
		{
			error -= Time.deltaTime;
		}
		if ((ttoend <= 0 || error <= 0) && btime == true)///provera kraj igre
		{
			GameOver();
		}
		if (noftouches == 0)//gleda da li su prsti odvojeni od ekrana
		{
			apart = false;
		}
	}
	#endregion


	#region MyMethods
	public void CStart()//Pocetak igre kada se pritisne start dugme
	{

		ncurrent = Random.Range(1, 5); //random broj
		current.text = ncurrent.ToString();//ispisivanje trenutnog broja
		nnext = Random.Range(1, 5);//random sledeci broj
		next.text = nnext.ToString();//ispisivanje sledeceg broja
		nofscore = 0;// postavljanje skora
		bstart = true;// oznacavaje pocetka vremena
		btime = true;// oznacavaje pocetka vremena
		Object.Destroy(start);//unistavanje start dugmeta

	}
	public void NextNumber()//kada se ta;no pritisne
	{
		ncurrent = nnext;						//
		nnext = Random.Range(1, 5);				//
		current.text = ncurrent.ToString();     //postavljanje sldeceeg broj
		next.text = nnext.ToString();			//
		nofscore++;								//povecavanje skora
		score.text = nofscore.ToString();		//postavljanje skora
		error = 0.5f;							//
		apart = true;							//
		spawn = true;							//vracanje pocetne greske
		needing = true;							//
		Animation();							
		ttoend = ttoend + 0.1f;					//povecavanje vremena
	}
	public void Animation()//poziva animacije za stvaranje krug
	{
		for (int i = 0; i < 1; i++)
		{
			Transform pr = Instantiate(circle, parentcircle);
			pr.position = cordinates[i];
		}

	}
	public void Cordinate()//uzimanje kordinata dodira
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

	}//treba izbaciti
	public void GoOn()
	{
		if (btime == false)
		{
			pausepanel.SetActive(!pausepanel.activeSelf);
			btime = true;
		}
	}//treba izbaciti
	public void Home()
	{
		SceneManager.LoadScene("Scene/MainMenu");
	}
	public void GameOver()
	{
		if (nofscore > PlayerPrefs.GetInt("HighScore", 0))
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
