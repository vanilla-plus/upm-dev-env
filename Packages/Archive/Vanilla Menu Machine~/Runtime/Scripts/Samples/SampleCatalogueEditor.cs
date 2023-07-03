using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.Catalogue;

namespace Vanilla.MediaLibrary.Samples
{
    public class SampleCatalogueEditor : CatalogueEditor<SampleCatalogue, SampleCatalogueItem>
    {

        [ContextMenu(itemName: "Export as Fetch")]
        protected override void ExportAsFetchForContextMenu() => ExportAsFetch();

    }
}
