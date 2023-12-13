using System;

namespace Vanilla.DataAssets.Sources
{

    public interface IDataSource<T>
    {
//
//        T Get();
//
//        void Set(T newValue);
        
        public T Value
        {
            get;
            set;
        }

    }

    public interface IMonitorableDataSource<T> : IDataSource<T>
    {
        
//        public T Value
//        {
//            get;
//            set;
//        }


        public Action<T> OnValueChanged
        {
            get;
            set;
        }
        
        public Action<T, T> OnValueChangedWithHistory
        {
            get;
            set;
        }


    }

}