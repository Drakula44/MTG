using UnityEngine;

public class PointMain : MonoBehaviour {

	#region Variables
	int n = 0;
	float time = 100f;
	public GameObject original;
	public Transform parent;
	PointTarget[] targetscript = new PointTarget[4];
	GameObject[] targets = new GameObject[4];
    public GameObject original1;
	#endregion

	#region UnityMethods
	void Start () {
		OnStart();
		
	}

	void Update () {
		time -= Time.deltaTime;
        OnStart();
		/*if(CheckingAll())
		{
			NextTargets();
		}*/
	}
	
	#endregion

	#region MyMethods
	void OnStart()
	{
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
		float _x = Random.Range(0, Screen.width/(Screen.width/720.0f));
		float _y = Random.Range(0, Screen.height/ (Screen.height/1280.0f));
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
