using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EntityUi.Extensions;

namespace EntityUi.HtmlHelpers
{
    public class DropDown
    {
        public string SelectedId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

        public string GetSelectedValue()
        {
            var value = "";
            Items.ToList().ForEach(x => { if (x.Value == SelectedId) value = x.Text; });
            return value.ToSeparatedWords();
        }
    }
}