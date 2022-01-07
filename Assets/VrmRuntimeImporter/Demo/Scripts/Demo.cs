using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMRuntimeImporter
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] VRMRuntimeImporter VRMRuntimeImporter;

        private readonly string VRM_FILE_PATH_KEY = "VRM_FILE_PATH_KEY";

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(VRM_FILE_PATH_KEY)) return;

            string path = PlayerPrefs.GetString(VRM_FILE_PATH_KEY);

            if ((System.IO.File.Exists(path)))
            {
                VRMRuntimeImporter.LoadVrm(path);
            }
        }

        public void HandleVrmGameObject(GameObject go, string vrmFilepath)
        {
            Debug.Log("HandleVrmGameObject");
            Debug.Log(go);
            Debug.Log(vrmFilepath);
            // TODO: Your game's own code here
            PlayerPrefs.SetString(VRM_FILE_PATH_KEY, vrmFilepath);
        }
    }
}
