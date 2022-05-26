using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IfGyroPresent : MonoBehaviour {


	// this script is to enable or disable AcceleroGyro
	// when some conditions happends.
	// for example, if the devices has giroscope, this
	// script can detect this, and disable acceleroGyro
	// IS VERY EASY TO USE, ONLY ENABLE editorcondition
	// if you works on unity editor, and disable when
	// you want compile and android version.
	// bool editor ForceAccelerometer is only for test accelerometer rotation with phones that have gyroscope, disable when you publish your game.

	GameObject acceGyroOff;		 	 // the gameobject thats contain script Accelerogyro.
	public GameObject correctedcam;			 // the object with improved rotation.

	public bool editorcondition;			// disable this option if you compile your game if you work on unity editor enable this.
	public bool ForceAccelerometer;			 // force to works with accelerometer, only recomended for test. disable always for finished versions.
    public Text text;

    private void Awake()
    {
        editorcondition = Application.isEditor;
        acceGyroOff = gameObject;
    }

    void Start ()
	{

		checks ();
		checksII ();

	}

    public void Change()
    {
        editorcondition = !editorcondition;
        if (editorcondition)
        {
            editor();
        }
        else
        {
            checksII();
        }
    }

	void checks() {
		#if UNITY_EDITOR
		editor();
		#endif
	}
	void editor () {
		if (editorcondition == true) {
            Debug.Log ("disabled sensor rotations, mouse rotation enabled");
			acceGyroOff.GetComponent<AcceleroGyro> ().enabled = false;			// to disable accelerogyro script.
			acceGyroOff.GetComponent<MoveCamera> ().enabled = true;				// enables camera rotating with mouse and keys in unity.
			acceGyroOff.GetComponent<GyroRotation> ().enabled = false;			// disable gyroscript.
		}
	}

	void checksII() {
		#if UNITY_ANDROID
		if (editorcondition == false) {
		andro();
        }
		#endif
	}
	void andro() {

		if (SystemInfo.supportsGyroscope == true && ForceAccelerometer == false) {	// check if the gyroscope is activated.
			Debug.Log(" selected : accelerogyro disabled, gyrorotation enabled");
			correctedcam.SetActive(false);										// disable object corrected.
			acceGyroOff.GetComponent <Camera> ().enabled = true;				// enabled camera, because correction object have a camera.
			Input.gyro.enabled = true;											// enabling gyro if this is present on sistem.
			acceGyroOff.GetComponent<AcceleroGyro> ().enabled = false;			// action to disable accelerogyro script.
			acceGyroOff.GetComponent<GyroRotation> ().enabled = true;			// action to enable your Gyroscope script. CHANGE <YourGyroScript> with the name of your gyroscope Script.
			acceGyroOff.GetComponent<MoveCamera> ().enabled = false;				// disable camera rotating with mouse and keys in unity.
		}

		else if (SystemInfo.supportsGyroscope == false) {						// check if the gyroscope is disabled or not present in sistem, and public bool detectgyro is disabled on inspector.
			Debug.Log(" selected : accelerogyro enabled, gyrorotation disabled");
			//Input.gyro.enabled = false;										// force to disable gyroscope
			acceGyroOff.GetComponent<AcceleroGyro> ().enabled = true;			// action to enable accelerogyro script.
			acceGyroOff.GetComponent<GyroRotation> ().enabled = false;   		// action to disable your gyroscript if not detected gyroscope on sistem.
			acceGyroOff.GetComponent<MoveCamera> ().enabled = false;			// disable camera rotating with mouse and keys in unity.

	}
		else {
			Debug.Log(" selected : accelerogyro enabled, gyrorotation disabled");
			//Input.gyro.enabled = false;										// force to disable gyroscope
			acceGyroOff.GetComponent<AcceleroGyro> ().enabled = true;			// action to enable accelerogyro script.
			acceGyroOff.GetComponent<GyroRotation> ().enabled = false;   		// action to disable your gyroscript if not detected gyroscope on sistem.
			acceGyroOff.GetComponent<MoveCamera> ().enabled = false;			// disable camera rotating with mouse and keys in unity.
        }
    }
}