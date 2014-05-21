using UnityEngine;

public class globalData : MonoBehaviour {
    // TODO: pass in the difficulty of the mission.
    public AllyUnitStats[] allyUnits = new AllyUnitStats[20];
    public AllyUnitStats[] selectedUnits = new AllyUnitStats[10];//at most 10 units;
    public int numUnits;
    public int gold;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
