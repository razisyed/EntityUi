using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityUi.Attributes
{
    public class HideInGrid : Attribute, IMetadataAware
    {
        public HideInGrid()
        {            
        }
        
        public void OnMetadataCreated(ModelMetadata metadata)
        {            
        }
    }
}
