using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using UnityEngine.UIElements;

public class BasicEnemyFire : MonoBehaviour
{
    public GameObject Bullet;
    private GameObject Player;
    private bool hasFired = false;
    private float resetTime = 0;
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
        if (!hasFired && Mathf.Abs(Player.transform.position.x - gameObject.transform.position.x) < distanceFromPlayer)
        {
            //StartCoroutine(FireEveryHalfSecond());
            float elapsed = Time.time - resetTime;
            if (elapsed > 1)
            {
                Fire();
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
