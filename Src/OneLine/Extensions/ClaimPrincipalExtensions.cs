using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OneLine.Extensions
{
    public static class ClaimPrincipalExtensions
    {
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
        /// <summary>
        /// Gets the current user access token by <see cref="Constants.ClaimTypes.AccessToken"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string AccessToken(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(Constants.ClaimTypes.AccessToken)?.Value;
        }
        /// <summary>
        /// Gets the current user preferred culture locale by <see cref="Constants.ClaimTypes.PreferredCultureLocale"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string PreferredCultureLocale(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(Constants.ClaimTypes.PreferredCultureLocale)?.Value;
        }
    }
}

