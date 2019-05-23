using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using live2d;
using live2d.framework;

public class Live2DModel : MonoBehaviour {

    public TextAsset textAst;
    public TextAsset[] motionFile; 
    public Live2DMotion[] motions;
    public Texture2D[] textures;        //贴图数组


    Matrix4x4 transformMar;
    private Live2DModelUnity live2DMode;
    private L2DMotionManager motionManager = new L2DMotionManager();    //动画播放管理器
    private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();
    private L2DTargetPoint dragEff = new L2DTargetPoint();

    // Use this for initialization
    void Start () {
        Live2D.init();
        //Live2DModelUnity asset = Live2DModelUnity.loadModel(Application.dataPath + "/Resources/Epsilon/runtime/Epsilon.moc");
        live2DMode = Live2DModelUnity.loadModel(textAst.bytes);
        for (int i = 0; i < textures.Length; i++)
        {
            live2DMode.setTexture(i, textures[i]);
        }
        float canvasWidth = live2DMode.getCanvasWidth();
        transformMar = Matrix4x4.Ortho(0, canvasWidth, canvasWidth, 0, -50, 50);
         motions = new Live2DMotion[motionFile.Length];
           for (int i = 0; i < motions.Length; i++)
           {
               motions[i] = Live2DMotion.loadMotion(motionFile[i].bytes);
           }
        motions[0].setLoopFadeIn(true);
        motions[0].setLoop(true);

        motionManager.startMotion(motions[0]);

        //眨眼
        eyeBlink.setParam(live2DMode);
 
    }
	
	// Update is called once per frame
	void Update () {
        live2DMode.setMatrix(transform.localToWorldMatrix * transformMar);
        motionManager.updateParam(live2DMode);

        Vector3 mousePos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            dragEff.Set(mousePos.x / Screen.width * 2 - 1, mousePos.y / Screen.height * 2 - 1);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            dragEff.Set(0, 0);
        }
        dragEff.update();
        if (dragEff.getX()!= 0 && dragEff.getY()!=0)
        {
            live2DMode.setParamFloat("PARAM_ANGLE_X", dragEff.getX()*30);
            live2DMode.setParamFloat("PARAM_ANGLE_Y", dragEff.getY()*30);
            live2DMode.setParamFloat("PARAM_EYE_BALL_X", -dragEff.getX());
            live2DMode.setParamFloat("PARAM_EYE_BALL_Y", -dragEff.getY());
            live2DMode.setParamFloat("PARAM_BODY_ANGLE_X", 10*dragEff.getX());
            live2DMode.setParamFloat("PARAM_BODY_ANGLE_Y", 10 * dragEff.getY());
        }
        live2DMode.update();
    }

    private void OnRenderObject()
    {
        live2DMode.draw();
    }
}
