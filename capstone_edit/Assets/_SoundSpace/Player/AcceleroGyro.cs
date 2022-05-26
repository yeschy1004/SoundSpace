using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AcceleroGyro : MonoBehaviour {

	// ABOUT ACCELEROGYRO.
	/*AceleroGyro is a Script for tracking like gyroscope without gyroscope and without a Compas.
	 * All devices have accelerometer but only some have gyroscope.
	 * with this script you can provide tracking for all devices.
	 * It has 4 different forms of tracking and all forms work.
	 * Best tracking form in this order, front camera tracking, back camera tracking, VR nonecamera tracking
	 * nonecamera tracking. Frankly, back camera and front camera work equally without any difference about performace or
	 * rotation.
	 * 
	 * tracking only with accelerometer have 2 variants, tracking with VR and tracking without VR.
	 * tracking with accelerometer and camera also have 2 variants, tracking with frontal camera and back camera.
	 
	 * IT IS NOT NECESSARY to touch any line of CODE in this script to check and test.
	 * At least, at the beginning, I do not recommend it.
	 * 
	 * YOU CAN USE THE OPTIONS IN THE INSPECTOR, IT IS VERY EASY AND INTUITIVE.
	 */


	//with these bools choose the option for tracking.
	/* ONLY ONE AT THE SAME TIME CAN BE SELECTED FOR CORRECT TRACKING
	 * but you can combine all options with showaccedata
	 * showaccedata serves to see the data of the accelerometer on the screen.
	 * you can activate the debug mode, and disable it when you publish your game.
	*/

	public bool nonecamera; 		// to enable tracking without cameras and without using VR glasses.
	public bool nonecameraVR;  		// to enable tracking without any camera but using VR Glasses.
	public bool facecamera;  		// to enable tracking with frontal camera.
	public  bool backcamera; 		// to enable tracking with main camera. I RECOMMEND USE THIS OPTION.

	public bool ShowAcceData; 		// to see data of the accelerometer on the screen.

	Vector3 nothing;				// Vector 3 zero. (X0, Y0, Z0);
	Vector3 laspost;  				// used with currentpost to know the last position
	Vector3 currentpost; 			// actual position of the camera
	Vector3 dir; 					// to know how to execute right and left, when camera tracking is disabled.
	Vector3 value; 					// for acceleration in unity
	Vector3 forward; 				// move on.
	Vector3 moving;					// to say that movement is nothing
	Vector3 sideways; 				// move sideways.

	float increspeedVR;				// to increase the speed with VR Glasses without camera tracking.
	float increspeed;				// to increase the speed without VR Glasses and without camera tracking.
	float increspeedVRleft;			// to increase the speed with VR Glasses without camera tracking.
	float increspeedVRright;		// to increase the speed with VR Glasses without camera tracking.
	float increspeedright;			// to increase thespeed without VR Glasses and without camera tracking.
	float increspeedleft;			// to increase the speed without VR Glasses and without camera tracking.


	public float NoCamSpeedFactorVR;// speed value of left and right movement in VR glasses, without tracking camera, defect 50f.
	public float NocamSensivityVR;  // sensitivity value of tracking with VR glasses and without camera tracking. value defect 0.011f.

	public float NoCamSpeedFactor;	// speed value of left and right movement, without VR glasses without tracking camera, value defect 700f.
	public float NocamSensivity;	// sensitivity value of tracking, without VR glasses and without camera tracking. value defect, posible lowest value. 0.0001f.


	WebCamTexture webcam; 			// to access the camera. in some devices is better frontcam and in others backcam.
	WebCamTexture frontcam;			// for asign camera face camera.
	WebCamTexture Backcam;			// for asign camera main camera.


	//	the camera in the game, in VR, the head, the player, or similar.
	// 	you need to know that a collider will be added to this gameobject at certain times.
	//	and the collider changes from kinematic to normal. depending on the conditions.
	// 	if you need a permanent collider you can fix this by putting other colliders around it, envolve with other colliders is a great solution.
	// 	and easy to do.
	GameObject ObjectRotation;


	// I DO NOT RECOMMEND TO TOUCH THESE VALUES, BUT IF YOU WANT, YOU CAN EXPERIMENT.

	int WebcamResolution; 			// Camera resolution. 32x32 in Void Start. you can change it but, I do not recommend. a big value decrease performance.
	int SensivitySensor = 128; 		// multiplier compares previous FPS to current FPS. "FPS = frames x second"
	int CompareRow = 16; 			// check two camera captures with rows.
	int MultipiqueYaw = 4096; 		// yaw movement is multiplied.



	public Text Texto; 			// to see the info of accelerometer "Only to see the data, only for developers"
	string datatostring; 		// to convert data of accelerometer to string "Only to see the data, only for developers"


    private void Awake()
    {
        ObjectRotation = gameObject;
    }
    void Start () {

		ObjectRotation.GetComponent<Rigidbody>();
		ObjectRotation.GetComponent<Rigidbody> ().isKinematic = false;
		ObjectRotation.GetComponent<Rigidbody> ().useGravity = false;
		nothing = Vector3.zero;
		WebcamResolution = 32;
		SensivitySensor = 64;
		MultipiqueYaw = 64;

		// instantiate a camera position equals Object position
		// and ad character controller.
		ObjectRotation.transform.position = GetComponent<Camera> ().transform.position;
		ObjectRotation.AddComponent<CharacterController> ();

		if (backcamera) {
			frontcam = null;
			Backcam = new WebCamTexture (WebCamTexture.devices [0].name, (int)WebcamResolution, (int)WebcamResolution);
			webcam = Backcam;
			webcam.Play ();

		}

		if (facecamera) {
			Backcam = null;
			frontcam = new WebCamTexture (WebCamTexture.devices [1].name, (int)WebcamResolution, (int)WebcamResolution);
			webcam = frontcam;
			webcam.Play ();

		}
	}

	// long values correction.

	string itDoed(long val) {
		if (val > 500) return ".";
		if (val > 250) return ",";
		if (val > 125) return "-";
		if (val > 62) return "+";
		if (val > 32) return "o";
		return "O";
	}

	// A gui is used for tracking, you don't see this.

	void itSlip(float minimum, float maximus, ref float valuee, string descending) {

		GUILayout.BeginHorizontal();
		GUILayout.Label(descending);
		valuee = GUILayout.HorizontalSlider(valuee, minimum, maximus);
		GUILayout.EndHorizontal();
	}

	// put the colors in monochrome "for tracking, you don't see this"
	int[] MonochromeColors(Color32[] colors) {

		var MonoColors = new int[colors.Length];
		for (int i = 0; i < colors.Length; i++) {

			MonoColors[i] = colors[i].r * 3 + colors[i].g * 6 + colors[i].b;

		} return MonoColors;
	}


	/* this part is for calculate rotation with accelerometer and camera
	* takes pictures and compare this ones to find the correct direction
	*I not recomend touch anithing in here
	*/

	long[,] differ;

	int greatX, greatY;

	int[] previusPixelation;

	float flatten = 0;


	void DetectYaw() {

		var currentPixelation = MonochromeColors(webcam.GetPixels32());
		if (previusPixelation != null) {
			int outt = (int) (webcam.width / SensivitySensor);

			if (outt < 2) outt = 2;

			int[] xdm = new int[] {  -4, -3, -2, -1, 0, 1, 2, 3, 4 };
			int[] ydm = new int[] {  -4, -3, -2, -1, 0, 1, 2, 3, 4 };

			differ = new long[xdm.Length, ydm.Length];
			for (int x = 0; x < xdm.Length; x++) {
				for (int y = 0; y < ydm.Length; y++) {
					differ[x,y] = PixelDifference(currentPixelation, previusPixelation, 
						xdm[x] * outt, ydm[y] * outt,
						webcam.width, webcam.height);
				}
			}
			greatX = 0;
			greatY = 0;
			long less = long.MaxValue;

			for (int x = 0; x < xdm.Length; x++) {
				for (int y = 0; y < ydm.Length; y++) {
					if (differ[x,y] < less) {
						less = differ[x,y];
						greatX = x;
						greatY = y;
					}
				}
			}
			float rotation = xdm[greatX];

			#if UNITY_EDITOR

			rotation += Input.GetAxisRaw("Mouse X") * 
				(Input.GetMouseButton(0) ? 10 : 0);

			rotation += Input.GetAxisRaw("Mouse Y") * 
				(Input.GetMouseButton(0) ? 10 : 0);

			#endif       

			if (less == 0) rotation = 0;
			Smooth (ref flatten, 10, ref rotation);            
			ObjectRotation.transform.Rotate(Vector3.up, 
				rotation * Time.deltaTime * MultipiqueYaw);
		}

		previusPixelation = currentPixelation;

	}

	/// <summary>
	/// abs(a-b)
	/// </summary>
	int absd(int a, int b) {

		if (a > b) return a - b;
		if (b > a) return b - a;
		return 0;
	}


	// accumulates difference for each pixel of two arrays.

	long PixelDifference(int[] original, int[] shifted, 
		int xChange, int yChange,
		int width, int height) {
		long differencer = 0;
		int y = 0;
		int c = original.Length;
		int p = 1;
		while(true) {
			
			for (int x = 0; x < width; x++) {
				int index = x + y * width; 							// index  array pixels
				if (index >= c) return (1000*differencer)/p; 		// reached last pixel return result 
				int index2 = (x + xChange) + (y + yChange) * width; // change index
				if (index2 >= c) return (1000*differencer)/p; 		// if come last pixel, return result
				if (x + xChange > width) break; 					// skip if shift created position after end of line
				if (x + xChange < 0) { x += -xChange; continue; } 	// skip iterations if shift created position before start of line
				if (y + yChange > height) break; 					// same for y axis
				if (y + yChange < 0) { y += -yChange; continue; }
				if (index < 0 || index2 < 0) continue; 				// prevent out of range
				// actual comparison and accumulation (in diff)
				var col1 = original[index];
				var col2 = shifted[index2];
				differencer += absd(col1, col2);
				p++; 												// accumulations counter
			}
			y += CompareRow; 										// skipp rows
		}
	}

	// instantiate rigidbody and put values in the beguining, I not recomend touch anithing of here.
	// change camera resolution if you want, but 32 works great, and a big size can down performance




	// for change two variables at sime time.
	void Smooth(ref float acc, float spd, ref float val) {
		acc = acc * Mathf.Clamp01(1 - Time.deltaTime * spd) + 
			val * (Mathf.Clamp01(Time.deltaTime * spd));
		val = acc;
	}

	float fspeed = 0;
	float fspeedd = 0;
	float smoothEAZ = 0,
	smoothEAX = 0;


	// NONECAMERA. HOW IT WORKS AND CONFIGURATION.

	// nonecamera is used to track rotation using only accelerometer.
	// It is not an exactly natural movement, it is approximate, and it works fine.
	// probably in some devices it works better then in others.

	// and the nonecamera form of tracking consumes less resources
	// and probably it is compatible with 100% smartphones.
	// this is the reason about this form of tracking.

	// with camera tracking options in the 90% of cases it will probably be sufficient.
	// but hey folks,, I want to go beyond with this.

	// the values are orientative, and if you want, you can change them.
	// you can change the most important values in inspector.
	// but the current values, that I put in the inspector, are preferable.

	// Nonecamera rotation happens because this track sistem is based on a trick.
	// when you look in some direction "left and right" your neck inclines a bit.
	// it is something natural for the human body movements that we are not conscious.
	// the track system benefits from this.
	// this form of tracking is not perfect, but it works fine.
	// The user can control the tracking with 1 minute with their VR glasses.
	// for VR games with glasses this form of tracking works PRETTY WELL.

	//and other important thing is that when we track with cameras we need light. with this metod we do not have this problem.

	void Update ()
	{	
		increspeedleft = (Input.acceleration.x) * NoCamSpeedFactor;
		increspeedright = (Input.acceleration.x) * NoCamSpeedFactor;

		increspeedVRleft = (Input.acceleration.x) * NoCamSpeedFactorVR;
		increspeedVRright = (Input.acceleration.x) * NoCamSpeedFactorVR;

		Vector3 dir = Vector3.zero;

		dir = Input.acceleration.normalized;
		dir.y = 0;
		dir.z = 0;
		currentpost = transform.position;

		if (ShowAcceData == true) {

			ShowAcceData = Texto;
			datatostring = Input.acceleration.ToString ();
			Texto.text = datatostring;

		}

		if ((nonecameraVR) && (dir.x >= NocamSensivityVR)) {  //begins with a little value

			//nonecamera = false;
			dir.x = NocamSensivityVR;
			increspeedVRleft = 0f;
			transform.Rotate (0f, 0.11f * increspeedVRright, 0f); // gyro direction and apply a public multiple factor
			//nonecamera = false;

		} else if ((nonecameraVR) && (dir.x <= -NocamSensivityVR)) {

			//nonecamera = false;
			dir.x = -NocamSensivityVR;
			increspeedVRright = 0f;
			transform.Rotate (0f, -0.11f *  -increspeedVRleft, 0f); // gyro direction and apply a public multiple factor
			moving = nothing;

		} else

			//nonecamera = false;
			//nonecameraVR = !nonecameraVR;
			laspost = currentpost;


		if ((nonecamera) && (dir.x >= NocamSensivity)) {  //begins with a little value

			//nonecameraVR = false;
			dir.x = NocamSensivity;
			increspeedright = 0f;
			transform.Rotate (0f, -0.02f * -increspeedleft, 0f); // gyro direction and apply a public multiple factor

		} else if ((nonecamera) && (dir.x <= -NocamSensivity)) {

			//nonecameraVR = false;
			dir.x = -NocamSensivity;
			increspeedleft = 0f;
			transform.Rotate (0f, 0.02f * increspeedright, 0f); // gyro direction and apply a public multiple factor
			moving = nothing;

		} else

			//nonecameraVR = false;	
			//nonecamera = !nonecamera;
			laspost = currentpost;





		#if UNITY_EDITOR

		value += Vector3.up * Input.GetAxisRaw ("Mouse Y") *
			(Input.GetMouseButton (0) ? 0 : 1);


		value += Vector3.forward * Input.GetAxisRaw ("Mouse Y") *
			(Input.GetMouseButton (0) ? 1 : 0);
		value.Normalize ();

		#else

		value = Input.acceleration * (Time.deltaTime);
		value.Normalize();

		#endif

		var ea = gameObject.transform.localEulerAngles;
		ea.z = value.x * -89.9f;
		ea.x = value.z * -89.9f; 
		Smooth (ref smoothEAZ, 10, ref ea.z);
		Smooth (ref smoothEAX, 15, ref ea.x);


		sideways = (ObjectRotation.transform.right * fspeedd * 20 * Time.deltaTime);
		forward = (ObjectRotation.transform.forward * fspeed * 20 * Time.deltaTime);

		moving = sideways + forward;
		ObjectRotation.GetComponent<CharacterController> ().
		SimpleMove (moving);

		(gameObject.transform.localEulerAngles = ea).Normalize ();
		DetectYaw ();
		if (ea.z > 0f) {

			increspeedright = 0f;
			increspeedleft = 0f;
			increspeedVRleft = 0f;
			increspeedVRright = 0f;
			moving.Normalize ();
			dir = Vector3.zero;


		} else if (ea.x > 0f) {

			increspeedright = 0f;
			increspeedleft = 0f;
			increspeedVRleft = 0f;
			increspeedVRright = 0f;
			moving.Normalize ();
			dir = Vector3.zero;

	}
}
}