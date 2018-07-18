	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainModeCircle : MonoBehaviour {
	float scale = 0.03f;
	float smooth = 5.0f;
	float tiltAngle = 60.0f;
	void Update () {
		if(this.gameObject.activeSelf == true)
		{
			float a;
			this.transform.localScale += new Vector3(scale, scale, 1);
			a = this.GetComponent<Image>().color.a;
			a = a * 255 - 5;
			float tiltAroundZ =  tiltAngle;
			//float tiltAroundX =  tiltAngle;

			Quaternion target = Quaternion.Euler(0, 0, tiltAroundZ);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
			this.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)((int)a));
			if(this.transform.localScale.x > 1.5f)
			{
				this.gameObject.SetActive(false);
				this.transform.localScale = new Vector3(0, 0, 1);
			}
		}
	}
}
