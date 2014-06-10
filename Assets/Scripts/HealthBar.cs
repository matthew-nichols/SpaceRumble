using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public EnemyUnit target;//Should change target to enemy
	public float healthBarLength;

	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width/6;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth(0);
	}

	void OnGUI()
	{
		if (Vector3.Distance (target.transform.position, target.currentTarget.transform.position) < target.maxDist*3)
		{
			Vector2 targetPos;
			targetPos = Camera.main.WorldToScreenPoint(transform.position);

			GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20),target.currentHealth + "/" + target.health);
		}
	}

	public void AdjustCurrentHealth(int adj)
	{
		target.currentHealth += adj;
		if(target.currentHealth < 0)
		{
			target.currentHealth = 0;
		}
		if(target.currentHealth > target.health)
		{
			target.currentHealth = target.health;
		}

		if(target.health < 1)
		{
			target.health = 1;
		}

		healthBarLength = (Screen.width/6) * (target.currentHealth / target.health);
	}
}
