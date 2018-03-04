using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CrackMain : MonoBehaviour {

	#region Variables
	public GameObject[] Olines = new GameObject[4];
	public GameObject endpanel, pausepanel,canvas;
	GameObject start;
	public bool[,] filds = new bool[4, 2];
	public Angles[] angles = new Angles[4];
	Line[] lines = new Line[4];
	List<int> v = new List<int>();
	Touch[] myTouches;
	int n = 0,s = 0,bscore=0;
	bool bstart = false, btime = false, apart = false;
	Vector3 center = new Vector3(Screen.width /2, Screen.height /2);
	public Text endtext, time,score;
	float ftime = 10f;


	public struct Line
	{
		public float lenght;
		public float angle;
		//public Vector2 scord;
		public Vector2 ecord;
		public float n;
		public float k;

		public Line(float l, float a, /*Vector2 s*/ float n1, float k1, Vector2 e)
		{
			lenght = l;
			angle = a;
			//scord = s;
			n = n1;
			k = k1;
			ecord = e;
		}

	}
	public struct Angles
	{
		public float angle;
		public int line;

		public Angles(float a, int l)
		{
			angle = a;
			line = l;
		}
	}
	#endregion

	#region UnityMethods
	void Start () {
		score = GameObject.Find("score").GetComponent<Text>();
		time = GameObject.Find("time").GetComponent<Text>();
		start = GameObject.Find("start");
	}
	
	void Update () {
		myTouches = Input.touches;
		if (Input.touchCount == n && n != 0)
		{
			if (Alltouch())
			{
				NextLines();
			}
		}
		if (bstart = true && btime == true)
		{
			ftime -= Time.deltaTime;
			time.text = ftime.ToString("F2") + "sec";

		}
		if (ftime <= 0 && btime == true)
		{
			GameOver();
		}

	}
	
	#endregion

	#region MyMethods
	public void NextLines()
	{
		bscore++;
		apart = true;
		ftime += Mathf.Sin((float)(Mathf.Sin((float)bscore) + Mathf.Cos((float)bscore) / bscore * 10 + 0.1)) * Mathf.PI;
		for (int i = 0; i < 4; i++)
		{
			Olines[i].SetActive(false);
		}
		n = Random.Range(1, 5);
		for (int i = 0; i < n; i++)
		{
			Olines[i].SetActive(true);
			lines[i] = SpawnLine(RandomSide());
			Olines[i].GetComponent<RectTransform>().position = center;
			Olines[i].transform.eulerAngles = new Vector3(0, 0, lines[i].angle);
			//Olines[i].GetComponent<RectTransform>().sizeDelta = new Vector2(lines[i].lenght*100, 1);
			//Vector3 tmpPos = Camera.main.WorldToScreenPoint(new Vector3(lines[i].lenght,1));
			Olines[i].transform.localScale= new Vector2( lines[i].lenght/10,1);
		}
		
	}	
	Vector2 RandomSide()
	{
		int nside = Random.Range(1, 5);
		Vector2 sidekord;
		int x, y;
		switch (nside)
		{
			case 1:
				x = 0;
				y = Random.Range(0, Screen.height);
				s += y;
				sidekord = new Vector2(x, y);
				break;
			case 2:
				x = Random.Range(0, Screen.width);
				y = 0;
				s += x + Screen.height;
				sidekord = new Vector2(x, y);
				break;
			case 3:
				x = Screen.width;
				y = Random.Range(0, Screen.height);
				s += y + Screen.height + Screen.width;
				sidekord = new Vector2(x, y);
				break;
			case 4:
				x = Random.Range(0, Screen.width);
				y = Screen.height;
				s += x + 2 * Screen.height + Screen.width;
				sidekord = new Vector2(x, y);
				break;
			default:
				Debug.Log("gli");
				sidekord = new Vector2(0, 0);
				break;
		}
		if (v.Count != 0)
		{
			for (int i = 0; i < v.Count - 1; i++)
			{
				if (v[i] + 200 > s || v[i] - 200 > s)
				{
					return RandomSide();
				}
			}

		}
		v.Add(s);
		return sidekord;
	}
	Line SpawnLine(Vector2 _ecord)
	{
		Line _line;
		_line.ecord = _ecord;
		//_line.scord = new Vector3((_ecord.x + center.x) / 2, (_ecord.y + center.y) / 2);
		_line.k = (_ecord.y - center.y) / (_ecord.x - center.x);
		_line.n = _ecord.y - _line.k * _ecord.x;
		_line.angle = Mathf.Atan(_line.k) * Mathf.Rad2Deg;
		_line.lenght = Mathf.Sqrt((center.x - _ecord.x) * (center.x - _ecord.x) + (center.y - _ecord.y) * (center.y - _ecord.y));
		return _line;
	}
	bool Alltouch()
	{
		int w = 0;
		for (int i = 0; i < Input.touchCount; i++)
		{
			for (int j = 0; j < n; j++)
			{
				float a = Vector3.Distance(center, myTouches[i].position);
				float b = Vector3.Distance(center, lines[i].ecord);
				float c = Vector3.Distance(myTouches[i].position, lines[i].ecord);
				angles[i].angle = Mathf.Acos((a * a + b * b - c * c) / 2 * a * b) * Mathf.Rad2Deg;
				angles[i].line = j;
			}
			Quick_Sort(angles, 0, n - 1);
			if (filds[angles[i].line, 0] == false)
			{
				filds[angles[i].line, 0] = true;
				w++;
			}
			else
			{
				if (filds[angles[i].line, 1] == true)
					return false;
				else
				{
					filds[angles[i].line, 1] = true;
					w++;
				}
			}
		}
		Debug.Log(w);
        if (w >= 2 * n-1)
            return true;
        else
            return false;
	}
	public void Replay()
	{
		SceneManager.LoadScene("Scene/MainMenu");
	}
	public void Pause()
	{
		if (btime == true)
		{
			btime = false;
			pausepanel.SetActive(!pausepanel.activeSelf);
		}
	}
	public void GoOn()
	{
		if (btime == false)
		{
			pausepanel.SetActive(!pausepanel.activeSelf);
			btime = true;
		}
	}
	public void Home()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void GameOver()
	{
		if (bscore > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt("HighScore", bscore);
		}
		endpanel.SetActive(!endpanel.activeSelf);
		endtext.text = "GAME OVER\n score: " + bscore.ToString() + "\n HIGH SCORE:" + PlayerPrefs.GetInt("HighScore", 0).ToString();
		time.text = "0";
		bstart = false;
		btime = false;
	}
	private static void Quick_Sort(Angles[] arr, int left, int right)
	{
		if (left < right)
		{
			int pivot = Partition(arr, left, right);

			if (pivot > 1)
			{
				Quick_Sort(arr, left, pivot - 1);
			}
			if (pivot + 1 < right)
			{
				Quick_Sort(arr, pivot + 1, right);
			}
		}

	}
	private static int Partition(Angles[] arr, int left, int right)
	{
		float pivot = arr[left].angle;
		while (true)
		{

			while (arr[left].angle < pivot)
			{
				left++;
			}

			while (arr[right].angle > pivot)
			{
				right--;
			}

			if (left < right)
			{
				if (arr[left].angle.Equals(arr[right].angle)) return right;

				Angles temp = arr[left];
				arr[left] = arr[right];
				arr[right] = temp;


			}
			else
			{
				return right;
			}
		}
	}




	#endregion

}