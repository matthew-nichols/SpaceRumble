using UnityEngine;

public class EnemyFire : MonoBehaviour
{
		public GameObject EnemyProjectile;
		public GameObject allyUnit;
		public GameObject enemyUnit;
		public float reloadTime = 1f;
		public float turnSpeed = 5f;
		public float firePauseTime = .25f;
		public Transform myTarget;
		public Transform turret;//turretball
		public Transform[] muzzlePositions;
		public float range = 10000;
		double nextFireTime;
		float nextMoveTime;
		Quaternion desiredRotation;

		void Update ()
		{


		}
}

