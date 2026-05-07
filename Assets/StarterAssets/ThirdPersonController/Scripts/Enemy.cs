using UnityEngine;
public class Enemy : MonoBehaviour
{
    private int health = 3;
    [SerializeField] private Transform vfxDeath;
    [SerializeField] private Vector3 deathVFXOffset = Vector3.zero;

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Instantiate(vfxDeath, transform.position + deathVFXOffset, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}