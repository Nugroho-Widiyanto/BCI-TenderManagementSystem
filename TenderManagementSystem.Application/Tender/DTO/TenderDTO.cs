namespace TenderManagementSystem.Application.Tender.DTO
{
    using AutoMapper;
    using Common.Mappings;
    using System;

    public class TenderDTO : IMapFrom<Domain.Entities.Tender>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Tender, TenderDTO>();
            profile.CreateMap<TenderDTO, Domain.Entities.Tender>();
        }
    }
}