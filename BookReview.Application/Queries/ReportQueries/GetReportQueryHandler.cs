using BookReview.Core.Services;
using MediatR;

namespace BookReview.Application.Queries.ReportQueries
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, byte[]>
    {
        private readonly IReportService _reportService;

        public GetReportQueryHandler(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<byte[]> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            return await _reportService.GenerateRatedBooksReport();
        }
    }
}
