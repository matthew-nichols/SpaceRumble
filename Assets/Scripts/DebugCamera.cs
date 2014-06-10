using UnityEngine;
using System.Collections;

public class DebugCamera : MonoBehaviour {

	public float flySpeed = 0.5f;
	public GameObject defaultCam;
	public GameObject playerObject;
	public bool isEnabled;
	
	public bool shift;
	public bool ctrl;
	public float accelerationAmount = 30.0f;
	public float accelerationRatio = 3.0f;
	public float slowDownRatio = 0.2f;
	
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			shift = true;
			flySpeed *= accelerationRatio;
		}
		
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			shift = false;
			flySpeed /= accelerationRatio;
		}
		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
		{
			ctrl = true;
			flySpeed *= slowDownRatio;
		}
		if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
		{
			ctrl = false;
			flySpeed /= slowDownRatio;
		}
		if (Input.GetAxis("Vertical") != 0)
		{
			transform.Translate(defaultCam.transform.forward * flySpeed * Input.GetAxis("Vertical"));
		}
		if (Input.GetAxis("Horizontal") != 0)
		{
			transform.Translate(defaultCam.transform.right * flySpeed * Input.GetAxis("Horizontal"));
		}
		if (Input.GetKey(KeyCode.E))
		{
			//transform.Translate(defaultCam.transform.up * flySpeed*0.5f);
			Time.timeScale = 1;
		}
		else if (Input.GetKey(KeyCode.Q))
		{
			//transform.Translate(-defaultCam.transform.up * flySpeed*0.5f);
			Time.timeScale = 0;
		}
	}
}
