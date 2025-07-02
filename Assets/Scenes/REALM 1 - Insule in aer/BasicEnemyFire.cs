using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BasicEnemyFire : MonoBehaviour
{
    public GameObject Bullet;
    private GameObject Player;
    private bool hasFired = false;
    public float resetTime = 0;
    public float distanceFromPlayer = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        resetTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.IsDestroyed())
        {
            if (!hasFired && Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x) < distanceFromPlayer)
            {
                //StartCoroutine(FireEveryHalfSecond());
                float elapsed = Time.time - resetTime;
                if (elapsed > 2)
                {
                    Fire();
                }
            }
        }
    }

    private void Fire()
    {
        resetTime = Time.time;
        Vector3 spawnPoint = transform.position;
        spawnPoint.y += 0.7f;

        Instantiate(Bullet, spawnPoint, Quaternion.identity);
    }

    IEnumerator FireEveryHalfSecond()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }
}
