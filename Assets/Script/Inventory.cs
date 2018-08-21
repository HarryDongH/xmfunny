using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private Slot[] slotList;
	// Use this for initialization
	public virtual void Start () {
        slotList= GetComponentsInChildren<Slot>();
	}

    public bool StoreItem(int id)//物品是否存储成功
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if(item==null)
        {
            Debug.LogWarning("要存储的物品id不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else
            {
                slot.StoreItem(item);//把物品存储到这个空的物品槽里面
            }
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    emptySlot.StoreItem(item);
                }
                else
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// 用此方法找到一个空的slot
    /// </summary>
    /// <returns></returns> 
    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }          
        }
        return null;
    }
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount >= 1&& slot.GetItemId()==item.ID&&slot.IsFilled()==false)
            {
                return slot;
            }
        }
        return null;
    }
}
