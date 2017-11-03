using System;
using System.Web.Mvc;

namespace EntityUi.Attributes
{
    public class DataBind : Attribute, IMetadataAware
    {

        public string DataBindText { get; set; }

        public DataBind(string databindText)
        {
            DataBindText = databindText;
        }


        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["DataBindText"] = DataBindText;
        }
    }
}
