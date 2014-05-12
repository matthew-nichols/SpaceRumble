using UnityEngine;

public class AllyUnit : baseUnit
{
		public Vector3 targetLocation;
		public bool canMove;
		public int energy;
		public int currentEnergy;

		protected override void Start ()
		{
				base.Start ();
		}

		protected override void Update ()
		{
				if (currentHealth <= 0) {
						Destroy (gameObject.rigidbody);
						Destroy (gameObject);
				}
				if (isClicked) {
						renderer.material = onHoverMaterial;
						moveUnit ();
				} else {
						renderer.material = defaultMaterial;
				}

				Collider[] enemies = Physics.OverlapSphere (transform.position, 100);
				for (int i = 0; i < enemies.Length; i++) {
						GameObject obj = enemies [i].gameObject;
						if (obj.tag == "Enemy") {
								obj.GetComponent<EnemyUnit> ().ApplyDamage (5);
								break;
						}

				}
		}

		void moveUnit ()
		{
				if (updateRightClick) {
						Debug.Log ("moving unit");
						agent.SetDestination (destinationVector);
						updateRightClick = false;
				}
		}
}
