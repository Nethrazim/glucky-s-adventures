using UnityEngine;

public class EnemyBasicScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool movingLeft = true;
    private float speed = 2.0f;
    private float distanceMoved = 0f;
    public float targetDistance = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = speed * Time.deltaTime;

        if (movingLeft)
        {
            _animator.SetTrigger("isMovingLeft");
            transform.Translate(Vector3.left * move);
            distanceMoved += move;
            if (distanceMoved >= targetDistance)
            {
                movingLeft = false;
                _animator.ResetTrigger("isMovingLeft");
                distanceMoved = 0f;
            }
        }
        else
        {
            _animator.SetTrigger("isMovingRight");
            transform.Translate(Vector3.right * move);
            distanceMoved += move;
            if (distanceMoved >= targetDistance)
            {
                _animator.ResetTrigger("isMovingRight");
                movingLeft = true;
                distanceMoved = 0f;
            }
        }
    }
}
