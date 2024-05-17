#if vanilla_metascript
using System;

using UnityEngine;

using Vanilla.MetaScript;
using Vanilla.MetaScript.DataSources.Strings;
using Vanilla.MetaScript.Debugging;
using Vanilla.MetaScript.Flow;
using Vanilla.TypeMenu;

namespace Vanilla.FileSync
{
	
	[Serializable]
    public class If_Sync_Is_Required : Switch
    {

	    public override void OnValidate()
	    {
		    #if UNITY_EDITOR
		    if (_tasks        == null ||
		        _tasks.Length == 0)
		    {
			    _tasks = new MetaTask[]
			             {
				             new Sync_File_Map(),
				             new Log_To_Console()
				             {
					             taskOptions = 0,
					             MessageSource = new StringSource_Direct
					                             {
						                             Value = "No download required :)"
					                             }
				             }
			             };
		    }

		    base.OnValidate();
		    #endif
	    }

	    protected override string CreateAutoName() => "If FileMap needs synchronizing...";


        public override int Evaluate() => FileSync.FileMapSyncRequired() ?
	                                          0 :
	                                          1;

    }
}
#endif