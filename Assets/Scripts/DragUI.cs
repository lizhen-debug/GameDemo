﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("是否精准拖拽")]
    public bool m_isPrecision = true;

    //存储图片中心点与鼠标点击点的偏移量
    private Vector3 m_offset;

    //存储当前拖拽图片的RectTransform组件
    private RectTransform m_rt;

    void Start()
    {
        //初始化
        m_rt = gameObject.GetComponent<RectTransform>();
    }

    //开始拖拽触发
    public void OnBeginDrag(PointerEventData eventData)
    {
        //如果精准拖拽则进行计算偏移量操作
        if (m_isPrecision)
        {
            //存储点击时的鼠标坐标
            Vector3 tWorldPos;
            //UI屏幕坐标转换为世界坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
            //计算偏移量
            m_offset = transform.position - tWorldPos;
        }
        //否则 默认偏移量为0
        else
        {
            m_offset = Vector3.zero;
        }
        SetDraggedPosition(eventData);
    }

    //拖拽过程中触发
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        m_rt.SetAsLastSibling();
    }

    //结束拖拽触发
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        print(gameObject.name);
        if (eventData.position.x > 540 && eventData.position.x < 1340 && eventData.position.y > 200 && eventData.position.y < 700 && GlobalFunctions.PrepareFoodQTE.is_active == false)
        {
            GlobalFunctions.PrepareFoodQTE.ActiveQTE("PotatoQTE", 30);
        }
        
    }

  
    // 设置图片位置方法
    private void SetDraggedPosition(PointerEventData eventData)
    {
        //存储当前鼠标所在位置
        Vector3 globalMousePos;
        //UI屏幕坐标转换为世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            //设置位置及偏移量
            m_rt.position = globalMousePos + m_offset;
        }
    }

}
