using System;

using Vanilla.MetaScript.Flow;

namespace Vanilla.FileSync
{
	
	[Serializable]
    public class If_Sync_Is_Required : Switch
    {

        protected override string CreateAutoName() => "If FileMap needs synchronizing...";


        public override int Evaluate() => FileSync.FileMapSyncRequired() ?
	                                          0 :
	                                          1;

    }
}
