# VRM Runtime Importer

This is a package for adding characters to the screen by loading a VRM file while the Unity application is running.

![demo scene](./Doc/demo.jpeg)

This package contains UniVrm and the Runtime File Browser. (These are both free and publicly available assets.)

* [UniVrm](https://github.com/vrm-c/UniVRM)
* [Runtime File Browser](https://assetstore.unity.com/packages/tools/gui/runtime-file-browser-113006?aid=1011liAjm)

The UniVrm version of this package is v0.92.0.

Drag and drop the unipackage file into your Unity project.

[VrmRuntimeImporter_1_0_0.unitypackage](https://github.com/EvelynGameDev/VRMRuntimeImporter/releases/)


## Usage

<!-- The following [Youtube video](http://www.youtube.com/watch?v=QSpa_vyYA1Q) explains how to use it in detail. -->

<!-- [![Animate Vroid 3D model in Unity's URP (Universal Render Pipeline) & Mixamo](https://img.youtube.com/vi/QSpa_vyYA1Q/0.jpg)](http://www.youtube.com/watch?v=QSpa_vyYA1Q) -->

I have prepared a demo scene, which will help you understand how to use it.

__VRMRuntimeImporter Class__

I have a prefab called VRMRuntimeImporter that you can add to the scene you want to use.

You can set the ParentTransform to the parent element where you want the character's game object to appear.

Also, the function registered in Callback will be executed after the VRM file is loaded. You can accept the character's game object and vrm file path as arguments.

```cs
[System.Serializable]
public class LoadCallbackEvent : UnityEvent<GameObject, string> { }
```

![inspector of VRMRuntimeImporter Prefab](./Doc/usage_1.jpeg)

Using the UseVRM method of the VRMRuntimeImporter class will open the file browser. Then select the VRM file in the file browser, and the 3D model of the VRM file will appear on the screen.

![button](./Doc/usage_2.jpeg)

__Sample__

```cs
using UnityEngine;
using VRMRuntimeImporter;

public class Demo : MonoBehaviour
{
    [SerializeField] VRMRuntimeImporter VRMRuntimeImporter;

    private readonly string VRM_FILE_PATH_KEY = "VRM_FILE_PATH_KEY";

    // This is a sample of loading the previously selected VRM file when the game is launched.
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(VRM_FILE_PATH_KEY)) return;

        string path = PlayerPrefs.GetString(VRM_FILE_PATH_KEY);

        if (System.IO.File.Exists(path))
        {
            VRMRuntimeImporter.LoadVrm(path);
        }
    }

    // You can receive the game object and VRM path by registering it in the VRMRuntimeImporter's Callback.
    public void HandleVrmGameObject(GameObject go, string vrmFilepath)
    {
        Debug.Log("HandleVrmGameObject");
        Debug.Log(go);
        Debug.Log(vrmFilepath);
        PlayerPrefs.SetString(VRM_FILE_PATH_KEY, vrmFilepath);
        PlayerPrefs.Save();

        // TODO: Your game's own code here
    }

    // Using the UseVRM method of the VRMRuntimeImporter, the file browser will open and the VRM can be selected; if a VRM file is selected, the loading process will be executed.
    public void OpenFileBrowser()
    {
      VRMRuntimeImporter.UseVRM();
    }
}
```


## License

[MIT](./LICENSE.txt)

## Author

Evelyn GameDev

* [Youtube](https://www.youtube.com/c/EvelynGameDev)
* [Twitter](https://twitter.com/EvelynSoloDev)
* [Blog](https://gamedev.soarhap.com/)

If you like this project, please support my activities.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/S6S52PWUR)

## 日本語READMEはこちら

[日本語README](./README_jp.md)
