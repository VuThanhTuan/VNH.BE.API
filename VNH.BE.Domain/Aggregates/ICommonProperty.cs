using System;

namespace VNH.BE.Domain.Aggregates
{
    public interface ICommonProperty
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
