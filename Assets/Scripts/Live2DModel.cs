using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using live2d;

public class Live2DModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Live2D.init();
        Live2DModelUnity asset = Live2DModelUnity.loadModel(Application.dataPath + "/Resources/Epsilon/runtime/Epsilon.moc");
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
