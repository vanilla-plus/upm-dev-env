using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Vanilla.FolderToBundle.Editor
{

    public class FolderToBundleMenu
    {

        /// <summary>
        ///     Flips any instances of the wrong slash in a path (i.e. \ instead of /) to the correct slash for this platform.
        /// </summary>
        private static string CorrectDirectorySeperators(string directory) =>
            directory.Replace(oldChar: Path.AltDirectorySeparatorChar,
                              newChar: Path.DirectorySeparatorChar);


        /// <summary>
        ///     Turns a project-relative path (i.e. one that starts from the Assets folder) into a full path (i.e. D:/Projects/etc)
        /// </summary>
        private static string GlobalizeLocalPath(string directory) => $"{Application.dataPath.Substring(startIndex: 0, length: Application.dataPath.Length - 6)}{directory}";


        // ------------------------------------------------------------------------------------------------ Context Menu //


        // This Context Menu option will be greyed out if this method returns false
        [MenuItem(itemName: "FolderToBundle/Export Folder",
                  isValidateFunction: true)]
        private static bool NewMenuOptionValidation()
        {
            var path = AssetDatabase.GetAssetPath(assetObject: Selection.activeObject);

            return Directory.Exists(path: path) && path.StartsWith("Assets"); // Otherwise we would be able to bundle up Packages!
        }


        [MenuItem(itemName: "FolderToBundle/Export Folder")]
        private static void ExportFolderAsAssetBundle()
        {
            var outputPath = GlobalizeLocalPath(directory: $"Assets{Path.DirectorySeparatorChar}+FolderToBundle");

            outputPath = CorrectDirectorySeperators(outputPath);

            if (!Directory.Exists(path: outputPath))
            {
                Directory.CreateDirectory(path: outputPath);
            }

            var localPath  = AssetDatabase.GetAssetPath(assetObject: Selection.activeObject); // "Assets/Blah/Stuff"
            var bundleName = Selection.activeObject.name;                                     // "Stuff"

            RecursiveAssetBundlerDrill(startingDirectory: localPath,
                                       bundleName: ref bundleName);

            AssetDatabase.RemoveUnusedAssetBundleNames();

            var assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName: bundleName.ToLower());

            Debug.Log(assetNames.Aggregate(seed: $"Attempting to build the bundle [{bundleName}] which will contain the following assets:\n\n",
                                           func: (current,
                                                  next) => current + next + "\n"));

            BuildPipeline.BuildAssetBundles(outputPath: outputPath,
                                            builds: new[]
                                                    {
                                                        new AssetBundleBuild
                                                        {
                                                            assetBundleName = bundleName,
                                                            assetNames      = assetNames
                                                        }
                                                    },
                                            assetBundleOptions: BuildAssetBundleOptions.ChunkBasedCompression,
                                            targetPlatform: EditorUserBuildSettings.activeBuildTarget);

            AssetDatabase.Refresh();
        }


        // ----------------------------------------------------------------------------------------------------- Helpers //


        /// <summary>
        ///     This function will iterate through all files in all subdirectories of the given path and,
        ///     if the file isn't a meta file (which aren't required in AssetBundles?), will assign it to a
        ///     bundle of the given bundleName.
        /// </summary>
        ///
        /// <param name="startingDirectory">
        ///     What directory should we iterate through?
        /// </param>
        ///
        /// <param name="bundleName">
        ///    What name should this new bundle go by?
        /// </param>
        private static void RecursiveAssetBundlerDrill(string     startingDirectory,
                                                       ref string bundleName)
        {
            // Add all files in the starting directory
            foreach (var fileName in Directory.GetFiles(path: startingDirectory))
            {
                AddFileToBundle(fileName: fileName,
                                bundleName: ref bundleName);
            }

            // Drill down through all the subdirectories and do the same thing for them
            foreach (var directory in Directory.GetDirectories(path: startingDirectory))
            {
                RecursiveAssetBundlerDrill(startingDirectory: directory,
                                           bundleName: ref bundleName);
            }
        }


        private static void AddFileToBundle(string     fileName,
                                            ref string bundleName)
        {
            // Not doing this logs the warning: 
            // Getting the AssetImporter for asset at path [Assets\SomeFolder\Potato.mat.meta] failed.
            if (fileName.EndsWith(value: ".meta")) return;

            var fixedFilename = CorrectDirectorySeperators(fileName);

            Debug.Log(message: $"Working on asset at path [{fixedFilename}]");

            var assetImporter = AssetImporter.GetAtPath(path: fixedFilename);

            if (assetImporter == null)
            {
                Debug.LogWarning(message: $"Getting the AssetImporter for asset at path [{fixedFilename}] failed.");

                return;
            }

            assetImporter.assetBundleName = bundleName;

            assetImporter.SaveAndReimport();

            Debug.Log($"AssetImporter success for [{fixedFilename}] -> [{bundleName}.assetbundle]");
        }

    }

}