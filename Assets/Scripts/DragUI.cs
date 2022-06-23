using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("�Ƿ�׼��ק")]
    public bool m_isPrecision = true;

    //�洢ͼƬ���ĵ�����������ƫ����
    private Vector3 m_offset;

    //�洢��ǰ��קͼƬ��RectTransform���
    private RectTransform m_rt;

    void Start()
    {
        //��ʼ��
        m_rt = gameObject.GetComponent<RectTransform>();
    }

    //��ʼ��ק����
    public void OnBeginDrag(PointerEventData eventData)
    {
        //�����׼��ק����м���ƫ��������
        if (m_isPrecision)
        {
            //�洢���ʱ���������
            Vector3 tWorldPos;
            //UI��Ļ����ת��Ϊ��������
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
            //����ƫ����
            m_offset = transform.position - tWorldPos;
        }
        //���� Ĭ��ƫ����Ϊ0
        else
        {
            m_offset = Vector3.zero;
        }
        SetDraggedPosition(eventData);
    }

    //��ק�����д���
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        m_rt.SetAsLastSibling();
    }

    //������ק����
    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        print(gameObject.name);
        if (eventData.position.x > 540 && eventData.position.x < 1340 && eventData.position.y > 200 && eventData.position.y < 700 && GlobalFunctions.PrepareFoodQTE.is_active == false)
        {
            GlobalFunctions.PrepareFoodQTE.ActiveQTE("PotatoQTE", 30);
        }
        
    }

  
    // ����ͼƬλ�÷���
    private void SetDraggedPosition(PointerEventData eventData)
    {
        //�洢��ǰ�������λ��
        Vector3 globalMousePos;
        //UI��Ļ����ת��Ϊ��������
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            //����λ�ü�ƫ����
            m_rt.position = globalMousePos + m_offset;
        }
    }

}
