using UnityEngine;

public class baseUnit : MonoBehaviour
{
		public Material defaultMaterial;
		public Material onHoverMaterial;
		public int health;
		public int currentHealth;
		public int attackDmg;
		public int attackRange;
		public double attackRate;
		public bool isClicked = false;
		public bool updateRightClick = false;
		public Vector3 destinationVector;
		protected NavMeshAgent agent;
		public string name = "unit";
		public baseUnit currentTarget;

		protected virtual void Start ()
		{
				agent = GetComponent<NavMeshAgent> ();
		}
	
		protected virtual void Update ()
		{
				if (isClicked) {
						print (gameObject.name + " is active: " + gameObject.activeSelf);
						renderer.material = onHoverMaterial;
				} else {
						renderer.material = defaultMaterial;
				}
		}

		void OnMouseEnter ()
		{
				renderer.material = onHoverMaterial;
		}

		void OnMouseExit ()
		{
				renderer.material = defaultMaterial;
		}

		public virtual void ApplyDamage (int n)
		{
				currentHealth -= n;
		}
}
