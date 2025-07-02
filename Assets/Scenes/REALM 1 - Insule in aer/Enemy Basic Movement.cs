using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyBasicMovement : MonoBehaviour
{
    public GameObject lifePrefab;

    private Rigidbody2D _rb;
    private Animator _animator;
    private bool movingLeft = true;
    public float speed = 2.0f;
    private float distanceMoved = 0f;
    public float targetDistance = 5f;
    public Sprite crushedSprite;
    private bool isCrushed = false;
    public GameObject deathParticle;
    private ScoreScript Score;
    private int Life = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Score = GameObject.FindWithTag("Score").GetComponent<ScoreScript>();
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

        if (Life <= 0)
        {
            Score.Score++;
            if (Random.Range(0, 10) % 2 == 0)
            {
                Instantiate(lifePrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), Quaternion.identity);
            }

            GameObject.FindWithTag("EnemySpawner").GetComponent<SpawnerScript>().enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            //isCrushed = true;

            //Death();
        }

        if (collision.gameObject.tag == "player_bullets")
        {
            Life--;
            Debug.Log("A FOST LOVIT");

            Destroy(collision.gameObject);
        }
    }

    private void Death()
    {
        Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject, 0.03125f);
    }
}
