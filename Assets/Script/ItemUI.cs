using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    public Item Item { get; set; }
    public int Amount { get; set; }

    private Image itemImage;
    private Text amountText;

    private Image ItemImage //写成属性的原因的 实力化对象后调用setItem方法 时Start方法还没有执行
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }

    private void Start()
    {
        itemImage = GetComponent<Image>();
        amountText = GetComponentInChildren<Text>();
    }

    private float targetScale = 1f;
    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);
    private float smoothing = 4;

    private void Update()
    {
        if (transform.localScale.x != targetScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < 0.2f)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }

    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Amount == 1)
            AmountText.text = "";
        else
            AmountText.text = Amount.ToString();//生成一个物品他的初始数量为1
    }
    //public void SetItemUI(ItemUI itemUI)
    //{
    //    SetItem(itemUI.Item, itemUI.Amount);
    //}
    public void AddAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        AmountText.text = Amount.ToString();//没达到容量前每次获得的就加1
    }
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount = amount;
        AmountText.text = Amount.ToString();
    }
    public void reduceAmount(int amount)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;
        AmountText.text = Amount.ToString();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
