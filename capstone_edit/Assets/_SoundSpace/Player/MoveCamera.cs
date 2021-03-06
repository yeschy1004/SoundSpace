using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	GameObject ObjectToRotate;
	private float speed = 2.0f;
	private float zoomSpeed = 2.0f;
	float rotationY = 0.0f;
	float rotationX = 0.0f;

	public float minX = -360.0f;
	public float maxX = 360.0f;
	public float minY = -45.0f;
	public float maxY = 45.0f;
	public float sensX = 100.0f;
	public float sensY = 100.0f;

    private void Awake()
    {
        ObjectToRotate = gameObject;
    }

    void Update () {
		//float scroll = Input.GetAxis("Mouse ScrollWheel");
		//ObjectToRotate.transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);

		if (Input.GetKey(KeyCode.RightArrow)){
			ObjectToRotate.transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			ObjectToRotate.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.UpArrow)){
			ObjectToRotate.transform.position += Vector3.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			ObjectToRotate.transform.position += Vector3.back * speed * Time.deltaTime;
		}

		if (Input.GetMouseButton (0)) {
			rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			ObjectToRotate.transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		}
	}
}
