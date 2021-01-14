using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OneLine.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        /// <summary>
        /// Gets the current user Id. This api will be deprecated on future versions. Please use the extension method <see cref="UserId(ClaimsPrincipal)"/> instead.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        [Obsolete("This api will be deprecated on future versions. Please use the extension method UserId instead.")]
        public static string CurrentUserId(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        /// <summary>
        /// Gets the current user name. This api will be deprecated on future versions. Please use the extension method <see cref="UserName(ClaimsPrincipal)"/> instead.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        [Obsolete("This api will be deprecated on future versions. Please use the extension method UserName instead.")]
        public static string CurrentUserName(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
        /// <summary>
        /// Gets the current user email. This api will be deprecated on future versions. Please use the extension method <see cref="Email(ClaimsPrincipal)"/> instead.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        [Obsolete("This api will be deprecated on future versions. Please use the extension method Email instead.")]
        public static string CurrentUserEmail(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }
        /// <summary>
        /// Gets the current user roles. This api will be deprecated on future versions. Please use the extension method <see cref="Roles(ClaimsPrincipal)"/> instead.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        [Obsolete("This api will be deprecated on future versions. Please use the extension method Roles instead.")]
        public static IEnumerable<string> CurrentUserRoles(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindAll(ClaimTypes.Role).Select(s => s.Value);
        }
        /// <summary>
        /// Gets the current user id by <see cref="ClaimTypes.NameIdentifier"/>.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string UserId(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        /// <summary>
        /// Gets the current user name by <see cref="ClaimTypes.Name"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string UserName(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
        /// <summary>
        /// Gets the current user email by <see cref="ClaimTypes.Email"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string Email(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }
        /// <summary>
        /// Gets the current user roles by <see cref="ClaimTypes.Role"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static IEnumerable<string> Roles(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindAll(ClaimTypes.Role).Select(s => s?.Value);
        }
    }
}

