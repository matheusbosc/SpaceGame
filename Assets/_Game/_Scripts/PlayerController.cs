using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    [FormerlySerializedAs("xSpeed")] public float speed = 4;
    public float shootTime = 0.1f;

    public Transform[] shootPoint;
    public Transform[] barrelPositions;
	public GameObject bulletPrefab;

    [HideInInspector]public bool canInteract = true;
	[HideInInspector]public bool _shootReload = false;
	public Transform explosionParticles;
    
    public Vector3 defaultPos, defaultRot;

    public Animator animator;

    public AudioSource shootAudio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.enabled = false;
        gameObject.transform.position = defaultPos;
        gameObject.transform.eulerAngles = defaultRot;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && canInteract && !_shootReload){
            _shootReload = true;
            foreach (var barrel in shootPoint)
            {
                GameObject bullet = Instantiate(bulletPrefab, barrel.position, bulletPrefab.transform.rotation);
                bullet.GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,17);
                StartCoroutine(WaitForShoot(shootTime));
                Destroy(bullet, 4);
                shootAudio.Play();
            }
        }
        
	    explosionParticles.position = transform.position;
    }

    private IEnumerator WaitForShoot(float t){
        yield return new WaitForSeconds(t);
        _shootReload = false;
    }


    private void FixedUpdate()
    {
        if (!canInteract)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
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
