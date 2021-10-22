using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour {
	// Use this for initialization
	void Awake () {
        var data = GameObject.FindGameObjectWithTag("Data");
        if (data == null)
        {
            var o = Resources.Load("Data") as GameObject;
            Instantiate(o);
            return;
        }
    }
}
