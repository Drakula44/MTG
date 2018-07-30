using UnityEngine;

public class SetSize : MonoBehaviour {

	#region Variables
	GameObject MainCanvas;
	public Camera cam;
	RectTransform MainCanvasRT;
	#endregion

	#region UnityMethods
	void Start () {
		MainCanvas = GameObject.Find("MainCanvas");
		MainCanvasRT = MainCanvas.GetComponent<RectTransform>();
		MainCanvasRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
		MainCanvasRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
		cam.orthographicSize = Screen.height;
		cam.rect = new Rect(0, 0, Screen.width, Screen.height);
	}

	void Update () {
		
	}
	
	#endregion

	#region MyMethods



	#endregion
}
