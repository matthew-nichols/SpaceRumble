﻿using UnityEngine;

public class missionSettings : MonoBehaviour
{
		public int goldReward;
		public int difficulty;
		public int allowedUnits;
		public EnemyUnit[] enemyTypes = new EnemyUnit[5];//at most 5 enemy types(probably won't be used this version
		//public item[] itemRewards = new item[10]//non gold rewards;
		public AllyUnitStats unitRewards; //possibly get a unit as a reward.
        public Secondary second;
        public mainSlot main;
        //mission type
        public string type; //can be Attack, Defend, Find
}
