using UnityEngine;

public class Bullet1Script : MonoBehaviour
{
    public Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _rb.linearVelocityX = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
