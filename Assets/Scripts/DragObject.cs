using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DragObject : MonoBehaviour
{
    /// <summary>
    /// 将要拖动的物体
    /// </summary>
    private Transform _dragGameObject;

    /// <summary>
    /// 获取射线需要碰撞的层
    /// </summary>
    private LayerMask _canDrag;

    /// <summary>
    /// 直接从外部定义好层，简单理解
    /// </summary>
    public LayerMask canDrag2;

    /// <summary>
    /// 获得鼠标的位置和cube位置差
    /// </summary>
    private Vector3 _offset;

    /// <summary>
    /// 是否点击到cube
    /// </summary>
    private bool _isClickCube;

    /// <summary>
    /// 目标对象的屏幕坐标
    /// </summary>
    private Vector3 _targetScreenPoint;

    //限制拖动范围的最小值和最大值
    public float xMin, xMax, yMin, yMax, zMin, zMax;

    // Use this for initialization
    private void Start()
    {
        _canDrag = 1 << LayerMask.NameToLayer("Food");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckGameObject())
            {
                _offset = _dragGameObject.transform.position -
                          Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                              _targetScreenPoint.z));
            }
        }

        if (_isClickCube)
        {
            if (_dragGameObject.tag == "sliced_food")
            {
                foreach(Transform gameObject in _dragGameObject)
                {
                    //print(gameObject.transform.position - _dragGameObject.position);
                    gameObject.GetComponent<Rigidbody>().AddForce(100 * (_dragGameObject.position - gameObject.transform.position));
                    if ((_dragGameObject.position - gameObject.transform.position).magnitude < 0.01)
                    {
                        gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }

            //当前鼠标所在的屏幕坐标
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _targetScreenPoint.z);
            //把当前鼠标的屏幕坐标转换成世界坐标
            Vector3 curWorldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
            _dragGameObject.position = curWorldPoint + _offset;

            //限制拖动范围
            _dragGameObject.transform.position = new Vector3(
                Mathf.Clamp(_dragGameObject.transform.position.x, xMin, xMax),
                Mathf.Clamp(_dragGameObject.transform.position.y, yMin, yMax),
                Mathf.Clamp(_dragGameObject.transform.position.z, zMin, zMax)
            );
            

        }

        if (Input.GetMouseButtonUp(0))
        {
            _isClickCube = false;
            if(_dragGameObject)
            {
                if (_dragGameObject.GetComponent<Rigidbody>())
                    _dragGameObject.GetComponent<Rigidbody>().isKinematic = false;
                else
                    foreach (Transform gameObject in _dragGameObject)
                    {
                        gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    }

            }

            

                //恢复拖拽物体的Y轴为原点
                //_dragGameObject.GetComponent<Rigidbody>().transform.position = new Vector3(
                //    _dragGameObject.GetComponent<Rigidbody>().transform.position.x, 0,
                //    _dragGameObject.GetComponent<Rigidbody>().transform.position.z);
            }
    }

    /// <summary>
    /// 检查是否点击到cbue
    /// </summary>
    /// <returns></returns>
    private bool CheckGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, _canDrag))
        {

            _isClickCube = true;
            
            _dragGameObject = hitInfo.collider.gameObject.transform;

            if (_dragGameObject.tag == "sliced_food")
            {
                // create a new game object and remove the old one when drag
                GameObject new_drag = new GameObject(_dragGameObject.parent.name);
                Transform old_drag = _dragGameObject.parent;
                new_drag.transform.position = _dragGameObject.transform.position;
                List<Transform> children = new List<Transform>();
                foreach (Transform child in old_drag)
                {
                    children.Add(child);
                }
                foreach (Transform child in children)
                {
                    child.SetParent(new_drag.transform);
                    //child.GetComponent<Rigidbody>().isKinematic = true;
                }
                Destroy(old_drag.gameObject);

                new_drag.AddComponent<Outline>().enabled = false;
                new_drag.GetComponent<Outline>().OutlineColor = Color.green;
                _targetScreenPoint = Camera.main.WorldToScreenPoint(new_drag.transform.position);
                _dragGameObject = new_drag.transform;
                _dragGameObject.tag = "sliced_food";
            }
            else
            {
                _targetScreenPoint = Camera.main.WorldToScreenPoint(_dragGameObject.position);
                _dragGameObject.GetComponent<Rigidbody>().isKinematic = true;
            }


            return true;
        }

        return false;
    }
}