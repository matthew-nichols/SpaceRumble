using UnityEngine;
using System.Collections;

public class perspectiveCameraMouse : MonoBehaviour {
	
	public float scrollSpeed = 20.0f;
	public string keyboardXAxis = "Horizontal";
	public string keyboardYAxis = "Vertical";
	//percent of screen that cursor needs to be on to scroll camera
	//should be less than 0.5
	public float edgeOfScreen = .15f;
	
	//returns where mouse cursor is on click
	public Vector3 mouseLocation;
	
	private baseUnit selectedUnit;
	private bool hasUnitSelected = false;
	private int previousSelectedID = -1;
	
	void Update () {
		PanCamera();
		if(Input.GetMouseButtonUp(0)){
			print ("isSelected: " + hasUnitSelected);
			LeftClick();
		}
		if(Input.GetMouseButtonUp(1)){
			RightClick();
		}
	}
	
	void PanCamera(){
		//keyboard
		transform.Translate(Input.GetAxis(keyboardXAxis) * scrollSpeed * Time.deltaTime,
		                    Input.GetAxis(keyboardYAxis) * scrollSpeed * Time.deltaTime,
		                    Input.GetAxis(keyboardYAxis) * 1.66f * scrollSpeed * Time.deltaTime);
		/*
		//up 
		if(Input.mousePosition.y >= Screen.height - (Screen.height * edgeOfScreen) && Input.mousePosition.y <= Screen.height)
			transform.position += (Vector3.forward + Vector3.right) * Time.deltaTime * scrollSpeed;
		
		//down
		if(Input.mousePosition.y <= Screen.height * edgeOfScreen && Input.mousePosition.y >= 0.0f)
			transform.position += -(Vector3.forward - Vector3.left) * Time.deltaTime * scrollSpeed;
		
		//left
		if(Input.mousePosition.x <= Screen.width * edgeOfScreen && Input.mousePosition.x >= 0.0f)
			transform.position += (Vector3.left + Vector3.forward) * Time.deltaTime * scrollSpeed;
		
		//right
		if(Input.mousePosition.x >= Screen.width - (Screen.width * edgeOfScreen)  && Input.mousePosition.y <= Screen.width)
			transform.position += (Vector3.right - Vector3.forward) * Time.deltaTime * scrollSpeed;
		*/
	}
	
	void RayHitSelectable(GameObject rayGO){
        //selectedUnit.isClicked = false;
        if (selectedUnit != null)
        {
            if (selectedUnit != rayGO.GetComponent<baseUnit>())
            {
                ((SelectionDisplay)selectedUnit.GetComponent("SelectionDisplay")).disp = false;
                selectedUnit.isClicked = false;
            }
        }
        selectedUnit = rayGO.GetComponent<baseUnit>();
		if(!selectedUnit.isClicked){
			selectedUnit.isClicked = true;

            ((SelectionDisplay)selectedUnit.GetComponent("SelectionDisplay")).disp = true;
		}
		else{
            ((SelectionDisplay)selectedUnit.GetComponent("SelectionDisplay")).disp = false;
			selectedUnit.isClicked = false;
		}
	}
	
	void RightClickSelectable(){
		if(hasUnitSelected){
			selectedUnit.updateRightClick = true;
			selectedUnit.destinationVector = mouseLocation;
		}
	}
	
	void LeftClick(){
		Debug.Log("LeftClick");
		RaycastHit hit = new RaycastHit();
		bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f);
		if(isHit){
			GameObject rayGO = hit.collider.gameObject;
			mouseLocation = hit.point;
			Debug.Log("Hit " + hit.transform.gameObject.name + " ID: " + rayGO.GetInstanceID());

			//if clicked object is selectable
			//still buggy
			if(rayGO.GetComponent("AllyUnit") != null){
				//Debug.Log("select: " + rayGO.GetType());
				RayHitSelectable(rayGO);
				
                hasUnitSelected = true;
				previousSelectedID = rayGO.GetInstanceID();
				//rayGO.SetActive(true);
                //sd.disp = true;
                //rayGO.GetComponent("SelectionDisplay").disp = true;
                //rayGO.GetComponent("AllyUnit").canMove = true;
            }
			else{
				Debug.Log("unselect");
                if (selectedUnit != null)
                {
                    selectedUnit.GetComponent<SelectionDisplay>().disp = false;

                    selectedUnit.isClicked = false;
                }
                hasUnitSelected = false;
				previousSelectedID = -1;
                selectedUnit = null;
			}
		}
	}
	
	void RightClick(){
		Debug.Log("RightClick");
		RaycastHit hit = new RaycastHit();
		bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000.0f);
		if(isHit){
			Debug.Log("Hit " + hit.transform.gameObject.name);
			GameObject rayGO = hit.collider.gameObject;
			mouseLocation = hit.point;
			//if(rayGO.GetComponent("baseUnit") != null){
				RightClickSelectable(); 
			//}
			//else if(rayGO.GetComponent("enemyUnit") != null)
		}
	}
}
