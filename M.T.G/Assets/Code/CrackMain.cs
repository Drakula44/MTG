using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackMain : MonoBehaviour {

	#region Variables

	int n;
	List<Vector2> ivice;
	List<GameObject> givice;
	List<Vector2> cordinates;
	GameObject original;
	Transform parent;
	


	#endregion

	#region UnityMethods

	#endregion


	#region MyMethods
	public void Cordinate()//uzimanje kordinata dodira
	{
		Touch[] myTouches = Input.touches;
		for (int i = 0; i < Input.touchCount; i++)
		{
			cordinates.Add(myTouches[i].position);
		}
	}
	public void BStart()
	{
		n = Random.Range(1, 4);
		for (int i = 0; i < n; i++)
		{
			Vector2 t = RandomSide(ivice);
			GameObject gt = Instantiate(original, t, Quaternion.identity, parent);
			ivice.Add(t);
			
		}

	}
	Vector2 RandomSide(List<Vector2> ch)
	{
		int nside = Random.Range(1, 5);
		Vector2 sidekord;
		int x, y;
		switch (nside)
		{
			case 1:
				x = 0;
				y = Random.Range(0, Screen.height);
				sidekord = new Vector2(x, y);
				break;
			case 2:
				x = Random.Range(0, Screen.width);
				y = 0;
				sidekord = new Vector2(x, y);
				break;
			case 3:
				x = Screen.width;
				y = Random.Range(0, Screen.height);
				sidekord = new Vector2(x, y);
				break;
			case 4:
				x = Random.Range(0, Screen.width);
				y = Screen.height;
				sidekord = new Vector2(x, y);
				break;
			default:
				sidekord = new Vector2(0, 0);
				break;
		}
		return sidekord;
	}
	#endregion
}
