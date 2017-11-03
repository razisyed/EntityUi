using System;
namespace EntityUi.Core
{
    public interface IDomainModelBase
    {
        int AccountId { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        int Id { get; set; }
        DateTime Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}
