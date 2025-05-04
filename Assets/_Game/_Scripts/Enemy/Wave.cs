using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Enemies/Wave", order = 2)]
    public class Wave : ScriptableObject {
        public List<WaveData> enemyGroups;
    }
    
    [System.Serializable]
    public class WaveData {
        public GameObject[] enemyPrefabs;
        public EnemyPath path;
        public int countPerType;
    }
}