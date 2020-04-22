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
        where T : new()
        where TIdentifier : IIdentifier<TId>
        where TId : class
        where TBlobData : IBlobData
        where TBlobValidator : IValidator, new()
        where TUserBlobs : IUserBlobs
    {
        public virtual string LockUnlockMethod { get; set; } = "lockunlock";
        public virtual string ConfirmEmailMethod { get; set; } = "confirmemail";
        public virtual string ForgotPasswordMethod { get; set; } = "forgotpassword";
        public virtual string LoginMethod { get; set; } = "login";
        public virtual string RegisterMethod { get; set; } = "register";
        public virtual string RegisterInternalMethod { get; set; } = "registerinternal";
        public virtual string ResetPasswordMethod { get; set; } = "resetpassword";
        public virtual string ResetPasswordInternalMethod { get; set; } = "resetpasswordinternal";
        public virtual string SetupMethod { get; set; } = "setup";
        public HttpBaseUsersService()
        {
            if (!string.IsNullOrWhiteSpace(BaseAddress))
            {
                HttpClient = new HttpClient
                {
                    BaseAddress = new Uri(BaseAddress)
                };
            }
            else
            {
                HttpClient = new HttpClient();
            }
        }
        public HttpBaseUsersService(HttpClient httpClient) : base(httpClient)
        {
            HttpClient = httpClient;
        }
        public HttpBaseUsersService(Uri baseAddress) : base(baseAddress)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
        }
        public HttpBaseUsersService(string AuthorizationToken, bool AddBearerScheme = true) : base(AuthorizationToken, AddBearerScheme)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public HttpBaseUsersService(Uri baseAddress, string AuthorizationToken, bool AddBearerScheme = true) : base(baseAddress, AuthorizationToken, AddBearerScheme)
        {
            HttpClient = new HttpClient
            {
                BaseAddress = baseAddress
            };
            if (!string.IsNullOrWhiteSpace(AuthorizationToken))
            {
                HttpClient.DefaultRequestHeaders.Remove("Authorization");
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{(AddBearerScheme ? "Bearer" : null)} {AuthorizationToken}");
            }
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> LockUnlock<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"/{ControllerName}/{LockUnlockMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> LockUnlock(ILockUnlock record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, ILockUnlock>(HttpMethod.Put, $"/{ControllerName}/{LockUnlockMethod}", record, new LockUnlockValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> ConfirmEmail<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Get, $"/{ControllerName}/{ConfirmEmailMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> ConfirmEmail(IConfirmEmail record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IConfirmEmail>(HttpMethod.Get, $"/{ControllerName}/{ConfirmEmailMethod}", record, new ConfirmEmailValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> ForgotPassword<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{ForgotPasswordMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> ForgotPassword(IForgotPassword record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IForgotPassword>(HttpMethod.Post, $"/{ControllerName}/{ForgotPasswordMethod}", record, new ForgotPasswordValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> Login<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{LoginMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<IAspNetUsers>>> Login(ILogin record)
        {
            return await HttpClient.SendJsonResponseResultAsync<IAspNetUsers, ILogin>(HttpMethod.Post, $"/{ControllerName}/{LoginMethod}", record, new LoginValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> Register<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{RegisterMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> Register(IRegister record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IRegister>(HttpMethod.Post, $"/{ControllerName}/{RegisterMethod}", record, new RegisterValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> RegisterInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Post, $"/{ControllerName}/{RegisterInternalMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> RegisterInternal(IRegisterInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IRegisterInternal>(HttpMethod.Post, $"/{ControllerName}/{RegisterInternalMethod}", record, new RegisterInternalValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> ResetPassword<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> ResetPassword(IResetPassword record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetPassword>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordMethod}", record, new ResetPasswordValidator());
        }
        public async Task<IResponseResult<IApiResponse<TResponse>>> ResetPasswordInternal<TResponse, TModel>(TModel record, IValidator validator)
        {
            return await HttpClient.SendJsonResponseResultAsync<TResponse, TModel>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordInternalMethod}", record, validator);
        }
        public async Task<IResponseResult<IApiResponse<string>>> ResetPasswordInternal(IResetPasswordInternal record)
        {
            return await HttpClient.SendJsonResponseResultAsync<string, IResetPasswordInternal>(HttpMethod.Put, $"/{ControllerName}/{ResetPasswordInternalMethod}", record, new ResetPasswordInternalValidator());
        }
        public async Task<IResponseResult<IApiResponse<string>>> Setup()
        {
            return await HttpClient.GetJsonResponseResultAsync<IApiResponse<string>>($"/{ControllerName}/{SetupMethod}");
        }
    }
}
