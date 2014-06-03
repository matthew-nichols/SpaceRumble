using UnityEngine;

public class globalData : MonoBehaviour {
    // TODO: pass in the difficulty of the mission.
    public AllyUnitStats[] allyUnits = new AllyUnitStats[20];
    public AllyUnitStats[] unselectedUnits = new AllyUnitStats[20];//unselected
    public AllyUnitStats[] selectedUnits = new AllyUnitStats[10];//at most 10 units;
    public int numUnits;
    public int gold;
    //Item lists In each category the 0th element is the default
    public Secondary[] secondaries = new Secondary[10];//probably too many???
    public mainSlot[] mains = new mainSlot[10];

    //items owned//extras TODO Will have to come up with some way to display extra items, maybe tabs?
    public Secondary[] secondaryInv = new Secondary[14];
    public mainSlot[] mainInv = new mainSlot[14];
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
