using UnityEngine;

public class CanvasHolderScripy : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasInstance;
    private float playerCanvasDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCanvasDistance = player.transform.position.x - canvasInstance.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2(player.transform.position.x - playerCanvasDistance, transform.position.y);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Round(player.transform.position.x * 100f) / 100f - Mathf.Round(playerCanvasDistance * 100f) / 100f, transform.position.y);
    }
}
