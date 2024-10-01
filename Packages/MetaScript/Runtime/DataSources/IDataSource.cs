using System;

namespace Vanilla.MetaScript.DataSources
{

    public interface IGetSetSource<T> : IGettableSource<T>,
                                        ISettableSource<T>
    {

        new T Value
        {
            get;
            set;
        }

    }

    public interface IGettableSource<T>
    {

        T Value
        {
            get;
        }

    }
    
    public interface ISettableSource<T>
    {

        T Value
        {
            get;                
            set;
        }

//        void Set(T value);

    }

    public interface IObservableSource<T>
    {

        Action<T> OnSet
        {
            get;
            set;
        }

        Action<T,T> OnSetWithHistory
        {
            get;
            set;
        }

    }
    
}
