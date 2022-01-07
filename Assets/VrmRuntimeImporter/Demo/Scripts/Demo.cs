using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMRuntimeImporter
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] VRMRuntimeImporter VRMRuntimeImporter;

        private readonly string VRM_FILE_PATH_KEY = "VRM_FILE_PATH_KEY";

        // This is a sample of loading the previously selected VRM file when the game is launched.
        private void Awake()
        {
            if (!PlayerPrefs.HasKey(VRM_FILE_PATH_KEY)) return;

            string path = PlayerPrefs.GetString(VRM_FILE_PATH_KEY);

            if ((System.IO.File.Exists(path)))
            {
                VRMRuntimeImporter.LoadVrm(path);
            }
        }

        // Using the UseVRM method of the VRMRuntimeImporter, the file browser will open and the VRM can be selected; if a VRM file is selected, the loading process will be executed.
        public void HandleVrmGameObject(GameObject go, string vrmFilepath)
        {
            Debug.Log("HandleVrmGameObject");
            Debug.Log(go);
            Debug.Log(vrmFilepath);
            PlayerPrefs.SetString(VRM_FILE_PATH_KEY, vrmFilepath);
            PlayerPrefs.Save();

            // TODO: Your game's own code here
        }

        // To open the file browser and load the VRM file, use the UseVRM method.
        public void OpenFileBrowser()
        {
            VRMRuntimeImporter.UseVRM();
        }
    }
}
