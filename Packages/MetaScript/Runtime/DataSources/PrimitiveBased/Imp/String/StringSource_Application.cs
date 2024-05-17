using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Strings
{
    
	[SerializeField] public class StringSource_Application_Identifier : StringSource { public override          string Value { get => Application.identifier;          set { } } public override string ToString() => "<Application.identifier>"; }
	[SerializeField] public class StringSource_Application_Version : StringSource { public override             string Value { get => Application.version;             set { } } public override string ToString() => "<Application.version>"; }
	[SerializeField] public class StringSource_Application_CompanyName : StringSource { public override         string Value { get => Application.companyName;         set { } } public override string ToString() => "<Application.companyName>"; }
	[SerializeField] public class StringSource_Application_DataPath : StringSource { public override            string Value { get => Application.dataPath;            set { } } public override string ToString() => "<Application.dataPath>"; }
	[SerializeField] public class StringSource_Application_ProductName : StringSource { public override         string Value { get => Application.productName;         set { } } public override string ToString() => "<Application.productName>"; }
	[SerializeField] public class StringSource_Application_UnityVersion : StringSource { public override        string Value { get => Application.unityVersion;        set { } } public override string ToString() => "<Application.unityVersion>"; }
	[SerializeField] public class StringSource_Application_PersistentDataPath : StringSource { public override  string Value { get => Application.persistentDataPath;  set { } } public override string ToString() => "<Application.persistentDataPath>"; }
	[SerializeField] public class StringSource_Application_StreamingAssetsPath : StringSource { public override string Value { get => Application.streamingAssetsPath; set { } } public override string ToString() => "<Application.streamingAssetsPath>"; }
	[SerializeField] public class StringSource_Application_Platform : StringSource { public override            string Value { get => Application.platform.ToString(); set { } } public override string ToString() => "<Application.platform>"; }

}