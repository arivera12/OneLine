using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace OneLine.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the claim and converts them into <typeparamref name="T"/> by the specified claim type.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static T GetClaim<T>(this ClaimsPrincipal principal, string claimType)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return (T)Convert.ChangeType(principal.FindFirst(claimType)?.Value, typeof(T));
        }
        /// <summary>
        /// Gets the claims and converts them into <typeparamref name="T"/> by the specified claim type.
        /// This method expect that the claim value is a valid json and the specified <typeparamref name="T"/> is compatible with deserialization. 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetClaims<T>(this ClaimsPrincipal principal, string claimType)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            var claims = principal.FindAll(claimType);
            if (claims.IsNull() || !claims.Any())
                return null;
            IList<T> dynamicObjectClaim = new List<T>();
            foreach (var claim in claims)
            {
                dynamicObjectClaim.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(claim.Value));
            }
            return dynamicObjectClaim;
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
        /// <summary>
        /// Gets the short message service gateway by <see cref="Constants.ClaimTypes.SMSGateway"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string ShortMessageServiceGateway(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(Constants.ClaimTypes.SMSGateway)?.Value;
        }
        /// <summary>
        /// Gets the multimedia messaging service gateway by <see cref="Constants.ClaimTypes.MMSGateway"/>
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string MultimediaMessagingServiceGateway(this ClaimsPrincipal principal)
        {
            if (principal.IsNull())
                throw new ArgumentNullException(nameof(principal));
            return principal.FindFirst(Constants.ClaimTypes.MMSGateway)?.Value;
        }
    }
}

