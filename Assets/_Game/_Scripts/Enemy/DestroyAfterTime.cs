using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class DestroyAfterTime : MonoBehaviour
    {
        public void DAT(float t)
        {
            Destroy(gameObject, t);
        }
    }
}