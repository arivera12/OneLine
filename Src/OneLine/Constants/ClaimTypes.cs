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
        public const string AccessToken = "http://schemas.oneline.com/identity/claims/accesstoken";
        //
        // Summary:
        //     The URI for a claim that specifies the prefered culture locale.
        public const string PreferredCultureLocale = "http://schemas.oneline.com/identity/claims/preferredculturelocale";
        //
        // Summary:
        //     The URI for a claim that specifies the short message service gateway.
        public const string SMSGateway = "http://schemas.oneline.com/identity/claims/shortmessageservicegateway";
        //
        // Summary:
        //     The URI for a claim that specifies the multimedia messaging service gateway.
        public const string MMSGateway = "http://schemas.oneline.com/identity/claims/multimediamessagingservicegateway";
        //
        // Summary:
        //     The URI for a claim that specifies the password.
        public const string Password = "http://schemas.oneline.com/identity/claims/password";
    }
}
