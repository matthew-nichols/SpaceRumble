using UnityEngine;

public class globalData : MonoBehaviour {
    // TODO: pass in the difficulty of the mission.
    public AllyUnitStats[] allyUnits = new AllyUnitStats[20];
    public AllyUnitStats[] unselectedUnits = new AllyUnitStats[20];//unselected
    public AllyUnitStats[] selectedUnits = new AllyUnitStats[10];//at most 10 units;
    public int numUnits;
    public int gold;

    //Item lists In each category the 0th element is the default
    public Weapon[] weapons = new Weapon[10];//probably too many???
    public Secondary[] secondaries = new Secondary[10];//probably too many???
    public Armor[] armors = new Armor[10];//probably too many???
    public Accessory[] accessories = new Accessory[10];//probably too many???


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
