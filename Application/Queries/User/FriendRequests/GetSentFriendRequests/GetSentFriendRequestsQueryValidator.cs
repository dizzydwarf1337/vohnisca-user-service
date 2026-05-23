using FluentValidation;

namespace Application.Queries.User.FriendRequests.GetSentFriendRequests;

public class GetSentFriendRequestsQueryValidator : AbstractValidator<GetSentFriendRequestsQuery>
{
    public GetSentFriendRequestsQueryValidator()
    {
        RuleFor(x => x.Pagination).NotEmpty().WithName("Pagination specification");
        RuleFor(x => x.Pagination.Page).NotEmpty().GreaterThan(0).WithName("Page");
        RuleFor(x => x.Pagination.PageSize).NotEmpty().GreaterThan(0).WithName("Page size");
    }
}