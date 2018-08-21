using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public GameObject itemPrefab;


    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localScale = Vector3.one;
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }

    /// <summary>
    /// 得到当前物品槽的第一个子物品的 类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.Type;
    }
    /// <summary>
    /// 得到当前物品槽的第一个子物品的 Id
    /// </summary>
    /// <returns></returns>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;//当前数量大于等于容量
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string tip = transform.GetChild(0).GetComponent<ItemUI>().Item.GetTipText();
            InventoryManager.Instance.ShowTip(tip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            InventoryManager.Instance.HideTip();
        }
    }

    public void OnPointerDown(PointerEventData eventData)  
    {
        if (transform.childCount > 0)//当前物品槽不为空时
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            if (InventoryManager.Instance.IsPickedItem == false)//当前手上没有任何物品
            {
                Debug.Log("使用");
            }
            else//手上有物品
            {
                if (currentItem.Item.ID == InventoryManager.Instance.PickedItem.Item.ID)//同id的物品
                {
                    if (currentItem.Item.Capacity > currentItem.Amount)//当前的物品槽还有容量
                    {
                        int amountRemain = currentItem.Item.Capacity - currentItem.Amount;//当前物品槽剩余空间
                        if (InventoryManager.Instance.PickedItem.Amount > amountRemain)//手上大于 剩余量
                        {
                            currentItem.SetAmount(currentItem.Amount + amountRemain);
                            InventoryManager.Instance.RemoveItem(amountRemain);
                        }
                        else//手上小于剩余量
                        {
                            currentItem.AddAmount(InventoryManager.Instance.PickedItem.Amount);//槽内加上手上的数量
                            InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else//id不同的物品
                {
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    currentItem.SetItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                    InventoryManager.Instance.PickedItem.SetItem(item, amount);
                }
            }
        }
        else//点击的物品槽是空的时候
        {
            if (InventoryManager.Instance.IsPickedItem == true)//手上有东西
            {
                for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                }
                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
            }
        }
        //自身是空的 1，pickedItem!=null 放置item到这个槽位
        //按下ctrl

            //没有按下ctrl                                                      //物品比较少一次取一个就不做ctrl了
            //2 pickedItem==null 不做任何处理
            //自身不是空 1，pickedItem!=null item与槽位上的物品交换
            //槽位物品上的id==pickedItem//可以放的下和只能放下一部分
            //是否按下了ctrl
            //槽位物品上的id!=pickedItem  交换
            //2 pickedItem==null 槽位上的物品和pickeditem交换
            //是否按下ctrl

            //if (transform.childCount > 0)//当前物品槽不为空时
            //{
            //    ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            //    if (InventoryManager.Instance.IsPickedItem == false)//当前手上没有任何物品
            //    {
            //        //if (Input.GetKey(KeyCode.LeftControl))
            //        //{
            //        //    int amountPicked = (currentItem.Amount + 1) / 2;
            //        //    InventoryManager.Instance.PickupItem(currentItem.Item, amountPicked);
            //        //    int amountRemained = currentItem.Amount - amountPicked;
            //        //    if (amountRemained <= 0)
            //        //    {
            //        //        Destroy(currentItem.gameObject);
            //        //    }
            //        //    else
            //        //    {
            //        //        currentItem.SetAmount(amountRemained);
            //        //    }
            //        //}
            //        InventoryManager.Instance.PickupItem(currentItem.Item, currentItem.Amount);
            //        Destroy(currentItem.gameObject);//销毁当前物品
            //    }
            //    else//当前的鼠标上有物品
            //    {
            //        if (currentItem.Item.ID == InventoryManager.Instance.PickedItem.Item.ID)//同id的物品
            //        {
            //            if (currentItem.Item.Capacity > currentItem.Amount)//当前的物品槽还有容量
            //            {
            //                int amountRemain = currentItem.Item.Capacity = currentItem.Item.Capacity - currentItem.Amount;//当前物品槽剩余空间
            //                if (InventoryManager.Instance.PickedItem.Amount > amountRemain)//手上大于 剩余量
            //                {
            //                    currentItem.SetAmount(currentItem.Amount + amountRemain);
            //                    InventoryManager.Instance.RemoveItem(amountRemain);
            //                }
            //                else//手上小于剩余量
            //                {
            //                    currentItem.AddAmount(InventoryManager.Instance.PickedItem.Amount);//槽内加上手上的数量
            //                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
            //                }
            //            }
            //            else
            //            {
            //                return;
            //            }
            //        }
            //        else//id不同的物品
            //        {

            //        }
            //    }
            //}
            //else//当前物品槽自身为空的情况下
            //{
            //    if (InventoryManager.Instance.IsPickedItem == true)//手上有东西
            //    {
            //        for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
            //        {
            //            this.StoreItem(InventoryManager.Instance.PickedItem.Item);
            //        }
            //        InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
            //    }
            //    else//手上没东西
            //    {
            //        return;
            //    }
            //}
    }

    public void OnBeginDrag(PointerEventData eventData)//开始推拽
    {
        if (transform.childCount > 0)//当前物品槽不为空时
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            if (InventoryManager.Instance.IsPickedItem == false)//当前手上没有任何物品
            {
                //if (Input.GetKey(KeyCode.LeftControl))
                //{
                //    int amountPicked = (currentItem.Amount + 1) / 2;
                //    InventoryManager.Instance.PickupItem(currentItem.Item, amountPicked);
                //    int amountRemained = currentItem.Amount - amountPicked;
                //    if (amountRemained <= 0)
                //    {
                //        Destroy(currentItem.gameObject);
                //    }
                //    else
                //    {
                //        currentItem.SetAmount(amountRemained);
                //    }
                //}
                InventoryManager.Instance.PickupItem(currentItem.Item, currentItem.Amount);
                Destroy(currentItem.gameObject);//销毁当前物品
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("正再拖拽！");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;//获取到鼠标终点位置下 可能的物体
        if(go==null)//如果放置点没有检测到格子
        {
            //这个条件是避免从这里脱出的时候 加出上限
            if (transform.childCount == 0)
            {
                for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                }
                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
            }
            //else if (transform.GetComponentInChildren<ItemUI>().Amount < transform.GetComponentInChildren<ItemUI>().Item.Capacity)
            //{
            //    for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
            //    {
            //        this.StoreItem(InventoryManager.Instance.PickedItem.Item);
            //    }
            //    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
            //}
        }
        else if (go.transform.childCount == 0)//当前物品槽自身为空的情况下
        {
            if (InventoryManager.Instance.IsPickedItem == true)//手上有东西
            {
                for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                {
                    go.GetComponent<Slot>().StoreItem(InventoryManager.Instance.PickedItem.Item);
                }
                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
            }
            else//手上没东西
            {
                this.StoreItem(InventoryManager.Instance.PickedItem.Item);
            }
        }
        else if(go.transform.childCount == 1)//当前物品槽有物品
        {
            ItemUI currentItem = go.transform.GetChild(0).GetComponent<ItemUI>();        
            if (InventoryManager.Instance.IsPickedItem == true)//当前的鼠标上有物品
            {
                if (currentItem.Item.ID == InventoryManager.Instance.PickedItem.Item.ID)//同id的物品
                {
                    if (currentItem.Item.Capacity > currentItem.Amount)//当前的物品槽还有容量
                    {
                        int amountRemain = currentItem.Item.Capacity - currentItem.Amount;//当前物品槽剩余空间
                        if (InventoryManager.Instance.PickedItem.Amount > amountRemain)//手上大于 剩余量
                        {
                            currentItem.SetAmount(currentItem.Amount + amountRemain);
                            InventoryManager.Instance.RemoveItem(amountRemain);
                        }
                        else//手上小于剩余量
                        {
                            currentItem.AddAmount(InventoryManager.Instance.PickedItem.Amount);//槽内加上手上的数量
                            InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else//id不同的物品
                {
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    currentItem.SetItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                    InventoryManager.Instance.PickedItem.SetItem(item, amount);
                }
            }
        }
    }
}
