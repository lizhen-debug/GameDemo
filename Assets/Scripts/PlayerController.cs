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
            if (GlobalFunctions.MoveNavMeshAgent() == "KitchenArea")
            {
                GlobalFunctions.outline_objects[0].GetComponent<BoxCollider>().enabled = false;
                GameManager.ChangeGameState(GlobalFunctions.GameState.Cooking);
                Event.Cook(GlobalFunctions.player);
            }
        }


    }


    //private bool isClick = false;
    //private Transform curTf = null;
    //private Vector3 oriMousePos;
    //private Vector3 oriObjectScreenPos;

    private void ControllerLogic_Cooking()
    {
        //Debug.Log("MainMenu state controller");

        GlobalFunctions.MouseHover();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (GlobalFunctions.mouse_hovered_obj)
        //    {
        //        curTf = GlobalFunctions.mouse_hovered_obj;
        //        oriObjectScreenPos = Camera.main.WorldToScreenPoint(curTf.position);
        //        oriMousePos = Input.mousePosition;
        //    }
        //    isClick = !isClick;
        //    
        //}
        //if (isClick)
        //{
        //    if (curTf != null)
        //    {
        //        Vector3 curMousePos = Input.mousePosition;
        //        Vector3 mouseOffset = curMousePos - oriMousePos;
        //        Vector3 curObjectScreenPos = oriObjectScreenPos + mouseOffset;
        //        Vector3 curObjectWorldPos = Camera.main.ScreenToWorldPoint(curObjectScreenPos);
        //        curTf.position = new Vector3(curObjectWorldPos.x, curObjectWorldPos.y, (float)-10.3);
        //    }
        //}


        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.ChangeGameState(GlobalFunctions.GameState.MainScene);
            GlobalFunctions.outline_objects[0].GetComponent<BoxCollider>().enabled = true;
        }

    }
}
