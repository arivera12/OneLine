namespace OneLine.Constants
{
    //
    // Summary:
    //     Defines constants for the well-known claim types that can be assigned to a subject.
    //     This class cannot be inherited.
    public static class ClaimTypes
    {
        //
        // Summary:
        //     The URI for a claim that specifies the access token.
        public const string AccessToken = "http://schemas.microsoft.com/identity/claims/accesstoken";
        //
        // Summary:
        //     The URI for a claim that specifies the prefered culture locale.
        public const string PreferredCultureLocale = "http://schemas.microsoft.com/identity/claims/preferredculturelocale";
    }
}
