using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

namespace Vanilla.MenuMachine
{

    public interface ICatalogue<I>
        where I : ICatalogueItem
    {

        bool Initialized
        {
            get;
            set;
        }

        string DefaultRemoteConfig
        {
            get;
        }
        
        UniTask Initialize();

        UniTask Update();

        I[] Items
        {
            get;
            set;
        }
        
    }

}