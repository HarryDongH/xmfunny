using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Inventory
{
    private static Bag _instance;
    public static Bag Instance
    {
        get
        {
            if(_instance==null)
            {
                GameObject root = GameObject.Find("Canvas");
                _instance = root.transform.Find("BagPanel").GetComponent<Bag>();                
            }
            return _instance;
        }

    }

}
