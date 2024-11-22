using RDF.Arcana.API.Common.Pagination;

namespace RDF.Arcana.API.Features.Get_Reports
{
    public class GetCheckInReportsFilo
    {
        public class GetCheckInFiloQuery : UserParams, IRequest<PagedList<GetCheckInFiloResult>>
        {
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int? ClusterId { get; set; }
        }

        public class GetCheckInFiloResult
        {
            public string CdoName { get; set; }
            public DateTime FirstIn { get; set; }
            public DateTime LastOut { get; set; }
            public string Duration { get; set; }
        }

        public class Handler : IRequestHandler<GetCheckInFiloQuery, PagedList<GetCheckInFiloResult>>
        {
            public Task<PagedList<GetCheckInFiloResult>> Handle(GetCheckInFiloQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
