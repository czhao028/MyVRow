using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour {
    private double[] speedArray = new double[] { 0.0, 5, 10, 20, 30, 50, 5};
    private int counting = 0;
    public float factor;
	
    void Update () {
        counting += 1;
        transform.position += new Vector3(0f, 0f, (float)speedArray[counting % speedArray.Length]*factor);
        
	}
}
