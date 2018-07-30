using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;

public class PointMain : MonoBehaviour {

	#region Variables
	int n = 0,nscore = 0;
	float ttoend = 10f;
	public GameObject original;
	public Transform parent;
	PointTarget[] targetscript = new PointTarget[4];
	GameObject[] targets = new GameObject[4];
	
	public GameObject endpanel,start;
	bool bstart = false,btime = false;
	public Text time, endtext;
	#endregion

	#region UnityMethods
	void Start () {
		//OnStart();
		
	}

	void Update () {
		
		if(CheckingAll())
		{
			NextTargets();
		}
		if (bstart = true && btime == true)
		{
			ttoend -= Time.deltaTime;
			time.text = ttoend.ToString("F2") + "sec";

		}
		if ((ttoend <= 0) && btime == true)
		{
			GameOver();
		}
	}

	#endregion

	#region MyMethods
	public void GameOver()
	{
		if (nscore > PlayerPrefs.GetInt("HighScoreP", 0))
		{
			PlayerPrefs.SetInt("HighScoreP", nscore);
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
		endtext.text = "GAME OVER\n score: " + nscore.ToString() + "\n HIGH SCORE:" + PlayerPrefs.GetInt("HighScoreP", 0).ToString();
		time.text = "0";
		bstart = false;
		btime = false;
	}
	public void Home()
	{
		SceneManager.LoadScene("Scene/MainMenu");
	}
	public void Replay()
	{
		SceneManager.LoadScene("Scene/MainMode");
	}
	public void OnStart()
	{
		Destroy(start);
		bstart = true;
		btime = true;
		n = Random.Range(1, 4);
		for (int i = 0; i < n; i++)
		{
			targets[i] = Instantiate(original,Vector3.zero,Quaternion.identity,parent);
			targets[i].transform.localPosition = RandomCordinate();
			targetscript[i] = targets[i].GetComponent<PointTarget>();
		}
	}
	void NextTargets()
	{
		ttoend += 0.2f;
		nscore++;
		for (int i = 0; i < n; i++)
		{
			Destroy(targets[i]);
			targetscript = new PointTarget[4];
		}
		n = Random.Range(1, 4);
		for (int i = 0; i < n; i++)
		{
			targets[i] = Instantiate(original, RandomCordinate(), Quaternion.identity, parent);
			targetscript[i] = targets[i].GetComponent<PointTarget>();
		}
	}
	Vector2 RandomCordinate()
	{
		float _x = Random.Range(-Screen.width/2, Screen.width/2);
		float _y = Random.Range(-Screen.height / 2, Screen.height/2);
		return (new Vector2(_x, _y));
	}
	public bool CheckingAll()
	{
		bool _st = false;
		for (int i = 0; i < n; i++)
		{
			_st = targetscript[i].CheckingTouch();
			if (_st == false)
				break;
		}
		return _st;
	}

	#endregion
}
