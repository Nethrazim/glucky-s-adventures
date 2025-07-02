using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private float CurrentHealth;
    public float MaxHealth;
    private RectTransform rectTransform;
    private float originalWidth;
    private float ratio;
    private float pixelsByRatio = 0.0f;
    private float reduceWidthBy = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalWidth = rectTransform.rect.width;
        ratio = MaxHealth / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (reduceWidthBy > 0.0f)
        {
            
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x - reduceWidthBy, gameObject.transform.position.y, gameObject.transform.position.z);
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width - (originalWidth / reduceWidthBy), rectTransform.sizeDelta.y);
            reduceWidthBy = 0.0f;
        }
    }

    public void SetHealth(float damage)
    {
        if (CurrentHealth > 0.0f)
        {
            CurrentHealth -= damage;
            reduceWidthBy = damage * ratio;
        }
    }

    public void FillHealth()
    {
        CurrentHealth = MaxHealth;
        rectTransform.sizeDelta = new Vector2(originalWidth, rectTransform.sizeDelta.y);
    }

    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
        ratio = MaxHealth / 100;
    }

}
