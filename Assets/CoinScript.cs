using Unity.VisualScripting;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject collisionParticleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(collisionParticleSystem, gameObject.transform.position, Quaternion.Euler(0,0,180f));
            Destroy(gameObject);
        }
    }
}
