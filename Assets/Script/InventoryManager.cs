using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryManager : MonoBehaviour {

    private List<Item> itemList;
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }

    private Tip tip;
    private bool isTipShow = false;//tip显示状态
    
    private bool isPickedItem = false;//当前是否选择了物品
    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }
    }
    
    private Canvas canvas;

    private ItemUI pickedItem;//鼠标选中的物体
    public ItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
    }
     
    void Start()
    {
        ParseItemJson();
        tip = GameObject.FindObjectOfType<Tip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();
    } 
    void Update()
    {
        if (isPickedItem == true)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            pickedItem.SetLocalPosition(position);
        }
        else if (isTipShow == true)
        {
            //控制提示面板跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            tip.SetLocalPosition(position+new Vector2(10,-10));//后面加的是关于中心的的偏移
        }
        
    }
    /// <summary>
    /// 解析物品信息
    /// </summary>
    void ParseItemJson()
    {
        itemList = new List<Item>();
        //文本在unity中是textAsset类型 
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;//物品信息的json格式
        JsonData iJsonData = JsonMapper.ToObject(itemsJson);
        foreach (JsonData i in iJsonData)
        {
            //Debug.Log(i["name"].ToString());  测试
            string typeString = i["type"].ToString();
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeString);//根据字符串来获取枚举值
            int id = int.Parse(i["id"].ToString());
            string name = i["name"].ToString();
            string description = i["description"].ToString();
            int capacity = int.Parse(i["capacity"].ToString());
            string sprite = i["sprite"].ToString();

            Item item = new Item(id, name, type, description, capacity, sprite);
            //switch (type)
            //{
            //    case Item.ItemType.Consumable:
            //        break;
            //    case Item.ItemType.Equipment:
            //        break;
            //    case Item.ItemType.Tool:
            //        break;
            //    default:
            //        break;
            //}
            itemList.Add(item);
            Debug.Log(item);
        }
    }
    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if(item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowTip(string content)
    {
        if (this.isPickedItem)
            return;
        isTipShow = true;
        tip.Show(content);
    }
    public void HideTip()
    {
        isTipShow = false;
        tip.Hide();
    }
    //捡起物品槽中指定数量的物品
    public void PickupItem(Item item,int amount)
    {
        PickedItem.SetItem(item,amount);
        PickedItem.Show();
        isPickedItem = true;
        this.tip.Hide();
    }
    /// <summary>
    /// 把手上的物品放入物品槽内
    /// </summary>
    //public void RemoveItem()
    //{
    //    PickedItem.Amount = 0;
    //    isPickedItem = false;
    //    pickedItem.Hide();
    //}
    
    ///移除自定个数的物品
    public void RemoveItem(int amount=1)
    {
        PickedItem.reduceAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
        //isPickedItem = false;
        //pickedItem.Hide();
    }
} 
