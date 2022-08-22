using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController
{
    private static List<Action> ActionArrayBasedGameState = new List<Action>();

    // Start is called before the first frame update
    public PlayerController()
    {
        InitAllActionsBasedGameState();
    }

    private void InitAllActionsBasedGameState()
    {
        ActionArrayBasedGameState.Add(ControllerLogic_MainScene);
        ActionArrayBasedGameState.Add(ControllerLogic_Cooking);
    }
   
    public void InvokeControllerAction(GlobalFunctions.GameState gameState)
    {
        ActionArrayBasedGameState[((int)gameState)].Invoke();
    }

    private void ControllerLogic_MainScene()
    {
        //Debug.Log("MainMenu state controller");

        GlobalFunctions.MouseHover();
        
        if (Input.GetMouseButtonDown(0))
        {
            if (GlobalFunctions.MoveNavMeshAgent() == "KitchenArea_Anchor")
            {
                GlobalFunctions.outline_objects[0].GetComponent<BoxCollider>().enabled = false;
                GameManager.ChangeGameState(GlobalFunctions.GameState.Cooking);
                Event.Cook(GlobalFunctions.player);
            }
        }


    }

    private void ControllerLogic_Cooking()
    {
        //Debug.Log("MainMenu state controller");

        GlobalFunctions.MouseHover();

        
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.ChangeGameState(GlobalFunctions.GameState.MainScene);
            GlobalFunctions.outline_objects[0].GetComponent<BoxCollider>().enabled = true;
        }

    }
}
