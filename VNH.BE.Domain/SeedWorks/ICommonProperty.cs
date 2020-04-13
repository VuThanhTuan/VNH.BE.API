using System;

namespace VNH.BE.Domain.SeedWorks
{
    public interface ICommonProperty
    {
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
