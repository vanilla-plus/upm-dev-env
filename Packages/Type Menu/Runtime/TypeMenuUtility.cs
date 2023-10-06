#if UNITY_EDITOR
using System;

using UnityEditor;
using UnityEditor.Events;

using UnityEngine.Events;

using static UnityEngine.Debug;

using Object = UnityEngine.Object;

namespace Vanilla.TypeMenu
{

	public static class TypeMenuUtility
	{

		/// <summary>
		///		This method will copy all the PersistentCalls (from any (Serialized) UnityEvent) to a nominated UnityEvent.
		///
		///		Overloads are available for up to 3 generic parameters. Make sure the event parameter signature matches!
		///
		///		This is useful in the case where a class is being replaced with another class through a custom Editor.
		/// </summary>
		/// <param name="serializedEvent">A SerializedProperty that is a known UnityEvent. It can have 0/1/2/3 type parameters.</param>
		/// <param name="targetEvent">The UnityEvent that the persistent calls will be copied to.</param>
		/// <param name="runLogs">Log a report about each call to the console? This can help with debugging.</param>
		public static void TransferPersistentCalls<TypeA>(SerializedProperty serializedEvent,
		                                                 UnityEvent<TypeA> targetEvent,
		                                                 bool runLogs = false)
		{
			var callGroup = serializedEvent.FindPropertyRelative("m_PersistentCalls.m_Calls");

			for (var i = 0;
			     i < callGroup.arraySize;
			     ++i)
			{
				var call = callGroup.GetArrayElementAtIndex(index: i);

				var (callTarget, callMethodName, callState, callMode) = ExtractCallParams(call: call);

				if (callMode != PersistentListenerMode.EventDefined)
				{
					TransferPersistentCall_Basic(call: call,
					                             callTarget: callTarget,
					                             callMethodName: callMethodName,
					                             callState: callState,
					                             unityEvent: targetEvent,
					                             callMode: callMode,
					                             logCall: runLogs);

				}
				else
				{
					var genericTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                          functionName: callMethodName,
					                                                          argumentTypes: new[] { typeof(TypeA) });

					var genericMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<TypeA>),
					                                                    firstArgument: callTarget,
					                                                    method: genericTargetInfo) as UnityAction<TypeA>;

					UnityEventTools.AddPersistentListener(unityEvent: targetEvent,
					                                      call: genericMethodDelegate);

					targetEvent.SetPersistentListenerState(index: targetEvent.GetPersistentEventCount() - 1,
					                                       state: callState);

					if (runLogs)
					{
						Log(message: $"Retrieved Persistent Call\n\nTarget Object\t[{callTarget.name}]\nTarget Type\t[{callTarget.GetType().Name}]\nMethod Name\t[{callMethodName}]\nCall State\t\t[{callState}]\nArgument Mode\t[EventDefined]\nArgument\t\t[{typeof(TypeA).Name} (Dynamic)]\n");
					}

				}
			}
		}

		/// <summary>
		///		This method will copy all the PersistentCalls (from any (Serialized) UnityEvent) to a nominated UnityEvent.
		///
		///		Overloads are available for up to 3 generic parameters. Make sure the event parameter signature matches!
		/// 
		///		This is useful in the case where a class is being replaced with another class through a custom Editor.
		/// </summary>
		/// <param name="serializedEvent">A SerializedProperty that is a known UnityEvent. It can have 0/1/2/3 type parameters.</param>
		/// <param name="targetEvent">The UnityEvent that the persistent calls will be copied to.</param>
		/// <param name="runLogs">Log a report about each call to the console? This can help with debugging.</param>
		public static void TransferPersistentCalls<TypeA, TypeB>(SerializedProperty serializedEvent,
		                                                        UnityEvent<TypeA, TypeB> targetEvent,
		                                                        bool runLogs = false)
		{
			var callGroup = serializedEvent.FindPropertyRelative("m_PersistentCalls.m_Calls");

			for (var i = 0;
			     i < callGroup.arraySize;
			     ++i)
			{
				var call = callGroup.GetArrayElementAtIndex(index: i);
				
				var (callTarget, callMethodName, callState, callMode) = ExtractCallParams(call: call);

				if (callMode != PersistentListenerMode.EventDefined)
				{

					TransferPersistentCall_Basic(call: call,
					                             callTarget: callTarget,
					                             callMethodName: callMethodName,
					                             callState: callState,
					                             unityEvent: targetEvent,
					                             callMode: callMode,
					                             logCall: runLogs);

				}
				else
				{
					var genericTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                          functionName: callMethodName,
					                                                          argumentTypes: new[] { typeof(TypeA), typeof(TypeB) });

					var genericMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<TypeA, TypeB>),
					                                                    firstArgument: callTarget,
					                                                    method: genericTargetInfo) as UnityAction<TypeA, TypeB>;

					UnityEventTools.AddPersistentListener(unityEvent: targetEvent,
					                                      call: genericMethodDelegate);

					targetEvent.SetPersistentListenerState(index: targetEvent.GetPersistentEventCount() - 1,
					                                       state: callState);

					if (runLogs)
					{
						Log(message: $"Retrieved Persistent Call\n\nTarget Object\t[{callTarget.name}]\nTarget Type\t[{callTarget.GetType().Name}]\nMethod Name\t[{callMethodName}]\nCall State\t\t[{callState}]\nArgument Mode\t[EventDefined]\nArgument\t\t[{typeof(TypeA).Name},{typeof(TypeB).Name} (Dynamic)]\n");
					}

				}
			}
		}


		/// <summary>
		///		This method will copy all the PersistentCalls (from any (Serialized) UnityEvent) to a nominated UnityEvent.
		///
		///		Overloads are available for up to 3 generic parameters. Make sure the event parameter signature matches!
		/// 
		///		This is useful in the case where a class is being replaced with another class through a custom Editor.
		/// </summary>
		/// <param name="serializedEvent">A SerializedProperty that is a known UnityEvent. It can have 0/1/2/3 type parameters.</param>
		/// <param name="targetEvent">The UnityEvent that the persistent calls will be copied to.</param>
		/// <param name="runLogs">Log a report about each call to the console? This can help with debugging.</param>
		public static void TransferPersistentCalls<TypeA, TypeB, TypeC>(SerializedProperty serializedEvent,
		                                                               UnityEvent<TypeA, TypeB, TypeC> targetEvent,
		                                                               bool runLogs = false)
		{
			var callGroup = serializedEvent.FindPropertyRelative("m_PersistentCalls.m_Calls");

			for (var i = 0;
			     i < callGroup.arraySize;
			     ++i)
			{
				var call = callGroup.GetArrayElementAtIndex(index: i);

				var (callTarget, callMethodName, callState, callMode) = ExtractCallParams(call: call);

				if (callMode != PersistentListenerMode.EventDefined)
				{

					TransferPersistentCall_Basic(call: call,
					                             callTarget: callTarget,
					                             callMethodName: callMethodName,
					                             callState: callState,
					                             unityEvent: targetEvent,
					                             callMode: callMode,
					                             logCall: runLogs);

				}
				else
				{
					var genericTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                          functionName: callMethodName,
					                                                          argumentTypes: new[] { typeof(TypeA), typeof(TypeB), typeof(TypeB) });

					var genericMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<TypeA, TypeB, TypeC>),
					                                                    firstArgument: callTarget,
					                                                    method: genericTargetInfo) as UnityAction<TypeA, TypeB, TypeC>;

					UnityEventTools.AddPersistentListener(unityEvent: targetEvent,
					                                      call: genericMethodDelegate);

					targetEvent.SetPersistentListenerState(index: targetEvent.GetPersistentEventCount() - 1,
					                                       state: callState);

					if (runLogs)
					{
						Log(message: $"Retrieved Persistent Call\n\nTarget Object\t[{callTarget.name}]\nTarget Type\t[{callTarget.GetType().Name}]\nMethod Name\t[{callMethodName}]\nCall State\t\t[{callState}]\nArgument Mode\t[EventDefined]\nArgument\t\t[{typeof(TypeA).Name},{typeof(TypeB).Name},{typeof(TypeC).Name} (Dynamic)]\n");
					}

				}
			}
		}

		private static(Object, string, UnityEventCallState, PersistentListenerMode) ExtractCallParams(SerializedProperty call) => (call.FindPropertyRelative(relativePropertyPath: "m_Target").objectReferenceValue,
		                                                                                                                           call.FindPropertyRelative(relativePropertyPath: "m_MethodName").stringValue,
		                                                                                                                           (UnityEventCallState) call.FindPropertyRelative(relativePropertyPath: "m_CallState").intValue,
		                                                                                                                           (PersistentListenerMode) call.FindPropertyRelative(relativePropertyPath: "m_Mode").intValue);


		private static void TransferPersistentCall_Basic(SerializedProperty call,
		                                                 Object callTarget,
		                                                 string callMethodName,
		                                                 UnityEventCallState callState,
		                                                 UnityEventBase unityEvent,
		                                                 PersistentListenerMode callMode,
		                                                 bool logCall = false)
		{
			string argString;

			switch (callMode)
			{
				case PersistentListenerMode.Void:
					argString = "void";

					var voidTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                       functionName: callMethodName,
					                                                       argumentTypes: new Type[0]);

					var voidMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction),
					                                                 firstArgument: callTarget,
					                                                 method: voidTargetInfo) as UnityAction;

					UnityEventTools.AddVoidPersistentListener(unityEvent: unityEvent,
					                                          call: voidMethodDelegate);

					break;

				case PersistentListenerMode.Object:


					var objectArg = call.FindPropertyRelative(relativePropertyPath: "m_Arguments.m_ObjectArgument").objectReferenceValue;

					argString = objectArg.name;

					var objectTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                         functionName: callMethodName,
					                                                         argumentTypes: new[] { typeof(Object) });

					var objectMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<Object>),
					                                                   firstArgument: callTarget,
					                                                   method: objectTargetInfo) as UnityAction<Object>;

					UnityEventTools.AddObjectPersistentListener(unityEvent: unityEvent,
					                                            call: objectMethodDelegate,
					                                            argument: objectArg);

					break;

				case PersistentListenerMode.Int:
					var intArg = call.FindPropertyRelative(relativePropertyPath: "m_Arguments.m_IntArgument").intValue;

					argString = intArg.ToString();

					var intTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                      functionName: callMethodName,
					                                                      argumentTypes: new[] { typeof(int) });

					var intMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<int>),
					                                                firstArgument: callTarget,
					                                                method: intTargetInfo) as UnityAction<int>;

					UnityEventTools.AddIntPersistentListener(unityEvent: unityEvent,
					                                         call: intMethodDelegate,
					                                         argument: intArg);

					break;

				case PersistentListenerMode.Float:
					var floatArg = call.FindPropertyRelative(relativePropertyPath: "m_Arguments.m_FloatArgument").floatValue;

					argString = floatArg.ToString();

					var floatTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                        functionName: callMethodName,
					                                                        argumentTypes: new[] { typeof(float) });

					var floatMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<float>),
					                                                  firstArgument: callTarget,
					                                                  method: floatTargetInfo) as UnityAction<float>;

					UnityEventTools.AddFloatPersistentListener(unityEvent: unityEvent,
					                                           call: floatMethodDelegate,
					                                           argument: floatArg);

					break;

				case PersistentListenerMode.String:
					var stringArg = call.FindPropertyRelative(relativePropertyPath: "m_Arguments.m_StringArgument").stringValue;

					argString = stringArg;

					var stringTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                         functionName: callMethodName,
					                                                         argumentTypes: new[] { typeof(string) });

					var stringMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<string>),
					                                                   firstArgument: callTarget,
					                                                   method: stringTargetInfo) as UnityAction<string>;

					UnityEventTools.AddStringPersistentListener(unityEvent: unityEvent,
					                                            call: stringMethodDelegate,
					                                            argument: stringArg);

					break;

				case PersistentListenerMode.Bool:
					var boolArg = call.FindPropertyRelative(relativePropertyPath: "m_Arguments.m_BoolArgument").boolValue;

					argString = boolArg.ToString();

					var boolTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
					                                                       functionName: callMethodName,
					                                                       argumentTypes: new[] { typeof(bool) });

					var boolMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<bool>),
					                                                 firstArgument: callTarget,
					                                                 method: boolTargetInfo) as UnityAction<bool>;

					UnityEventTools.AddBoolPersistentListener(unityEvent: unityEvent,
					                                          call: boolMethodDelegate,
					                                          argument: boolArg);

					break;

				default: throw new ArgumentOutOfRangeException();
			}

			unityEvent.SetPersistentListenerState(index: unityEvent.GetPersistentEventCount() - 1,
			                                      state: callState);

			if (logCall)
			{
				Log(message: $"Retrieved Persistent Call\n\nTarget Object\t[{callTarget.name}]\nTarget Type\t[{callTarget.GetType().Name}]\nMethod Name\t[{callMethodName}]\nCall State\t\t[{callState.ToString()}]\nArgument Mode\t[{callMode.ToString()}]\nArgument\t\t[{argString}]\n");
			}
		}


//		public static void TransferPersistentCall1Arg<TType>(SerializedProperty call,
//		                                                     UnityEvent<TType> unityEvent,
//		                                                     bool logCall = false)
//		{
//			var callTarget = call.FindPropertyRelative("m_Target").objectReferenceValue;
//
//			var callMethodName = call.FindPropertyRelative("m_MethodName").stringValue;
//
//			var callMode = (PersistentListenerMode) call.FindPropertyRelative("m_Mode").intValue;
//
//			var callState = (UnityEventCallState) call.FindPropertyRelative("m_CallState").intValue;
//
//			var argString = string.Empty;
//
//			switch (callMode)
//			{
//				case PersistentListenerMode.EventDefined:
//					argString = $"{typeof(TType).Name} (Dynamic)";
//
//					var genericTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                          functionName: callMethodName,
//					                                                          argumentTypes: new Type[] { typeof(TType) });
//
//					var genericMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<TType>),
//					                                                    firstArgument: callTarget,
//					                                                    method: genericTargetInfo) as UnityAction<TType>;
//
//					UnityEventTools.AddPersistentListener(unityEvent: unityEvent,
//					                                      call: genericMethodDelegate);
//
//					break;
//
//				case PersistentListenerMode.Void:
//					argString = "void";
//
//					var voidTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                       functionName: callMethodName,
//					                                                       argumentTypes: new Type[0]);
//
//					var voidMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction),
//					                                                 firstArgument: callTarget,
//					                                                 method: voidTargetInfo) as UnityAction;
//
//					UnityEventTools.AddVoidPersistentListener(unityEvent: unityEvent,
//					                                          call: voidMethodDelegate);
//
//					break;
//
//				case PersistentListenerMode.Object:
//
//
//					var objectArg = call.FindPropertyRelative("m_Arguments.m_ObjectArgument").objectReferenceValue;
//
//					argString = objectArg.name;
//
//					var objectTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                         functionName: callMethodName,
//					                                                         argumentTypes: new[] { typeof(Object) });
//
//					var objectMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<Object>),
//					                                                   firstArgument: callTarget,
//					                                                   method: objectTargetInfo) as UnityAction<Object>;
//
//					UnityEventTools.AddObjectPersistentListener(unityEvent: unityEvent,
//					                                            call: objectMethodDelegate,
//					                                            argument: objectArg);
//
//					break;
//
//				case PersistentListenerMode.Int:
//					var intArg = call.FindPropertyRelative("m_Arguments.m_IntArgument").intValue;
//
//					argString = intArg.ToString();
//
//					var intTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                      functionName: callMethodName,
//					                                                      argumentTypes: new[] { typeof(int) });
//
//					var intMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<int>),
//					                                                firstArgument: callTarget,
//					                                                method: intTargetInfo) as UnityAction<int>;
//
//					UnityEventTools.AddIntPersistentListener(unityEvent: unityEvent,
//					                                         call: intMethodDelegate,
//					                                         argument: intArg);
//
//					break;
//
//				case PersistentListenerMode.Float:
//					var floatArg = call.FindPropertyRelative("m_Arguments.m_FloatArgument").floatValue;
//
//					argString = floatArg.ToString();
//
//					var floatTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                        functionName: callMethodName,
//					                                                        argumentTypes: new[] { typeof(float) });
//
//					var floatMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<float>),
//					                                                  firstArgument: callTarget,
//					                                                  method: floatTargetInfo) as UnityAction<float>;
//
//					UnityEventTools.AddFloatPersistentListener(unityEvent: unityEvent,
//					                                           call: floatMethodDelegate,
//					                                           argument: floatArg);
//
//					break;
//
//				case PersistentListenerMode.String:
//					var stringArg = call.FindPropertyRelative("m_Arguments.m_StringArgument").stringValue;
//
//					argString = stringArg;
//
//					var stringTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                         functionName: callMethodName,
//					                                                         argumentTypes: new[] { typeof(string) });
//
//					var stringMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<string>),
//					                                                   firstArgument: callTarget,
//					                                                   method: stringTargetInfo) as UnityAction<string>;
//
//					UnityEventTools.AddStringPersistentListener(unityEvent: unityEvent,
//					                                            call: stringMethodDelegate,
//					                                            argument: stringArg);
//
//					break;
//
//				case PersistentListenerMode.Bool:
//					var boolArg = call.FindPropertyRelative("m_Arguments.m_BoolArgument").boolValue;
//
//					argString = boolArg.ToString();
//
//					var boolTargetInfo = UnityEventBase.GetValidMethodInfo(obj: callTarget,
//					                                                       functionName: callMethodName,
//					                                                       argumentTypes: new[] { typeof(bool) });
//
//					var boolMethodDelegate = Delegate.CreateDelegate(type: typeof(UnityAction<bool>),
//					                                                 firstArgument: callTarget,
//					                                                 method: boolTargetInfo) as UnityAction<bool>;
//
//					UnityEventTools.AddBoolPersistentListener(unityEvent: unityEvent,
//					                                          call: boolMethodDelegate,
//					                                          argument: boolArg);
//
//					break;
//
//				default: throw new ArgumentOutOfRangeException();
//			}
//
//			unityEvent.SetPersistentListenerState(index: unityEvent.GetPersistentEventCount() - 1,
//			                                      state: callState);
//
//			if (logCall)
//			{
//				Log($"Retrieved Persistent Call\n\nTarget Object\t[{callTarget.name}]\nTarget Type\t[{callTarget.GetType().Name}]\nMethod Name\t[{callMethodName}]\nCall State\t\t[{callState.ToString()}]\nArgument Mode\t[{callMode.ToString()}]\nArgument\t\t[{argString}]\n");
//			}
//		}

	}

}
#endif