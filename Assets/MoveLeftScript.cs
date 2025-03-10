using UnityEngine;
using UnityEngine.EventSystems;

public class MoveLeftScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    private PlayerScript playerScript;
    enum State
    {
        NoState,
        IsPressed,
        IsNotPressed
    }

    private State isPressed = State.NoState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == State.IsPressed)
        {
            moveLeft();
        }
        else if(isPressed == State.IsNotPressed)
        {
            stopMovingLeft();
            isPressed = State.NoState;
        }
    }

    public void moveLeft()
    {
        Debug.Log("LEFT");
        playerScript.moveLeft();
    }

    public void stopMovingLeft()
    {
        if (isPressed == State.IsNotPressed)
        {
            Debug.Log("STOP MOVING LEFT");
            playerScript.stopMovingLeft();
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = State.IsPressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = State.IsNotPressed;
    }
}
