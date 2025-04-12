using UnityEngine;

public class ButtonFireScript : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        playerScript.Fire();
    }
}
