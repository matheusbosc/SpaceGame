using UnityEngine;

namespace _Game._Scripts
{
    public class GameManager : MonoBehaviour
    {
        public float moveSpeed = 4f;
        public Transform everythingParent;
        void Update ()
        {
            everythingParent.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,moveSpeed * Time.deltaTime);
        }
    }
}