using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.JNode.Samples
{



    [Serializable]
    public class Earth : JNodeCollection<Continent>
    {

        [SerializeField] private string      _name;
        [SerializeField] private float       _radius;
        [SerializeField] private Continent[] _continents = Array.Empty<Continent>();

        public          string      Name   => _name;
        public          float       Radius => _radius;
        public override Continent[] Items  => _continents;
        
        internal override void           OnValidate() { }

        public override bool AutoUpdateJToken() => true;

        internal override async UniTask Initialize() => Debug.Log("Earth initialized (parsed for the first time).");

        internal override async UniTask Refresh() => Debug.Log("Earth refreshed (parsed again).");


        protected override async UniTask ItemAdded(Continent item)
        {
            Debug.Log($"[Earth] New continent added [{item.Name}]");
            
            await item.FromRemoteConfig(rootKey: item.Name.ToLower(),
                                        fallback: string.Empty);
        }


        protected override UniTask ItemRemoved(Continent item) => default;

    }


    [Serializable]
    public class Continent : JNodeCollection<Country>,
                             IEquatable<Continent>
    {

        [SerializeField] private string    _name;
        [SerializeField] private float     _population;
        [SerializeField] private float     _surfaceAreaKm;
        [SerializeField] private Country[] _countries => Array.Empty<Country>();

        public          string    Name          => _name;
        public          float     Population    => _population;
        public          float     SurfaceAreaKm => _surfaceAreaKm;
        public override Country[] Items         => _countries;

        public override string ToString() => _name;

        internal override void OnValidate() { }

        public override bool AutoUpdateJToken() => true;

        internal override UniTask Initialize() => default;

        internal override UniTask Refresh() => default;

        protected override async UniTask ItemAdded(Country item) => Debug.Log($"[{Name}] Country added [{item.Name}]");
        
        protected override async UniTask ItemRemoved(Country item) => Debug.Log($"[{Name}] Country removed [{item.Name}]");


        public bool Equals(Continent other)
        {
            if (ReferenceEquals(objA: null,
                                objB: other)) return false;

            if (ReferenceEquals(objA: this,
                                objB: other)) return true;

            return _name == other._name;
        }
        
        public override bool Equals(object obj)
        {
	        if (ReferenceEquals(objA: null,
	                            objB: obj)) return false;

	        if (ReferenceEquals(objA: this,
	                            objB: obj)) return true;

	        return obj.GetType() == GetType() && Equals((Continent) obj);
        }

        public override int GetHashCode() => _name != null ?
	                                             _name.GetHashCode() :
	                                             0;

    }


    [Serializable]
    public class Country : JNode,
                           IEquatable<Country>
    {

        [SerializeField] private string _name;
        [SerializeField] private float  _population;
        [SerializeField] private float  _surfaceAreaKm;

        public string Name          => _name;
        public float  Population    => _population;
        public float  SurfaceAreaKm => _surfaceAreaKm;

        internal override void OnValidate() { }

        public override bool AutoUpdateJToken() => true;

        internal override UniTask Initialize() => default;

        internal override UniTask Refresh() => default;

        internal override UniTask Deinitialize() => default;

        public override string ToString() => _name;


        public bool Equals(Country other)
        {
            if (ReferenceEquals(objA: null,
                                objB: other)) return false;

            if (ReferenceEquals(objA: this,
                                objB: other)) return true;

            return _name == other._name;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(objA: null,
                                objB: obj)) return false;

            if (ReferenceEquals(objA: this,
                                objB: obj)) return true;

            return obj.GetType() == GetType() && Equals((Country) obj);
        }


        public override int GetHashCode() => (_name != null ?
                                                  _name.GetHashCode() :
                                                  0);

    }

    [Serializable]
    public class SampleApp : MonoBehaviour
    {

        [SerializeField]
        public Earth earth;

        
        
        public int    adhocIndex = 0;
        public string adhocKey   = "_name";


        [ContextMenu("Parse Earth")]
        public async void ParseA() => await earth.FromRemoteConfig(rootKey: "earth",
                                                                       fallback: string.Empty);


        [ContextMenu("Parse Continents")]
        public async void ParseB() => await earth.FromRemoteConfig(rootKey: "earthContinents",
                                                                       fallback: string.Empty);


        void Start()
        {
            earth.OnItemAdded += item => Debug.Log($"The continent of [{item.Name}] has been discovered!");

            earth.OnItemRemoved += item => Debug.Log($"The continent of [{item.Name}] has completely disappeared.");

            ParseA();
        }

    }

}