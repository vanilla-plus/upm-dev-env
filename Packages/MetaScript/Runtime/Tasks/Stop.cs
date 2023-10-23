using System;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Stop : MetaTask
	{

		protected override bool CanAutoName() => true;
        
		protected override string CreateAutoName() => "Cancel the current thread";

		public enum StopType
		{

			Local,
			Global,
//			Return

		}

		public StopType stopType = StopType.Local;
		
		protected override UniTask<ExecutionTrace> _Run(ExecutionTrace trace)
		{
			switch (stopType)
			{
				case StopType.Local: 
					trace.scope.Cancel();

//					trace.Continue = false; // LOL this won't work - it's a struct ya dummy. It works by value...
					// Wait, this is even worse than we think! trace.Continue is meaningless as a struct value.
					// The ONLY way to stop "locally" is a Return-type stop.
					// I think the way this will have to go is that any control-exhibiting Tasks (like Sequence, Jump, etc)
					// will need to create a new ExecutionScope instance and parent them like a graph.
					// And yes, this will mean recursively checking them all for cancellation
					// This wouldn't be a problem even at very high depths though, you would need millions of levels for it to be an issue
					// Hell, maybe even stress-test that later INCLUDING some simulated work on a headset so we know if it'll be a problem.
					// The upside of all this fuckin around is that you would be able to cancel one scope at a time,
					// meaning you could effectively skip particular scopes for easier testing.
					break;
				
				case StopType.Global: 
					trace.scope.Cancel_Recursive();
					break;
				
//				case StopType.Return:
//					trace.scope.Cancel();
//					break;
			}
			
			return UniTask.FromResult(trace);
		}

	}
}