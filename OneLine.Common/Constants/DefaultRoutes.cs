namespace OneLine.Constants
{
    /// <summary>
    /// Default routes used by an api
    /// </summary>
    public static class DefaultRoutes
    {
        /// <summary>
        /// Api default route template
        /// </summary>
        public static class Api
        {
            /// <summary>
            /// Api default route template: "api/[controller]"
            /// </summary>
            public const string Default = "api/[controller]";
        }
        /// <summary>
        /// Base controller routes
        /// </summary>
        public static class BaseController
        {
            public const string Add = "Add";
            public const string Update = "Update";
            public const string Delete = "Delete";
            public const string GetOne = "GetOne";
            public const string Search = "Search";
            public const string List = "List";
            public const string DownloadCsvExcel = "DownloadCsvExcel";
            public const string SaveReplaceList = "SaveReplaceList";
        }
    }
}
