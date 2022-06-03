using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Event
{
    /* UI buttons events */
    public static void EatFood(Player player)
    {
        Debug.Log("Eat Food");
    }
    public static void FeedCat(Player player)
    {
        Debug.Log("Feed Cat");
    }
    public static void CovidTest(Player player)
    {
        Debug.Log("Covid Test");
    }
    public static void ExchangeFood(Player player)
    {
        Debug.Log("Exchange Food");
    }
    public static void Cook(Player player)
    {
        Debug.Log("Cook");
    }
    public static void Chat(Player player)
    {
        Debug.Log("Chat");
    }
    public static void ViewNews(Player player)
    {
        Debug.Log("View News");
    }

    public static void DecodeRandomEvent(PlayerChangeInfo playerChangeInfo)
    {
        Debug.Log("Decode message");
    }
    public static void RandomEvent(Player player,int eventID)
    {
        Debug.Log("Random Event");

        int btn_num = 0;
        int msg_num = 0;
        string btn_text = null;
        string msg_box_info = null;
        string[] btn_text_ary = null;
        string[] msg_info_ary = null;

        if (eventID>0)
        {
            btn_num = int.Parse(GlobalFunctions.random_event_dt.Rows[eventID - 1][1].ToString());
            msg_num = int.Parse(GlobalFunctions.random_event_dt.Rows[eventID - 1][2].ToString());
            btn_text = GlobalFunctions.random_event_dt.Rows[eventID - 1][3].ToString();
            msg_box_info = GlobalFunctions.random_event_dt.Rows[eventID - 1][4].ToString();
            btn_text_ary = btn_text.Split('|');
            msg_info_ary = msg_box_info.Split('|');
        }
 
        PlayerChangeInfo playerChangeInfo = new PlayerChangeInfo();


        GlobalFunctions.randomEventUI.origin_btn.GetComponent<Button>().onClick.AddListener(() => Event.RandomEvent(player, -1));
        if (eventID > 0)
            GlobalFunctions.randomEventUI.origin_btn.GetComponentInChildren<TextMeshProUGUI>().text = btn_text_ary[0];
        GlobalFunctions.randomEventUI.origin_btn.transform.localPosition = new Vector3(440, -156 - 200 / (btn_num + 1) * 1, 0);

        if (eventID > 0)
            GlobalFunctions.randomEventUI.origin_msg.GetComponent<TextMeshProUGUI>().text = msg_info_ary[0];
        GlobalFunctions.randomEventUI.origin_msg.transform.localPosition = new Vector3(-180, -156 - 200 / (msg_num + 1) * 1, 0);



        GlobalFunctions.randomEventUI.RemoveAllCloneObjectByTag("clone");
        GlobalFunctions.ChangeRandomEventUIVisible();

        
        if (GlobalFunctions.randomEventUI.gameObject.active)
        {

            for (int i = 0; i < btn_num - 1; i++)
            {
                GameObject clone_btn = GlobalFunctions.randomEventUI.AddButton();
                clone_btn.GetComponent<Button>().onClick.AddListener(() => Event.RandomEvent(player, -1));
                clone_btn.transform.localPosition = new Vector3(440, -156 - 200 / (btn_num + 1) * (i + 2), 0);
                clone_btn.GetComponentInChildren<TextMeshProUGUI>().text = btn_text_ary[i + 1];
            }


            for (int i = 0; i < msg_num - 1; i++)
            {
                GameObject clone_msg = GlobalFunctions.randomEventUI.AddMessageBox();
                clone_msg.transform.localPosition = new Vector3(-180, -156 - 200 / (msg_num + 1) * (i + 2), 0);
                clone_msg.GetComponent<TextMeshProUGUI>().text = msg_info_ary[i + 1];
            }
        }
        else
        {
            Event.DecodeRandomEvent(playerChangeInfo);
        }

    }
}
