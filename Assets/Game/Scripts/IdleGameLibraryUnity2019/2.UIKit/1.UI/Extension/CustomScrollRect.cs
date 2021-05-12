/*
 * 摘自：https://blog.csdn.net/linxinfa/article/details/105349046
 * 游戏开发过程中，很可能需要制作横竖嵌套的滑动列表。
 * Unity的滑动列表会根据用户的操作行为捕获到对应的事件，但是Unity的事件一旦被上层UI捕获，下层UI就不会响应，如果是嵌套列表，那么二级列表就会劫持掉事件，导致一级列表无法拖动。
 * 
 *  实现原理:
 *  要解决滑动列表嵌套的这个问题，可以根据用户滑动的方向，来进行事件的透传，比如在横向滑动的区域，如果用户是进行竖向滑动，则把事件透传到父级列表，如果父级列表是竖向滑动列表，则可以进行响应，否则继续透传。
 *  
 *  UI事件透传接口:
 *  ExecuteEvents.Execute<T>(GameObject target, BaseEventData eventData, EventFunction<T> functor) where T : IEventSystemHandler;
 *  
 *  ScrollRect_V为竖向滑动，ScrollRect_H为横向滑动。  
 *  ScrollRect_V节点和ScrollRect_H节点要挂CustomScrollRect脚本，并设置好Content对象，根据滑动放心勾选Horizontal或Vertical。
 *  ScrollRect_V节点要挂Rect Mask 2D组件，否则滑动列表没有裁切效果。
 *  ScrollRect_H节点的ChildContent挂上布局组件: Horizontal Layout Group、Content Size Fitter。
 *  
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 横竖嵌套的滑动列表
/// </summary>
public class CustomScrollRect : ScrollRect
{
    //[ShowInInspector]
    //父CustomScrollRect对象
    [SerializeField]
    private CustomScrollRect m_Parent;
    //public CustomScrollRect m_Parent;

    public enum Direction
    {
        Horizontal,
        Vertical
    }
    //滑动方向
    private Direction m_Direction = Direction.Horizontal;
    //当前操作方向
    private Direction m_BeginDragDirection = Direction.Horizontal;

    protected override void Awake()
    {

        //找到父对象
        if ( transform.parent != null && transform.parent.parent != null && transform.parent.parent.parent != null ) {
            Transform parent = transform.parent.parent.parent.parent;
            if ( parent ) {
                m_Parent = parent.GetComponentInParent<CustomScrollRect>();
            }
        }
        
        m_Direction = this.horizontal ? Direction.Horizontal : Direction.Vertical;
    }


    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (m_Parent)
        {
            m_BeginDragDirection = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y) ? Direction.Horizontal : Direction.Vertical;
            //m_BeginDragDirection = Mathf.Abs(eventData.delta.x) - Mathf.Abs(eventData.delta.y) > 50 ? Direction.Horizontal : Direction.Vertical;
            if (m_BeginDragDirection != m_Direction)
            {
                //当前操作方向不等于滑动方向，将事件传给父对象
                ExecuteEvents.Execute(m_Parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
                return;
            }
        }

        base.OnBeginDrag(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (m_Parent)
        {
            if (m_BeginDragDirection != m_Direction)
            {
                //当前操作方向不等于滑动方向，将事件传给父对象
                ExecuteEvents.Execute(m_Parent.gameObject, eventData, ExecuteEvents.dragHandler);
                return;
            }
        }
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (m_Parent)
        {
            if (m_BeginDragDirection != m_Direction)
            {
                //当前操作方向不等于滑动方向，将事件传给父对象
                ExecuteEvents.Execute(m_Parent.gameObject, eventData, ExecuteEvents.endDragHandler);
                return;
            }
        }
        base.OnEndDrag(eventData);
    }

    public override void OnScroll(PointerEventData data)
    {
        if (m_Parent)
        {
            if (m_BeginDragDirection != m_Direction)
            {
                //当前操作方向不等于滑动方向，将事件传给父对象
                ExecuteEvents.Execute(m_Parent.gameObject, data, ExecuteEvents.scrollHandler);
                return;
            }
        }
        base.OnScroll(data);
    }
}

