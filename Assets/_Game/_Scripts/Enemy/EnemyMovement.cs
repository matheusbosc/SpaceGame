using System.Collections;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class EnemyMovement : MonoBehaviour {
        [HideInInspector] public EnemyPath path;
        private int currentIndex = 0;
        public float speed = 5;
        
        public float shootTime = 0.5f;

        public Transform shootPoint;
        public GameObject bulletPrefab;

        private bool _canShoot = true;

        public WaveManager wM;

        public AudioSource shootAudio;

        void Update() {
	        if (path != null)
	        {
	        	if (currentIndex < path.waypoints.Length) {
		        	Vector3 target = path.waypoints[currentIndex];
		        	transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		        	if (Vector3.Distance(transform.position, target) < 0.1f)
			        	currentIndex++;
	        	}
	        	else
	        	{
		        	currentIndex = 0;
		        	Vector3 target = path.waypoints[currentIndex];
		        	transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		        	if (Vector3.Distance(transform.position, target) < 0.1f)
			        	currentIndex++;
	        	}
	        }

            if (_canShoot)
            {
                _canShoot = false;
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, bulletPrefab.transform.rotation);
	            bullet.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,-17);
	            bullet.GetComponent<BulletBehaviour>().damageAmount = wM.bulletDamage;
	            shootAudio.Play();
                StartCoroutine(WaitForShoot(shootTime));
                Destroy(bullet, 4);
            }
        }
        
        private IEnumerator WaitForShoot(float t){
            yield return new WaitForSeconds(t);
            _canShoot = true;
        }
    }
}