using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    
    private Rigidbody bulletRigidbody;

    private void Awake(){
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        bulletRigidbody.linearVelocity = transform.forward * 10f;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
