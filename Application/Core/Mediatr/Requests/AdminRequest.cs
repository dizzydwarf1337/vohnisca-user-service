
namespace Application.Core.Mediatr.Requests;

public class AdminRequest<T> : AuthorizedRequest<T>, IAdminRequest;

public interface IAdminRequest;