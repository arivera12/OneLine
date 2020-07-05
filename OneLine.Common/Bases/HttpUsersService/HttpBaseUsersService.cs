using FluentValidation;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Models.Users;
using OneLine.Validations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public class HttpBaseUsersService<T, TIdentifier, TId> :
        HttpBaseCrudExtendedService<T, TIdentifier, TId>,
        IHttpUsersService<T, TIdentifier>
        where T : class, new()
        where TIdentifier : class, IIdentifier<TId>
    {
        public override string Api { get; set; } = "api";
        public override string ControllerName { get; set; } = "usersaccount";
        public virtual string LockUnlockMethod { get; set; } = "lockunlock";
        public virtual string ConfirmEmailMethod { get; set; } = "confirmemail";
        public virtual string ForgotPasswordMethod { get; set; } = "forgotpassword";
        public virtual string LoginMethod { get; set; } = "login";
        public virtual string RegisterMethod { get; set; } = "register";
        public virtual string RegisterInternalMethod { get; set; } = "registerinternal";
        public virtual string UpdateInternalMethod { get; set; } = "updateinternal";
        public virtual string ResetPasswordMethod { get; set; } = "resetpassword";
        public virtual string ResetPasswordInternalMethod { get; set; } = "resetpasswordinternal";
        public virtual string ResetOwnPasswordInternalMethod { get; set; } = "resetownpasswordinternal";
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
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> LockUnlock<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{LockUnlockMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> LockUnlock(TIdentifier identifier)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, TIdentifier>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{LockUnlockMethod}", identifier, new IdentifierStringValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> ConfirmEmail<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{ConfirmEmailMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> ConfirmEmail(IConfirmEmail record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IConfirmEmail>(HttpMethod.Get, $"{GetApi()}/{ControllerName}/{ConfirmEmailMethod}", record, new ConfirmEmailValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> ForgotPassword<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{ForgotPasswordMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> ForgotPassword(IForgotPassword record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IForgotPassword>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{ForgotPasswordMethod}", record, new ForgotPasswordValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Login<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{LoginMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<AspNetUsersViewModel>>> Login(ILogin record)
        {
            return await HttpClient.SendJsonResponseResultAsync<AspNetUsersViewModel, ILogin>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{LoginMethod}", record, new LoginValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> Register<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{RegisterMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> Register(IRegister record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IRegister>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{RegisterMethod}", record, new RegisterValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> RegisterInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{RegisterInternalMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> RegisterInternal(IRegisterInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IRegisterInternal>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{RegisterInternalMethod}", record, new RegisterInternalValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> UpdateInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{UpdateInternalMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> UpdateInternal(IUpdateInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IUpdateInternal>(HttpMethod.Post, $"{GetApi()}/{ControllerName}/{UpdateInternalMethod}", record, new UpdateInternalValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> ResetPassword<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{ResetPasswordMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> ResetPassword(IResetPassword record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetPassword>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{ResetPasswordMethod}", record, new ResetPasswordValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> ResetOwnPasswordInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{ResetOwnPasswordInternalMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> ResetOwnPasswordInternal(IResetOwnPasswordInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetOwnPasswordInternal>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{ResetOwnPasswordInternalMethod}", record, new ResetOwnPasswordInternalValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<TResponse>>> ResetPasswordInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{ResetPasswordInternalMethod}", record, validator);
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> ResetPasswordInternal(IResetPasswordInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetPasswordInternal>(HttpMethod.Put, $"{GetApi()}/{ControllerName}/{ResetPasswordInternalMethod}", record, new ResetOwnPasswordInternalValidator());
        }
        public virtual async Task<IResponseResult<ApiResponse<string>>> Setup()
        {
            return await HttpClient.GetJsonResponseResultAsync<ApiResponse<string>>($"{GetApi()}/{ControllerName}/{SetupMethod}");
        }
    }
}
