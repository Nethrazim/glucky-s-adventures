using UnityEngine;

public class EnemyBasicScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool movingLeft = true;
    private float speed = 2.0f;
    private float distanceMoved = 0f;
    public float targetDistance = 5f;
    public Sprite crushedSprite;
    private bool isCrushed = false;
    public GameObject deathParticle;

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

        if (movingLeft && !isCrushed)
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
        else if(!movingLeft && !isCrushed)
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("crushed");
            //_animator.SetTrigger("isCrushed");
            //var spriteRenderer = GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = crushedSprite;
            //spriteRenderer.enabled = false;
            //spriteRenderer.enabled = true;
            //_animator.enabled = false;
            isCrushed = true;

            Death();
        }

        if (collision.gameObject.tag == "bullets")
        {
            Debug.Log("A FOST LOVIT");
            Death();
        }
    }

    private void Death()
    {
        Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject, 0.03125f);
    }
}
