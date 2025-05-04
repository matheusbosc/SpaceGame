using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Enemies/Level", order = 1)]
    public class Level : ScriptableObject {
        public List<Wave> waves;
    }
}