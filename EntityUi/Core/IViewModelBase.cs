using System;
namespace EntityUi.Core
{
    public interface IViewModelBase
    {
        int Id { get; set; }
        string ModelDescription { get; set; }
        int ParentId { get; set; }
    }
}
