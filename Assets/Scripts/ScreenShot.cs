using UnityEngine;

public class ScreenShot : MonoBehaviour
{ 
    public int superSize = 1;
    public int shotIndex = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            ScreenCapture.CaptureScreenshot($"Screenshot{shotIndex}.png", superSize);
            shotIndex ++;
        }
    }
}