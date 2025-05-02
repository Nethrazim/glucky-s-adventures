using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine;
using UnityEngine.UIElements;

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

    public ParticleSystem hitParticleSystem;
    public ParticleSystem iHitParticleSystem;

    public GameObject innerCircle;
    private Material innerCircleMaterial;
    private float glowStep = -1.0f;
    void Start()
    {
        innerCircleMaterial = innerCircle.GetComponent<Renderer>().material;
        // Store the object's original scale
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
        Vector3 position = gameObject.transform.position;
        iHitParticleSystem = Instantiate(hitParticleSystem, position, Quaternion.Euler(180.0f, 0.0f, 0f));
        iHitParticleSystem.Stop();
    }

    void Update()
    {
        float glowAmount = innerCircleMaterial.GetFloat("_GlowAmount");
        if (glowAmount < -15)
        {
            glowStep = 0.025f;
        } else if (glowAmount >= -6.5f)
        {
            glowStep = -0.025f;
        }

        float newGlowAmountValue = glowAmount + glowStep;

        innerCircleMaterial.SetFloat("_GlowAmount", newGlowAmountValue);

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
                StartCoroutine(IsDying(0.25f));
            }
        }
    }

    public IEnumerator IsDying(float delay)
    {
        isDying = true;
        yield return new WaitForSeconds(delay);
        Vector3 position = gameObject.transform.position;

        Instantiate(destroyedParticleSystem, position, Quaternion.identity);
        iHitParticleSystem.Stop();
        Destroy(gameObject);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullets")
        {
            hits--;
            iHitParticleSystem.Play();
            Destroy(collision.gameObject);
        }
    }

}
