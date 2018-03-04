using UnityEngine;
using UnityEngine.UI;

public class PointTarget : MonoBehaviour {

	#region Variables
	Vector2 target;
	RectTransform rt;
	[SerializeField]
	float speed = 10f;
	GameObject cam;
	PointMain script;

	#endregion

	#region UnityMethods
	void Start () {
		target = RandomSide();
		rt = (RectTransform)this.transform;
		cam = GameObject.Find("Main Camera");
		script = cam.GetComponent<PointMain>();
		
	}

	void Update () {
		Vector3 dir = (Vector3)target - this.transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime,Space.World);
		if(CheckingSideBouncing())
		{
			NextTarget();
			speed = Random.Range(50, 350);
		}
	}

	#endregion

	#region MyMethods
	
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
	public bool CheckingTouch()
	{
		Touch[] myTouches = Input.touches;
		bool _st = false;
		for (int i = 0; i < Input.touchCount; i++)
		{
			if(Mathf.Sqrt((myTouches[i].position.x-this.transform.position.x)* (myTouches[i].position.x - this.transform.position.x)+ (myTouches[i].position.y - this.transform.position.y)* (myTouches[i].position.y - this.transform.position.y)) <= rt.rect.height)
			{
				_st = true;
			}

		}
		return _st;
	}
	bool CheckingSideBouncing()
	{

		bool _check = false;
		if (Vector3.Distance(this.transform.position, (Vector3)target) <= rt.rect.height/2)
		{
			_check = true;
		}
		return _check;
	}
	void NextTarget()
	{
		target = RandomSide();
	}

	#endregion
}
