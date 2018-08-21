using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour {

    private Text TipText;
    private Text contentText;
    private CanvasGroup canvasGroup;

    private float targetAlpha = 0;
    public float smoothing = 1;

	// Use this for initialization
	void Start () {
        TipText = GetComponent<Text>();
        contentText = transform.Find("Contant").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
	}
    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            //在update的一次调用中deltaTime 更本不可能等于1 所以这个差值运算只能无限接近目标值
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if(Mathf.Abs(canvasGroup.alpha-targetAlpha)<0.01f)//两者之间的距离小于0.05 就让当前值等于目标值
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    public void Show(string text)
    {
        TipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }
    public void Hide()
    {
        targetAlpha = 0;
    }
    public void SetLocalPosition(Vector3 position)//提示面板的位置
    {
        transform.localPosition = position;
    }
}
