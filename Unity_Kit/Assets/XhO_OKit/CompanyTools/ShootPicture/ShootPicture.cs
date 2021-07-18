using UnityEngine;

public class ShootPicture : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            string directoryName = Screen.width + "x" + Screen.height;
            string path = Application.dataPath.Replace("/Assets", "/" + Application.productName + "_Screenshot/" + directoryName);
            string imageName = directoryName + "_" + System.Guid.NewGuid() + ".png";

            int fileCount = System.IO.File.Exists(path) ?
                new System.IO.DirectoryInfo(path).GetFiles().Length
                : System.IO.Directory.CreateDirectory(path).GetFiles().Length;

            ScreenCapture.CaptureScreenshot(path + "/" + imageName);
            Debug.Log("***截图成功:" + imageName + "  |***存放路径" + path + "  |***该尺寸数量" + (fileCount + 1));
        }
    }

}


