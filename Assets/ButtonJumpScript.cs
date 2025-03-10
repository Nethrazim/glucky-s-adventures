using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonJumpScript : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;

    private bool isPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void jump()
    {
        Debug.Log("Jump");
        playerScript.jump();
    }
}
