using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonRightScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
            moveRight();
        }
        else if(isPressed == State.IsNotPressed)
        {
            stopMovingRight();
            isPressed = State.NoState;
        }
            
    }

    public void moveRight()
    {
        Debug.Log("RIGHT");
        playerScript.moveRight();
    }

    public void stopMovingRight()
    {
        Debug.Log("STOP MOVING RIGHT");
        playerScript.stopMovingRight();
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
