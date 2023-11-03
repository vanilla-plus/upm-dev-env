//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//
//using Vanilla.DeltaValues;
//using Vanilla.Pools;
//
//namespace PHORIA.Studios.Showcase
//{
//    public class UI_Window_Search_Results : UI_Window
//    {
//        
//        public static UI_Window_Search_Results i;
//
//        public RectTransform resultsContainer;
//
//        public float resultUIWidth = 100.0f;
//
//        public static DeltaClass<GameObject> SelectedGameObject = new(name: "Selected GameObject",
//                                                                      defaultValue: null);
//
//        public static DeltaClass<UI_Entry_Search_Result> SelectedResult = new(name: "Selected Search Result",
//                                                                              defaultValue: null);
//        
//        public Button startExperienceButton;
//        
//        [SerializeField]
//        public DirectPrefabPool<UI_Entry_Search_Result> pool = new DirectPrefabPool<UI_Entry_Search_Result>();
//
//        protected override void OnValidate()
//        {
//            #if UNITY_EDITOR
//            base.OnValidate();
//
//            resultUIWidth = ((RectTransform) pool.Prefab.transform).sizeDelta.x;
//            #endif
//        }
//
//
//        protected override void Awake()
//        {
//            base.Awake();
//
//            i = this;
//
//            pool.CreateAll();
//            
//            startExperienceButton.onClick.AddListener(HandleStartExperienceButton);
//            
//            SelectedGameObject.OnValueChanged += HandleSelectedGameObjectChange;
//            SelectedResult.OnValueChanged     += HandleSelectedResultChange;
//        }
//
//
//
//
//        private void HandleSelectedGameObjectChange(GameObject outgoing,
//                                                    GameObject incoming)
//        {
//            if (incoming == null)
//            {
//                SelectedResult.Value = null;
//
//                return;
//            }
//
//            // LOL clicking the button counted as a selection and was preventing the button from being clicked... woops.
//            if (incoming == startExperienceButton.gameObject) return;
//
//            SelectedResult.Value = incoming.GetComponent<UI_Entry_Search_Result>();
//        }
//
//
//        private void HandleSelectedResultChange(UI_Entry_Search_Result outgoing,
//                                                UI_Entry_Search_Result incoming) => startExperienceButton.gameObject.SetActive(incoming != null);
//
//
//        private void HandleStartExperienceButton()
//        {
//            startExperienceButton.enabled = false;
//            
//            Active.Active.Value = false;
//
//            ShowcaseManager.i.BeginExperience(SelectedResult.Value.Result).Forget();
//        }
//
//
//        void Update() => SelectedGameObject.Value = EventSystem.current.currentSelectedGameObject;
//
//
//        protected override void OnDestroy()
//        {
//            base.OnDestroy();
//
//            i = null;
//            
//            startExperienceButton.onClick.RemoveListener(HandleStartExperienceButton);
//            
//            SelectedGameObject.OnValueChanged -= HandleSelectedGameObjectChange;
//            SelectedResult.OnValueChanged     -= HandleSelectedResultChange;
//        }
//
//
//        public async UniTask DisplayResults()
//        {
//            await pool.RetireAll();
//            
//            foreach (var result in Search.Search.Results.Entries)
//            {
//                var resultUI = await pool.Get();
//                
//                resultUI.Populate(result);
//                
//                resultUI.gameObject.SetActive(true);
//            }
//
//            resultsContainer.sizeDelta = new Vector2(x: pool.Active.Count * resultUIWidth,
//                                                     y: resultsContainer.sizeDelta.y);
//            
//            Active.Active.Value = true;
//        }
//        
//    }
//}
