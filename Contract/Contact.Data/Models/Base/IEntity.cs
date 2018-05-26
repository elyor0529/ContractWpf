using System;

namespace Contract.Data.Models.Base
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime? CreatedDate { get; set; }

        DateTime? ModefiedDate { get; set; }

        bool? IsDeleted { get; set; }
    }
}