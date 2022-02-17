using System;

using UnityEngine;

namespace Vanilla.Catalogue.Samples.Editor
{

    [Serializable]
    public class SampleCatalogueEditor : CatalogueEditor<SampleCatalogue, SampleCatalogueItem>
    {

        void Awake()
        {
            Debug.Log("Hi there, I'm a Catalogue Editor!");
            Debug.Log("I can be used to easily edit, import and export your catalogue json data.");
            Debug.Log("Simply make a class that inherits from CatalogueEditor with your own custom Catalogue and CatalogueItem class type as generic parameters.");
            Debug.Log("You can even use me to directly run a CatalogueBuilder fetch, invoking all your menu generation callbacks.");
            Debug.Log("Try right-clicking this component, or simply observe...!");

            ExportAsFetchForContextMenu();
        }

        [ContextMenu(itemName: "Export as fetch")]
        protected override void ExportAsFetchForContextMenu() => ExportAsFetch();

    }

}