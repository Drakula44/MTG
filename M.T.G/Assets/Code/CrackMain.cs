using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CrackMain : MonoBehaviour {

	#region Variables
	public Transform originalline;
	public Transform parentoflines;
	Vector2 ccord = new Vector2(Screen.width / 2, Screen.height / 2);
	Vector2 scord = new Vector2(Screen.width, Screen.height / 2);
	List<Line> lines;
	public Transform[] Olines;
	public class Line
	{
		public float angle;
		public Vector2 ecord;
		public float lenght;
	}
	int n;
	#endregion

	#region UnityMethods
	private void Start()
	{
		NextLines();
	}
	private void Update()
	{
		
	}
	#endregion

	#region MyMethods
	void NextLines()
	{ 
		for (int i = 0; i < 4; i++)
		{
			Olines[i].gameObject.SetActive(false);
		}
		n = Random.Range(1, 5);
		if (n == 1)
			n = 0;
		lines = new List<Line>();
		for (int i = 0; i < n; i++)
		{
			lines.Add(SpawnLine(RandomSide()));
			Olines[i].gameObject.SetActive(true);
			Vector3 a = new Vector3(0, 0, lines[i].angle*Mathf.Rad2Deg);
			//Olines[i].eulerAngles = a;
			Olines[i].localScale = new Vector3(lines[i].lenght,1,1);
		}
	}
	float Angle(Vector2 _a)
	{
		float angle;
		_a.x = _a.x - ccord.x;
		_a.y = _a.y - ccord.y;
		angle = Mathf.Acos((scord.x * _a.x + scord.y + _a.y) / Vector2.Distance(ccord, scord) * Vector2.Distance(ccord, _a)) * Mathf.Rad2Deg;
		if(_a.y<0)
		{
			angle = 360 - angle;
		}
		return angle;
	}
	Vector2 RandomSide()
	{	
		int nside = Random.Range(1, 5);
		Vector2 gl = new Vector2(Screen.width, Screen.height);
		Vector2 sidekord;
	 	float x, y;
		switch (nside)
		{
			case 1:
				x = -gl.x;
				y = Random.Range(-gl.y, gl.y);
				sidekord = new Vector2(x, y);
				break;
			case 2:
				x = Random.Range(-gl.x, gl.x);
				y = -gl.y;
				sidekord = new Vector2(x, y);
				break;
			case 3:
				x = gl.x;
				y = Random.Range(-gl.y, gl.y);
				sidekord = new Vector2(x, y);
				break;
			case 4:
				x = Random.Range(-gl.x, gl.x);
				y = gl.y;
				sidekord = new Vector2(x, y);
				break;
			default:
				sidekord = new Vector2(0, 0);
				break;
		}
		return sidekord;
	}
	Line SpawnLine(Vector2 _ecord)
	{
		Line _line  = new Line();
		_line.ecord = _ecord;
		_line.angle = Angle(_ecord);
		_line.lenght = Vector2.Distance(ccord, _ecord)*100;
		return _line;
	}
	#endregion

}