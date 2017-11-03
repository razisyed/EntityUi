using System;
using System.Web.Mvc;

namespace EntityUi.Attributes
{
    public class HelpTip : Attribute, IMetadataAware
    {

        public string HelpTipText { get; set; }

        public HelpTip(string helpTipText)
        {
            HelpTipText = helpTipText;
        }


        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["HelpTipText"] = HelpTipText;
        }
    }
}