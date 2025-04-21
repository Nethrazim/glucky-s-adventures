using System.Collections;
using UnityEngine;
using UnityEngine;

public class EvilDiamondScript : MonoBehaviour
{
    public int hits = 3;

    public float pulseSpeed = 2.0f; // Controls the speed of the heartbeat
    public float pulseIntensity = 0.1f; // Controls the amount of scale change
    private Vector3 originalScale;

    public float trembleIntensity = 0.1f;
    public float trembleSpeed = 50f;
    private Vector3 originalPosition;

    public GameObject destroyedParticleSystem;
    public bool isDying = false;

    void Start()
    {
        // Store the object's original scale
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (hits > 0)
        {
            // Calculate the scaling factor using a sine wave
            float scaleAmount = Mathf.Sin(Time.time * pulseSpeed) * pulseIntensity;
            // Apply the scaling factor to the object's original scale
            transform.localScale = originalScale + new Vector3(scaleAmount, scaleAmount, scaleAmount);
        }

        if (hits <= 0)
        {
            float offsetX = Mathf.Sin(Time.time * trembleSpeed) * trembleIntensity;
            float offsetY = Mathf.Cos(Time.time * trembleSpeed) * trembleIntensity;

            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, originalPosition.z);

            if (!isDying)
            {
                StartCoroutine(IsDying(2.0f));
            }
        }
    }

    public IEnumerator IsDying(float delay)
    {
        isDying = true;
        yield return new WaitForSeconds(delay);
        Vector3 position = gameObject.transform.position;

        Instantiate(destroyedParticleSystem, position, Quaternion.identity);
        Destroy(gameObject);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullets")
        {
            hits--;
        }
    }

}
