using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MediaLibrary
{

    public interface ITileGroupModule
    {

        void Update();

        void LateUpdate();

    }

    public interface ITileGroupModule<I,T> : ITileGroupModule
        where I : Tile<I,T>
        where T : Transform
    {

        HashSet<I> Items
        {
            get;
            set;
        }

        void OnItemAdded(I item);

        void OnItemRemoved(I item);

    }

}