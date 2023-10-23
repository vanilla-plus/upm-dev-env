using System;
//
//public enum TaskExecutionType
//{
//
//	RunAndWait,
//	RunAndDontWait,
//	Skip,
//	Stop,
//	Cancel
//	
//}
//
//[Flags]
//public enum ExecutionOptions
//{
//
//	Run            = 1,
//	Wait           = 2,
//	CancelLocal  = 4,
//	CancelGlobal = 8,
//
//}

[Flags]
public enum ExecutionOptions
{

	Run          = 1,
	Wait         = 2,
//	CancelLocal  = 4,
//	CancelGlobal = 8,

}