using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestSceneGameManager : MonoBehaviour
{
    public Texture2D cursor_texture;
    public List<Transform> area_anchors;
    public NavMeshAgent navMeshAgent;
    public List<Transform> outline_objs;
    // Start is called before the first frame update
    void Start()
    {
        GlobalFunctions.ChangeCursor(cursor_texture);
        GlobalFunctions.InitializeAreaAnchors(area_anchors);
        GlobalFunctions.InitializeObjOutlines(outline_objs);
    }

    // Update is called once per frame
    void Update()
    {
        GlobalFunctions.MouseHover();
        if (Input.GetButton("Fire1"))
        {
            GlobalFunctions.MouseClick(navMeshAgent);
        }
    }
}
