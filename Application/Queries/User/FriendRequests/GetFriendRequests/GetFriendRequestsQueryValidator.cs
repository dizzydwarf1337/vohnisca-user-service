using FluentValidation;

namespace Application.Queries.User.FriendRequests.GetFriendRequests;

public class GetFriendRequestsQueryValidator : AbstractValidator<GetFriendRequestsQuery>
{
    public GetFriendRequestsQueryValidator()
    {
        RuleFor(x => x.Pagination).NotEmpty().WithName("Pagination specification");
        RuleFor(x => x.Pagination.Page).NotEmpty().GreaterThan(0).WithName("Page");
        RuleFor(x => x.Pagination.PageSize).NotEmpty().GreaterThan(0).WithName("Page size");
    }
}