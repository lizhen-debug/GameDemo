using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFood : MonoBehaviour
{
    // Start is called before the first frame update

    public bool is_active;
    void Start()
    {
        is_active = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (!GlobalFunctions.PrepareFoodQTE.is_active)
                GlobalFunctions.PrepareFoodQTE.ActiveQTE("PotatoQTE", 30);
            else
                GlobalFunctions.PrepareFoodQTE.DeactiveQTE();
        }
    }

    public void ActivePickFood()
    {
        gameObject.SetActive(true);
        is_active = true;
    }
    public void DeactivePickFood()
    {
        gameObject.SetActive(false);
        is_active = false;
    }
}
