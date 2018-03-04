using UnityEngine;

public class PointMain : MonoBehaviour {

	#region Variables
	int n = 0;
	float time = 100f;
	public GameObject original;
	public Transform parent;
	PointTarget[] targetscript = new PointTarget[4];
	GameObject[] targets = new GameObject[4];
	#endregion

	#region UnityMethods
	void Start () {
		OnStart();
		
	}

	void Update () {
		time -= Time.deltaTime;
		
		if(CheckingAll())
		{
			NextTargets();
		}
	}
	
	#endregion

	#region MyMethods
	void OnStart()
	{
		n = Random.Range(1, 4);
		for (int i = 0; i < n; i++)
		{
			targets[i] = Instantiate(original,RandomCordinate(),Quaternion.identity,parent);
			targetscript[i] = targets[i].GetComponent<PointTarget>();
		}
	}
	void NextTargets()
	{
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
	Vector3 RandomCordinate()
	{
		float _x = Random.Range(0, Screen.width);
		float _y = Random.Range(0, Screen.height);
		return new Vector3(_x, _y);
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
