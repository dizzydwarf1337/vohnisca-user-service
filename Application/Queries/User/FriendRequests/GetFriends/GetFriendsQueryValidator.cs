using FluentValidation;

namespace Application.Queries.User.FriendRequests.GetFriends;

public class GetFriendsQueryValidator : AbstractValidator<GetFriendsQuery>
{
    public GetFriendsQueryValidator()
    {
        RuleFor(x => x.Pagination).NotEmpty().WithName("Pagination specification");
        RuleFor(x => x.Pagination.Page).NotEmpty().GreaterThan(0).WithName("Page");
        RuleFor(x => x.Pagination.PageSize).NotEmpty().GreaterThan(0).WithName("Page size");
    }
}