using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainModeCircle : MonoBehaviour {
	float scale = 0.003f;
	float smooth = 5.0f;
	float tiltAngle = 60.0f;
	Transform main;
	void Update () {
		this.transform.Rotate(new Vector3(0, 0, 1) * 100 * Time.deltaTime);
		float a;
		this.transform.localScale += new Vector3(scale, scale, 1);
		a = this.GetComponent<Image>().color.a;
		a = a * 255 - 0.5f;
		this.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)((int)a));
		if(this.transform.localScale.x > 1.5f)
		{
			this.gameObject.SetActive(false);
			this.transform.localScale = new Vector3(0, 0, 1);
		}
	}
}
