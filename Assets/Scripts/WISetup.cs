using UnityEngine;
using Ionic.Zip;
using System.IO;
// using Ada.Web;

public class WISetup
{
    private static int retry = 0;

    public void Run() {
        try {
            string srcPath = Path.Combine(Application.streamingAssetsPath, "wi.zip");
            string destPath = Path.Combine(Application.persistentDataPath, "wi");

            if (Directory.Exists(destPath)) {
                Directory.Delete(destPath, true);
            }

            if (!Directory.Exists(destPath)) {
                Directory.CreateDirectory(destPath);
            }

#if UNITY_IOS || UNITY_EDITOR
            srcPath = "file://" + srcPath;
#endif            

            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(srcPath);
            www.SendWebRequest();
            while (!www.isDone) {}

            byte[] results = www.downloadHandler.data;
            using (var ms = new MemoryStream(results)) {
                using (ZipFile zip = ZipFile.Read(ms)) {
                    zip.ExtractAll(destPath, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            retry = 0;
            Debug.Log("Web Interface - Setup Success");
        }
        catch (System.Exception e) {
            retry++;
            Debug.LogError("Web Interface - Setup fail " + e);

            if (retry < 3) {
                Run();
                Debug.Log("Web Interface - Retry Web Interface Setup");
            }
        }
    }
}
