//using System;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//using UnityEngine.UI;
//
//using Vanilla.DeltaValues;
//using Vanilla.Pools;
//
//namespace PHORIA.Studios.Showcase
//{
//
//    [Serializable]
//    public class UI_Entry_Search_Result : MonoBehaviour, 
//                                          IPoolItem
//    {
//
////        public static DeltaClass<UI_Entry_Search_Result> Selected = new(name: "Selected Search Result",
////                                                                        defaultValue: null);
//
//        [NonSerialized]
//        public SearchResult Result;
//
//        [SerializeField]
//        public TMP_Text Name;
//        [SerializeField]
//        public TMP_Text User;
//        [SerializeField]
//        public TMP_Text UploadDate;
//        [SerializeField]
//        public RawImage Thumbnail;
//        [SerializeField]
//        public TMP_Text Description;
////        [NonSerialized]
////        public string[] bundles;
//
//
////        void Start() => GetComponent<Button>().onClick.AddListener(HandleClick);
////
////        private void HandleClick() => Selected.Value = this;
////
////
////        void OnDestroy() => GetComponent<Button>().onClick.RemoveListener(HandleClick);
//
//
//        public void Populate(SearchResult Result)
//        {
//            this.Result = Result;
//            
//            Name.text         = Result.name;
//            User.text         = Result.user;
//            UploadDate.text   = DateTime.FromBinary(Result.timestamp).ToLongDateString();
//            Thumbnail.texture = Result.thumbnailImage;
//            Description.text  = Result.description;
//        }
//
//
//        public UniTask OnGet() => default;
//
//
//        public UniTask OnRetire() => default;
//
//
//        public UniTask RetireSelf() => default;
//
//        [SerializeField]
//        private bool _LeasedFromPool = false;
//        public bool LeasedFromPool
//        {
//            get => _LeasedFromPool;
//            set => _LeasedFromPool = value;
//        }
//
//        [NonSerialized]
//        private IPool<IPoolItem> _pool;
//        public IPool<IPoolItem> Pool
//        {
//            get => _pool;
//            set => _pool = value;
//        }
//
//    }
//
//}