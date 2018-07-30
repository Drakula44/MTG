	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlePooler : MonoBehaviour
{

	public Transform parent;
	[System.Serializable]
	public class Pool
	{

		public string tag;
		public GameObject prefab;
		public int size;


	}
	#region nes
	public static circlePooler Instance;

	private void Awake()
	{
		Instance = this;
	}
	#endregion
	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	void Start()
	{

		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				Debug.Log("nes");
				GameObject obj = Instantiate(pool.prefab, parent);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}

	}

	public GameObject SpawnFromPool(string tag, Vector3 position)
	{
		if(!poolDictionary.ContainsKey(tag))
		{
			return null;
		}
		GameObject objectToSpawn = poolDictionary[tag].Dequeue();

		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;

		poolDictionary[tag].Enqueue(objectToSpawn);

		return objectToSpawn;	
	}


}
