using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraFixer : MonoBehaviour
{
    public bool IsCanvas = false;
    /// <summary>
    /// 开发屏幕的宽
    /// </summary>
    public static float DevelopWidth = 1024f;

    /// <summary>
    /// 开发屏幕的长
    /// </summary>
    public static float DevelopHeigh = 576f;

    /// <summary>
    /// 开发高宽比
    /// </summary>
    public static float DevelopRate = DevelopHeigh /DevelopWidth;

    /// <summary>
    /// 设备自身的高
    /// </summary>
    public static int curScreenHeight = Screen.height;

    /// <summary>
    /// 设备自身的高
    /// </summary>
    public static int curScreenWidth = Screen.width;

    /// <summary>
    /// 当前屏幕高宽比
    /// </summary>
    public static float ScreenRate = (float)Screen.height / (float)Screen.width;
    
    /// <summary>
    /// 世界摄像机rect高的比例
    /// </summary>
    public static float cameraRectHeightRate = DevelopHeigh / ((DevelopWidth / Screen.width) * Screen.height);

    /// <summary>
    /// 世界摄像机rect宽的比例
    /// </summary>
    public static float cameraRectWidthRate = DevelopWidth / ((DevelopHeigh / Screen.height) * Screen.width);

    public void FitCamera(Camera camera)
    {
        ///适配屏幕。实际屏幕比例<=开发比例的 上下黑  反之左右黑
        if (DevelopRate <= ScreenRate)
        {
            camera.rect = new Rect(0, (1 - cameraRectHeightRate) / 2, 1, cameraRectHeightRate);
        }
        else
        {
            camera.rect = new Rect((1 - cameraRectWidthRate) / 2, 0, cameraRectWidthRate, 1);
        }
    }

    public void FitCanvas(CanvasScaler canvas)
    {
        Debug.Log("fixed:" + DevelopRate + "," + ScreenRate + " with " + canvas.name);
        if (DevelopRate <= ScreenRate)
        {
            canvas.matchWidthOrHeight = (1f - DevelopRate / ScreenRate) / DevelopRate;
        }
        else
        {
            canvas.matchWidthOrHeight = (DevelopRate / ScreenRate - 1f) / DevelopRate;
        }
    }
    void Awake()
    {
        if(IsCanvas) return;
        FitCamera(this.GetComponent<Camera>());
    }

    private void Start() {
        for(int i = 0;i < SceneManager.sceneCount;i++){
            Scene s = SceneManager.GetSceneAt(i);
            foreach(GameObject g in s.GetRootGameObjects()){
                CanvasScaler c = g.GetComponent<CanvasScaler>();
                if(c!=null) FitCanvas(c);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
