using System;

using Cysharp.Threading.Tasks;

using Vanilla.MetaScript;

namespace Vanilla.Hambundler
{
    
    [Serializable]
    public class Set_Remote_Bundle_Path_Root : MetaTask
    {

        public string RemoteBundlePathRoot = "https://<bucket>.s3.<region>.amazonaws.com";
        
        protected override bool CanAutoName() => !string.IsNullOrWhiteSpace(RemoteBundlePathRoot);
        
        protected override string CreateAutoName() => $"Set Remote Bundle Path Root to [{RemoteBundlePathRoot}]";
        
        protected override UniTask<Scope> _Run(Scope scope)
        {
            Hambundler.RemoteBundlePathRoot = RemoteBundlePathRoot;

            return UniTask.FromResult(scope);
        }

    }
}
