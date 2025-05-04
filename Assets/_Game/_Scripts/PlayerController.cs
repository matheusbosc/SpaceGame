using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    [FormerlySerializedAs("xSpeed")] public float speed = 4;
    public float shootTime = 0.1f;

    public Transform shootPoint;
	public GameObject bulletPrefab;

    private bool _canShoot = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _canShoot){
            _canShoot = false;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, bulletPrefab.transform.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,17);
            StartCoroutine(WaitForShoot(shootTime));
            Destroy(bullet, 4);
        }
    }

    private IEnumerator WaitForShoot(float t){
        yield return new WaitForSeconds(t);
        _canShoot = true;
    }


    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.Normalize(new Vector3(x,0,y));
        move *= speed;

	    if (x == 0 && y == 0){
            rb.linearVelocity = Vector3.zero;
        } else {
	        rb.linearVelocity = move;
        }

    }
}
