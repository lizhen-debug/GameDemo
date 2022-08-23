using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInfo player_info = new PlayerInfo();

    public PlayerController playerController = new PlayerController();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerController.InvokeControllerAction(GlobalFunctions.current_state);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        if (other.gameObject.tag == "anchor")
        {
            Debug.Log(other.gameObject.name);
            if (other.gameObject.name == "KitchenArea_Anchor")
            {
                GameManager.ChangeGameState(GlobalFunctions.GameState.Cooking);
            }
        }
    }
    //GameManager.ChangeGameState(GlobalFunctions.GameState.MainScene);
}
