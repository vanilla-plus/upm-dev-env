using System;

using UnityEngine;

namespace Vanilla.Collection
{
    
    public interface JNode
    {

//        bool Parsed
//        {
//            get;
//            set;
//        }
        
        // ToDo these events should go in JNodeCollection!
        // ToDo Think about the seasons example, Seasons.OnNewItem makes sense!
        // ToDo How would anything even subscribe to Winter.OnParse?? It doesn't exist yet!
        
//        Action OnPopulateFail
//        {
//            get;
//            set;
//        }
//
//        Action OnPopulateSuccess
//        {
//            get;
//            set;
//        }

        void OnValidate();

        void FromJson(string json)
        {
            try
            {
                JsonUtility.FromJsonOverwrite(json: json,
                                              objectToOverwrite: this);

//                JsonUtility.FromJson(json: json,
//                                     type: type);
//
//                JsonUtility.FromJson<T>(json: json);

//                if (Parsed)
//                {
//                    OnPopulateSuccess?.Invoke();
//                }
            }
            catch (Exception e)
            {
                Debug.LogError("Does this even happen");
                
                Debug.LogException(e);

//                OnPopulateFail?.Invoke();
            }
            
        }
        
        string ToJson(bool prettyPrint = false) => JsonUtility.ToJson(obj: this, prettyPrint: prettyPrint);

    }
}
