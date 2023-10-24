using UnityEditor;
using System.IO;

using UnityEngine;

using UnityEditor;

namespace PHORIA.Studios.PackMan
{

	public class PackManMenu
	{

		[MenuItem("PackMan/Build/Android")]
		static void BuildForAndroid() => Build(BuildAssetBundleOptions.StrictMode,
		                                       BuildTarget.Android);


		[MenuItem("PackMan/Build/StandaloneWindows64")]
		static void BuildForStandaloneWindows64() => Build(BuildAssetBundleOptions.StrictMode,
		                                                   BuildTarget.StandaloneWindows64);


		[MenuItem("PackMan/Build/StandaloneOSX")]
		static void BuildForStandaloneOSX() => Build(BuildAssetBundleOptions.StrictMode,
		                                             BuildTarget.StandaloneOSX);


		[MenuItem("PackMan/Build/VisionOS")]
		static void BuildForVisionOS() => Build(BuildAssetBundleOptions.StrictMode,
		                                        BuildTarget.VisionOS);


		[MenuItem("PackMan/Build/iOS")]
		static void BuildForiOS() => Build(BuildAssetBundleOptions.StrictMode,
		                                   BuildTarget.iOS);


		public static void Build(BuildAssetBundleOptions options,
		                         BuildTarget buildTarget)
		{
			var assetBundleDirectory = Path.Combine("PackMan/Bundles",
			                                        buildTarget.ToString());

			if (!Directory.Exists(assetBundleDirectory)) Directory.CreateDirectory(assetBundleDirectory);

			var manifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory,
			                                               options,
			                                               buildTarget);
			
			Debug.Log($"Succesfully built [{manifest.name}] for [{buildTarget}]");

			Debug.Log("Contains the following bundles:");

			foreach (var d in manifest.GetAllAssetBundles())
			{
				Debug.Log($"\t• {d}");
			}

//		Debug.Log($"Bundle Hash [{manifest.GetAssetBundleHash(manifest.name)}]");
		}

	}

}