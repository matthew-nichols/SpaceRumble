using UnityEngine;

public class globalData : MonoBehaviour {
    // TODO: pass in the difficulty of the mission.
    public AllyUnit[] allyUnits = new AllyUnit[20];
    public AllyUnit[] selectedUnits = new AllyUnit[10];//at most 10 units;
    public int numUnits;
    public int gold;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
