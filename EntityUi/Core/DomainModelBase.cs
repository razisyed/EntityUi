using System;

namespace EntityUi.Core
{
    /// <summary>
    /// Base for Domain Model
    /// </summary>
    [Serializable]
    public abstract class DomainModelBase : EntityUi.Core.IDomainModelBase
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
       
    }
}
