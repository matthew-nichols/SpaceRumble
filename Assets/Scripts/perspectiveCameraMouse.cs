using UnityEngine;

public class perspectiveCameraMouse : MonoBehaviour
{
		public float scrollSpeed = 20.0f;
		public string keyboardXAxis = "Horizontal";
		public string keyboardYAxis = "Vertical";
		//percent of screen that cursor needs to be on to scroll camera
		//should be less than 0.5
		public float edgeOfScreen = .15f;
		private int maxHeight = 14;
		private int currHeight = 7;
		private int zoomAmount = 3;
		//returns where mouse cursor is on click
		public Vector3 mouseLocation;
		baseUnit selectedUnit;
		bool hasUnitSelected = false;
		int previousSelectedID = -1;

		void Update ()
		{
				PanCamera ();
				if (Input.GetMouseButtonUp (0)) {
						LeftClick ();
				}
				if (Input.GetMouseButtonUp (1)) {
						RightClick ();
				}
		}

		void PanCamera ()
		{
				//keyboard
				transform.Translate (Input.GetAxis (keyboardXAxis) * scrollSpeed * Time.deltaTime,
						Input.GetAxis (keyboardYAxis) * scrollSpeed * Time.deltaTime,
						1.66f * Input.GetAxis (keyboardYAxis) * scrollSpeed * Time.deltaTime);
				/*
				//forward
				if (Input.mousePosition.y >= Screen.height - (Screen.height * edgeOfScreen) && Input.mousePosition.y <= Screen.height)
						transform.position += (Vector3.forward + Vector3.right) * Time.deltaTime * scrollSpeed;
		
				//backward
				if (Input.mousePosition.y <= Screen.height * edgeOfScreen && Input.mousePosition.y >= 0.0f)
						transform.position += -(Vector3.forward - Vector3.left) * Time.deltaTime * scrollSpeed;
		
				//left
				if (Input.mousePosition.x <= Screen.width * edgeOfScreen && Input.mousePosition.x >= 0.0f)
						transform.position += (Vector3.left + Vector3.forward) * Time.deltaTime * scrollSpeed;
		
				//right
				if (Input.mousePosition.x >= Screen.width - (Screen.width * edgeOfScreen) && Input.mousePosition.y <= Screen.width)
						transform.position += (Vector3.right - Vector3.forward) * Time.deltaTime * scrollSpeed;
		*/
				//up
				if (Input.GetAxis ("Mouse ScrollWheel") > 0 && currHeight < maxHeight) {
					transform.position += Vector3.up * zoomAmount;// * Time.deltaTime * scrollSpeed;
						++currHeight;
				}
				if (Input.GetAxis ("Mouse ScrollWheel") < 0 && currHeight > 0) {
					transform.position += Vector3.down * zoomAmount;// * Time.deltaTime * scrollSpeed;
						--currHeight;
				}
		}

		void RayHitSelectable (GameObject rayGO)
		{
				if (selectedUnit != null) {
						if (selectedUnit != rayGO.GetComponent<baseUnit> ()) {
								selectedUnit.GetComponent<SelectionDisplay> ().disp = false;
								selectedUnit.isClicked = false;
						}
				}
				selectedUnit = rayGO.GetComponent<baseUnit> ();
				if (!selectedUnit.isClicked) {
						selectedUnit.isClicked = true;
						selectedUnit.GetComponent<SelectionDisplay> ().disp = true;
				} else {
						selectedUnit.GetComponent<SelectionDisplay> ().disp = false;
						selectedUnit.isClicked = false;
				}
		}
	
		void RightClickSelectable ()
		{
				if (hasUnitSelected) {
						selectedUnit.updateRightClick = true;
						selectedUnit.destinationVector = mouseLocation;
				}
		}

		void LeftClick ()
		{
				RaycastHit hit;
				bool isHit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000.0f);
				if (isHit) {
						GameObject rayGO = hit.collider.gameObject;
						mouseLocation = hit.point;

						//if clicked object is selectable
						//still buggy
						if (rayGO.GetComponent ("AllyUnit") != null) {
								RayHitSelectable (rayGO);
				
								hasUnitSelected = true;
								previousSelectedID = rayGO.GetInstanceID ();
						} 
						else {
								if (selectedUnit != null) {
										selectedUnit.GetComponent<SelectionDisplay> ().disp = false;
										selectedUnit.isClicked = false;
								}
								hasUnitSelected = false;
								previousSelectedID = -1;
								selectedUnit = null;
						}
				}
		}

		void RightClick ()
		{
				RaycastHit hit;
				bool isHit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 1000.0f);
				if (isHit) {
						GameObject rayGO = hit.collider.gameObject;
						mouseLocation = hit.point;
						RightClickSelectable (); 
				}
		}
}
