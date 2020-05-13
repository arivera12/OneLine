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
        Task<ResponseResult<ApiResponse<TResponse>>> LockUnlock<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> LockUnlock(ILockUnlock record);
        Task<ResponseResult<ApiResponse<TResponse>>> ConfirmEmail<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> ConfirmEmail(IConfirmEmail record);
        Task<ResponseResult<ApiResponse<TResponse>>> ForgotPassword<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> ForgotPassword(IForgotPassword record);
        Task<ResponseResult<ApiResponse<TResponse>>> Login<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<AspNetUsersViewModel>>> Login(ILogin record);
        Task<ResponseResult<ApiResponse<TResponse>>> Register<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> Register(IRegister record);
        Task<ResponseResult<ApiResponse<TResponse>>> RegisterInternal<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> RegisterInternal(IRegisterInternal record);
        Task<ResponseResult<ApiResponse<TResponse>>> ResetPassword<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> ResetPassword(IResetPassword record);
        Task<ResponseResult<ApiResponse<TResponse>>> ResetPasswordInternal<TResponse, TModel>(TModel record, IValidator validator);
        Task<ResponseResult<ApiResponse<string>>> ResetPasswordInternal(IResetPasswordInternal record);
        Task<ResponseResult<ApiResponse<string>>> Setup();
 
    }
}
