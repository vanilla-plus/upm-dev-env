using System;

using Cysharp.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Unity.RemoteConfig;

using UnityEngine;

namespace Vanilla.JNode
{



    [Serializable]
    public class TestCatalogue : JNodeCollection<TestLocation>
    {

        internal override void OnValidate() { }

        public override bool AutoUpdateJToken() => true;

        internal override UniTask AddedToCollection() => default;


//        public override async UniTask ItemAdded(TestLocation item) => await item.FromRemoteConfig(rootKey: item._name.ToLower(),
//                                                                                                  fallback: string.Empty);

        public override UniTask ItemAdded(TestLocation item) => default;

        public override UniTask ItemRemoved(TestLocation item) => default;

    }
    
        
    [Serializable]
    public class TestLocation : JNodeCollection<TestEpisode>
    {

        public string  _name;
        public bool    _available;
        public int     _index;
        public float[] _borderColor;
        public float[] _latLong;

        internal override void OnValidate() { }

        public override bool AutoUpdateJToken() => true;

        internal override UniTask AddedToCollection() => default;


        public override UniTask ItemAdded(TestEpisode item) => default;


        public override UniTask ItemRemoved(TestEpisode item) => default;

        public override string ToString() => _name;

    }
    
    
    [Serializable]
    public class TestEpisode : JNode
    {

        public string _name;
        public bool   _available;
        public int    duration;
        public string _description;
        public string _thumbnailName;
        public string _videoPath;
        public int    _index;

        internal override void OnValidate() { }

        public override bool AutoUpdateJToken() => true;

        internal override UniTask AddedToCollection() => default;


        internal override UniTask RemovedFromCollection() => default;

        public override string ToString() => _name;

    }

    [Serializable]
    public class Dumb : MonoBehaviour
    {

        [SerializeField]
        public TestCatalogue catalogue;

        public int    adhocIndex = 0;
        public string adhocKey      = "_index";


        [ContextMenu("Parse A")]
        public async void ParseA() => await catalogue.FromRemoteConfig(rootKey: "catalogueA",
                                                                       fallback: string.Empty);


        [ContextMenu("Parse B")]
        public async void ParseB() => await catalogue.FromRemoteConfig(rootKey: "catalogueB",
                                                                       fallback: string.Empty);


        void Start()
        {
            catalogue.OnItemAdded += i => Debug.LogWarning("Oh a new item eh? " +i);
            
            catalogue.OnItemRemoved += i => Debug.LogWarning($"You're throwing [{i}] out? " +i);
            
//            ParseA();

        }


        [ContextMenu("Get Adhoc")]

//        public void GetAdhoc()
//        {
//            
//            Debug.Log("[Data] [Catalogue] "+catalogue.Data);
//
//            foreach (var location in catalogue.Items)
//            {
//                Debug.Log("[Data] [Location] " +location.Data);
//
//                foreach (var episode in location.Items)
//                {
//                    Debug.Log("[Data] [Episode] " +episode.Data);
//                    
//                    Debug.LogError(episode.Data == null);
//                }
//            }
//        }


        public void GetAdhoc() => Debug.Log(catalogue.Items[adhocIndex].Data[adhocKey]);
        
        
        // ToDo
        // Hm, looks like parsing just the items array by itself isn't possible.
        // According to a rather specific error based on the below:
        // "JSON must represent an object."
        // However...
        // Parsing a Collection with the only token being "_items" might work.
        // It only seems to update found or matching properties.
        // So you could try to parse a Collection as an 'object' that only contains the items array.
        // That should work and shouldn't require parsing anything else a second time.
        
        // The whole point of doing this is to prevent double-parsing which currently occurs.
        // Think about it, you parse Catalogue which contains the Locations array.
        // And then for each Location, it parses itself (again) including the Episodes array.
        
        // If we can distinguish between parsing a JNode and parsing its Children, we're in a good spot!
        
        [ContextMenu("Parse just the items array")]
        public async void ParseJustTheItemsArray()
        {
            await JNode.FetchRemCon();

            Debug.Log(ConfigManager.appConfig.GetJson("catalogueA"));

            var catalogueObject = JObject.Parse(ConfigManager.appConfig.GetJson("catalogueA"));

            Debug.Log(catalogueObject["_items"].ToString());
            
//            catalogue.FromJsonButItsJustTheItemArrayPlz(catalogueObject["_items"].ToString());

            var something = (TestLocation[]) JsonUtility.FromJson(json: catalogueObject["_items"].ToString(),
                                                                  type: typeof(TestLocation[]));

            foreach (var i in something)
            {
                Debug.Log(i);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetAdhoc();
            }
        }

//        public Blah blah;

//        [ContextMenu("Dumb")]
//        public async void DumbStuff()
//        {

//            ConfigManager.FetchConfigs(new Dumb2.userAttributes(), new Dumb2.appAttributes());

//            while (ConfigManager.requestStatus == ConfigRequestStatus.Pending) await UniTask.Yield();

//            var json =ConfigManager.appConfig.GetJson("dumb",
//                                            string.Empty);



//            JsonUtility.FromJsonOverwrite(json, blah);

//            Debug.Log(blah.stuff.Length);
//            Debug.Log(blah.stuff.Count);
//        }

    }

//    [Serializable]
//    public class Blah
//    {
//
//        public BlahBlah[] stuff;      // Yep
////        public List<BlahBlah> stuff;  // Yep
////        public Stack<BlahBlah> stuff; // Nope
////        public Queue<BlahBlah>     stuff; // Nope
//        
//        
//    }
//
//    [Serializable]
//    public class BlahBlah
//    {
//
//        public string name;
//        public int    age;
//
//    }

}
