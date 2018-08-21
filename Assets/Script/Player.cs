using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 3);
            Bag.Instance.StoreItem(id);
        }
	}
}
