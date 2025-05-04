using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy Path", menuName = "Enemies/Enemy Path", order = 0)]
    public class EnemyPath : ScriptableObject {
        public Vector3[] waypoints;
    }
}