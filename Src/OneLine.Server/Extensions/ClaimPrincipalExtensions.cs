using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OneLine.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        /// <summary>
        /// Gets the current user Id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string CurrentUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        /// <summary>
        /// Gets the current user name
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string CurrentUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
        /// <summary>
        /// Gets the current user email
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string CurrentUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }
        /// <summary>
        /// Gets the current user roles
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static IEnumerable<string> CurrentUserRoles(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Role)?.Value.Split(',');
        }

    }
}

