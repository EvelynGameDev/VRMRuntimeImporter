using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using VRM;
using UniGLTF;
using UnityEngine.Events;

namespace VRMRuntimeImporter
{
	public class VRMRuntimeImporter : MonoBehaviour
	{
		[Header("The parent game object where you want to place the character.")]
		[SerializeField] Transform ParentTransform;

		[Header("To execute after the VRM file is loaded")]
		[SerializeField] LoadCallbackEvent Callback;

		private GameObject _prevVRMGameObject = null;

		public void UseVRM()
		{
			FileBrowser.SetFilters(false, new FileBrowser.Filter("VRM", ".vrm"));
			FileBrowser.SetDefaultFilter(".vrm");
			FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
			StartCoroutine(ShowLoadDialogCoroutine());
		}

		IEnumerator ShowLoadDialogCoroutine()
		{
			yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load VRM FILE", "Load");

			Debug.Log(FileBrowser.Success);

			if (FileBrowser.Success)
			{
				for (int i = 0; i < FileBrowser.Result.Length; i++)
					Debug.Log(FileBrowser.Result[i]);

				byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

				if (!(System.IO.Directory.Exists(Application.persistentDataPath + "/vrm")))
				{
					System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/vrm");
				}
				DeletePrevVrmFiles();
				string destinationPath = Path.Combine(Application.persistentDataPath + "/vrm", FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));

				Debug.Log(destinationPath);

				FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
				if (_prevVRMGameObject != null)
				{
					DestroyVrm(_prevVRMGameObject);
				}
				GameObject go = LoadVrm(destinationPath);
				if (ParentTransform != null)
				{
					go.transform.SetParent(ParentTransform);
				}
				if (Callback != null)
				{
					Callback.Invoke(go);
				}
				_prevVRMGameObject = go;
			}
		}

		private GameObject LoadVrm(string vrmFilePath)
		{
			GlbFileParser parser = new GlbFileParser(vrmFilePath);
			GltfData gltfData = parser.Parse();
			VRMData vrm = new VRMData(gltfData);

			using (var context = new VRMImporterContext(vrm))
			{
				RuntimeGltfInstance instance = context.Load();
				instance.EnableUpdateWhenOffscreen();
				instance.ShowMeshes();
				return instance.Root;
			}
		}

		private void DestroyVrm(GameObject vrmGameObject)
		{
			UnityEngine.Object.Destroy(vrmGameObject);
			_prevVRMGameObject = null;
		}

		private void DeletePrevVrmFiles()
		{
			string[] fs = System.IO.Directory.GetFiles(Application.persistentDataPath + "/vrm", "*");
			foreach (string filePath in fs)
			{
				if (!(System.IO.File.Exists(filePath))) continue;
				Debug.Log(filePath);
				System.IO.File.Delete(filePath);
			}
		}
	}

	[System.Serializable]
	public class LoadCallbackEvent : UnityEvent<GameObject> { }
}
