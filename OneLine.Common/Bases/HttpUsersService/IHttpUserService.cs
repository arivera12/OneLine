using FluentValidation;
using OneLine.Models;
using OneLine.Models.Users;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpUsersService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs> : IHttpCrudExtendedService<T, TIdentifier, TBlobData, TBlobValidator, TUserBlobs>
    {
        string LockUnlockMethod { get; set; }
        string ConfirmEmailMethod { get; set; }
        string ForgotPasswordMethod { get; set; }
        string LoginMethod { get; set; }
        string RegisterMethod { get; set; }
        string RegisterInternalMethod { get; set; }
        string ResetPasswordMethod { get; set; }
        string ResetPasswordInternalMethod { get; set; }
        string SetupMethod { get; set; }
        Task<IResponseResult<IApiResponse<TResponse>>> LockUnlock<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> LockUnlock(ILockUnlock record);
        Task<IResponseResult<IApiResponse<TResponse>>> ConfirmEmail<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> ConfirmEmail(IConfirmEmail record);
        Task<IResponseResult<IApiResponse<TResponse>>> ForgotPassword<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> ForgotPassword(IForgotPassword record);
        Task<IResponseResult<IApiResponse<TResponse>>> Login<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<AspNetUsersViewModel>>> Login(ILogin record);
        Task<IResponseResult<IApiResponse<TResponse>>> Register<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> Register(IRegister record);
        Task<IResponseResult<IApiResponse<TResponse>>> RegisterInternal<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> RegisterInternal(IRegisterInternal record);
        Task<IResponseResult<IApiResponse<TResponse>>> ResetPassword<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> ResetPassword(IResetPassword record);
        Task<IResponseResult<IApiResponse<TResponse>>> ResetPasswordInternal<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<IApiResponse<string>>> ResetPasswordInternal(IResetPasswordInternal record);
        Task<IResponseResult<IApiResponse<string>>> Setup();
 
    }
}
