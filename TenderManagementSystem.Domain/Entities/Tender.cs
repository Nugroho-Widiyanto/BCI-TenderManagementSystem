namespace TenderManagementSystem.Domain.Entities
{
    using Dapper.Contrib.Extensions;
    using System;

    public class Tender
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Description { get; set; }
    }
}
