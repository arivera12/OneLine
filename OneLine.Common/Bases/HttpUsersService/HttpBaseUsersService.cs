using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpBaseUsersService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs> :
        HttpBaseCrudExtendedService<T, TIdentifier, TId, TBlobData, TBlobValidator, TUserBlobs>,
        IHttpUsersService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
        where TBlobData : class, IBlobData
        where TBlobValidator : class, IValidator, new()
        where TUserBlobs : class, IUserBlobs
    {
        public override string ControllerName { get; set; } = "usersaccount";
        public virtual string LockUnlockMethod { get; set; } = "lockunlock";
        public virtual string ConfirmEmailMethod { get; set; } = "confirmemail";
        public virtual string ForgotPasswordMethod { get; set; } = "forgotpassword";
        public virtual string LoginMethod { get; set; } = "login";
        public virtual string RegisterMethod { get; set; } = "register";
        public virtual string RegisterInternalMethod { get; set; } = "registerinternal";
        public virtual string ResetPasswordMethod { get; set; } = "resetpassword";
        public virtual string ResetPasswordInternalMethod { get; set; } = "resetpasswordinternal";
        public virtual string SetupMethod { get; set; } = "setup";
        public HttpBaseUsersService() : base()
        {
        }
        public HttpBaseUsersService(HttpClient httpClient) : base(httpClient)
        {
        }
        public HttpBaseUsersService(Uri baseAddress) : base(baseAddress)
        {
        }
        public HttpBaseUsersService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
        }
        public HttpBaseUsersService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> LockUnlock<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"/{ControllerName}/{LockUnlockMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> LockUnlock(ILockUnlock record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, ILockUnlock>(HttpMethod.Put, $"/{ControllerName}/{LockUnlockMethod}", record, new LockUnlockValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> ConfirmEmail<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Get, $"/{ControllerName}/{ConfirmEmailMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> ConfirmEmail(IConfirmEmail record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IConfirmEmail>(HttpMethod.Get, $"/{ControllerName}/{ConfirmEmailMethod}", record, new ConfirmEmailValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> ForgotPassword<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{ForgotPasswordMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> ForgotPassword(IForgotPassword record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IForgotPassword>(HttpMethod.Post, $"/{ControllerName}/{ForgotPasswordMethod}", record, new ForgotPasswordValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Login<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{LoginMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<IAspNetUsers>>> Login(ILogin record)
        {
            return await HttpClient.SendJsonResponseResultAsync<IAspNetUsers, ILogin>(HttpMethod.Post, $"/{ControllerName}/{LoginMethod}", record, new LoginValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> Register<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{RegisterMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> Register(IRegister record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IRegister>(HttpMethod.Post, $"/{ControllerName}/{RegisterMethod}", record, new RegisterValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> RegisterInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{RegisterInternalMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> RegisterInternal(IRegisterInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IRegisterInternal>(HttpMethod.Post, $"/{ControllerName}/{RegisterInternalMethod}", record, new RegisterInternalValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> ResetPassword<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> ResetPassword(IResetPassword record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetPassword>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordMethod}", record, new ResetPasswordValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<TResponse>>> ResetPasswordInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordInternalMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> ResetPasswordInternal(IResetPasswordInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetPasswordInternal>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordInternalMethod}", record, new ResetPasswordInternalValidator());
        }
        public virtual async Task<IResponseResult<IApiResponse<string>>> Setup()
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<string>>($"/{ControllerName}/{SetupMethod}");
        }
    }
}
