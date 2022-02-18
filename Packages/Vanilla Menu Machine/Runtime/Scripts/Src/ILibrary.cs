using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.Catalogue;
using Vanilla.Layout;
using Vanilla.Pools;

namespace Vanilla.MediaLibrary
{

	// public interface ILibrary<C, CI, LI, L, LOI, P, T, S>
	// 	where C : ICatalogue<CI>
	// 	where CI : ICatalogueItem
	// 	where LI : ILibraryItem<CI>, ILayoutItem, IPoolItem
	// 	where L : ILayout<LOI, T, S>
	// 	where LOI : ILayoutItem
	// 	where P : IPool<LI>
	// 	where T : Transform
	// 	where S : struct
	// {

	// 	C Catalogue
	// 	{
	// 		get;
	// 		set;
	// 	}

	// 	L Layout
	// 	{
	// 		get;
	// 	}

	// 	P Pool
	// 	{
	// 		get;
	// 	}

	// }

	public interface ILibrary<C, CI, LI>
		where C : ICatalogue<CI>
		where CI : ICatalogueItem
		where LI : ILibraryItem<CI>
	{

		C Catalogue
		{
			get;
			set;
		}

	}


	public interface IPooledLibrary<C, CI, LI, P, T, S>
		where C : ICatalogue<CI>
		where CI : ICatalogueItem
		where LI : ILibraryItem<CI>, IPoolItem
		where P : IPool<LI>
		where T : Transform
		where S : struct
	{

		C Catalogue
		{
			get;
			set;
		}

		P Pool
		{
			get;
		}

	}

		public interface IArrangedLibrary<C, CI, LI, L, LOI, T, S>
		where C : ICatalogue<CI>
		where CI : ICatalogueItem
		where LI : ILibraryItem<CI>, ILayoutItem
		where L : ILayout<LOI, T, S>
		where LOI : ILayoutItem
		where T : Transform
		where S : struct
	{

		C Catalogue
		{
			get;
			set;
		}

		L Layout
		{
			get;
		}

	}


}