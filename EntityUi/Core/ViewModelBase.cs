using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace EntityUi.Core
{
    public abstract class ViewModelBase : EntityUi.Core.IViewModelBase
    {

        [ReadOnly(true)]
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }

        [ReadOnly(true)]
        [HiddenInput(DisplayValue = false)]
        public int ParentId { get; set; }

        [ReadOnly(true)]
        [HiddenInput(DisplayValue = false)]
        public string ModelDescription { get; set; }
        
    }
}
