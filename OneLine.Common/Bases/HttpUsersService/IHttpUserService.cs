using FluentValidation;
using OneLine.Models;
using OneLine.Models.Users;
using System.Threading.Tasks;

namespace OneLine.Bases
{
    public interface IHttpUsersService<T, TIdentifier> : IHttpCrudExtendedService<T, TIdentifier>
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
        Task<IResponseResult<ApiResponse<TResponse>>> LockUnlock<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> LockUnlock(ILockUnlock record);
        Task<IResponseResult<ApiResponse<TResponse>>> ConfirmEmail<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> ConfirmEmail(IConfirmEmail record);
        Task<IResponseResult<ApiResponse<TResponse>>> ForgotPassword<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> ForgotPassword(IForgotPassword record);
        Task<IResponseResult<ApiResponse<TResponse>>> Login<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<AspNetUsersViewModel>>> Login(ILogin record);
        Task<IResponseResult<ApiResponse<TResponse>>> Register<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> Register(IRegister record);
        Task<IResponseResult<ApiResponse<TResponse>>> RegisterInternal<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> RegisterInternal(IRegisterInternal record);
        Task<IResponseResult<ApiResponse<TResponse>>> ResetPassword<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> ResetPassword(IResetPassword record);
        Task<IResponseResult<ApiResponse<TResponse>>> ResetPasswordInternal<TResponse, TModel>(TModel record, IValidator validator);
        Task<IResponseResult<ApiResponse<string>>> ResetPasswordInternal(IResetPasswordInternal record);
        Task<IResponseResult<ApiResponse<string>>> Setup();
 
    }
}
