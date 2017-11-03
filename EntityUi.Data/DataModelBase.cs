using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityUi.Data
{
    public interface IDataModelBase
    {
        int AccountId { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        int Id { get; set; }
        DateTime Modified { get; set; }
        string ModifiedBy { get; set; }
    }

    public abstract class DataModelBase : IDataModelBase
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }

    }
}
