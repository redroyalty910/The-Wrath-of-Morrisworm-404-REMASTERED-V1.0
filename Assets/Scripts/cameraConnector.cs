using UnityEngine;
using UnityEngine.Video;
//script is used for the video scenes in the title, game over, cutscene, and victory screens
public class cameraConnector : MonoBehaviour
{
    [SerializeField] private Canvas canvasToConnect; 
    void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        Camera cam = Camera.main;  //find the camera when it loads into the scene 

        if (vp != null && cam != null)
        {
            vp.targetCamera = cam;  
        }

        ConnectCanvasToCamera(cam);

    }

    void ConnectCanvasToCamera(Camera cam)   //to set the camera of a canvas to whatever is the current camera in the scene
    {
        if (canvasToConnect != null && cam != null)
        {
            canvasToConnect.renderMode = RenderMode.ScreenSpaceCamera;
            canvasToConnect.worldCamera = cam;
        }
    }
}

