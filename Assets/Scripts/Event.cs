using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public Player player0;
    public GameObject random_event_canvas;
    // Start is called before the first frame update
    void Start()
    {
        random_event_canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePlayerFoodValue()
    {
        player0.ChangePlayerInfo(Player.info_type.FOOD,Player.operate_type.INCREASE,10);
    }

    public void ShowRandomEvent()
    {
        random_event_canvas.SetActive(true);
    }
}
