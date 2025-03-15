using BookReview.Application.Models;
using MediatR;

namespace BookReview.Application.Queries.ReportQueries
{
    public class GetReportQuery : IRequest<byte[]>
    {
    }
}
