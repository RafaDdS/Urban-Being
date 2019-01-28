using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    [Range(0.001f,0.2f)]
    public float forca;

    Vector3 Linc, LLinc;
    Vector3 Ini;
    Camera thi;

	void Start () {
        Ini = transform.position - Player.Instan.transform.position;
        thi = GetComponent<Camera>();
	}
	
	void Update () {
        if (!Player.Instan.enabled) {
            enabled = false;
            return;
        }

        var inc = ((Player.Instan.transform.position + (Vector3)Player.Instan.rb.velocity * 0.3f + 0.5f * thi.ScreenToWorldPoint(Input.mousePosition) - 1.5f * transform.position + Ini) * forca).y * Vector3.up;
        transform.position += (LLinc.y > Linc.y && inc.y > Linc.y) ? Linc : inc;
        LLinc = Linc;
        Linc = inc;
        //Debug.Log(inc.y);
	}
}
