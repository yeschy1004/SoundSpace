using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterRotation : MonoBehaviour {

	public GameObject CameraToCorrect;
	public GameObject CorrectedCamera;
	public float SensValue = 0.15f;
	public bool EnabledGravity;
	public bool EnabledIsKinematic;
	private float t;
	private Vector3 corr;
	private Vector3 calc;
	private Vector3 old;
	private bool swithc;
	private float Yvalue;
	private Vector3 realresult;
	private Vector3 rot;

	// Use this for initialization
	void Start () {

		Yvalue = SensValue * 3f;
		CorrectedCamera.AddComponent<Rigidbody> ();
		Gravi ();
		Kinek ();


	}


	void Gravi () {
		if (EnabledGravity == true ) {
			CorrectedCamera.GetComponent<Rigidbody> ().useGravity = true;
		}
		else {
			CorrectedCamera.GetComponent<Rigidbody> ().useGravity = false;

		}
	}
	void Kinek () {
		if (EnabledIsKinematic == true) {
			CorrectedCamera.GetComponent<Rigidbody> ().isKinematic = true;
		}
		else {
			CorrectedCamera.GetComponent<Rigidbody> ().isKinematic = false;
		}
	}

	void CorrectedMove () {
		if (realresult.x > Yvalue) {  

			var xx = CameraToCorrect.transform.eulerAngles.x;
			var targetPoint = CameraToCorrect.transform.position;
			var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
			CorrectedCamera.transform.rotation = Quaternion.Slerp(transform.rotation, CameraToCorrect.transform.rotation, Time.deltaTime);
			refixt ();
		}
		if (realresult.y > SensValue) {

			var yy = CameraToCorrect.transform.eulerAngles.y;
			var targetPoint = CameraToCorrect.transform.position;
			var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
			CorrectedCamera.transform.rotation  = Quaternion.Slerp(transform.rotation, CameraToCorrect.transform.rotation, Time.deltaTime);

			refixt ();
			//Debug.Log (CorrectedCamera.transform.localEulerAngles.y);
		}

		if (realresult.z > SensValue) {
			var targetPoint = CameraToCorrect.transform.position;
			var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
			var zz = CameraToCorrect.transform.eulerAngles.z;
			CorrectedCamera.transform.rotation = Quaternion.Slerp(transform.rotation, CameraToCorrect.transform.rotation, Time.deltaTime); 

			refixt ();
		}
	}
	void refixt () {
		CorrectedCamera.transform.rotation = CameraToCorrect.transform.rotation;
	}
	void timess () {
		t = 0.02f;
	}
	void position () {
		CorrectedCamera.transform.position = CameraToCorrect.transform.position;
	}
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;

		calc.x = Mathf.Abs (CameraToCorrect.transform.eulerAngles.x) - Mathf.Abs (corr.x);
		calc.y = Mathf.Abs (CameraToCorrect.transform.eulerAngles.y) - Mathf.Abs (corr.y);
		calc.z = Mathf.Abs (CameraToCorrect.transform.eulerAngles.z) - Mathf.Abs (corr.z);

		realresult.x = Mathf.Abs (calc.x);
		realresult.y = Mathf.Abs (calc.y);
		realresult.z = Mathf.Abs (calc.z);
		rot.x = calc.x;
		rot.y = calc.y;
		rot.z = calc.z;
		position ();

		if (t <= 0) {
			corr.x = old.x;
			corr.y = old.y;
			corr.z = old.z;
			CorrectedMove ();
			timess ();
		}
		else {
			
			corr.x = CameraToCorrect.transform.eulerAngles.x;
			corr.y = CameraToCorrect.transform.eulerAngles.y;
			corr.z = CameraToCorrect.transform.eulerAngles.z;
			old.x = corr.x;
			old.y = corr.y;
			old.z = corr.z;

		}
	}
}

