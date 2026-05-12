using MyRow = SYP.Administration.UserRow;

namespace SYP.Administration.Endpoints;

public class GetImpersonateTokenRequest : ServiceRequest
{
    public int UserId { get; set; }
}

public class GetImpersonateTokenResponse : ServiceResponse
{
    public string Token { get; set; }
}

[Route("Services/Administration/User/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class UserEndpoint : ServiceEndpoint
{
    [HttpPost, AuthorizeCreate(typeof(MyRow))]
    public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request, [FromServices] IUserSaveHandler handler)
    {
        return handler.Create(uow, request);
    }

    [HttpPost, AuthorizeUpdate(typeof(MyRow))]
    public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request, [FromServices] IUserSaveHandler handler)
    {
        return handler.Update(uow, request);
    }

    [HttpPost, AuthorizeDelete(typeof(MyRow))]
    public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request, [FromServices] IUserDeleteHandler handler)
    {
        return handler.Delete(uow, request);
    }

    public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request, [FromServices] IUserRetrieveHandler handler)
    {
        return handler.Retrieve(connection, request);
    }

    public ListResponse<MyRow> List(IDbConnection connection, UserListRequest request, [FromServices] IUserListHandler handler)
    {
        return handler.List(connection, request);
    }

    [HttpPost]
    public GetImpersonateTokenResponse GetImpersonateToken(
        [FromServices] ImpersonateTokenService tokenService,
        GetImpersonateTokenRequest request)
    {
        if (request?.UserId <= 0)
            throw new ArgumentNullException(nameof(request.UserId));

        return new GetImpersonateTokenResponse
        {
            Token = tokenService.GenerateToken(request.UserId)
        };
    }
}