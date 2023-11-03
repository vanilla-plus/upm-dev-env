using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.FileSync
{
    
    [Serializable]
    public class Fetch_File_Map : MetaTask
    {
        
        [SerializeField]
        public FetchableDirectory[] directories = Array.Empty<FetchableDirectory>();

        [SerializeField]
        public string[] files = Array.Empty<string>();

        protected override bool CanAutoName() => true;


        protected override string CreateAutoName() => "Build a FileMap";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            FileSync.RemoteFileMap = await FileSync.GetFileMap(directories: directories,
                                                         files: files);

            return scope;
        }

    }
}
