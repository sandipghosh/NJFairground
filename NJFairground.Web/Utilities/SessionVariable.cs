
namespace NJFairground.Web.Utilities
{
    public static class SessionVariable
    {
        private static string _UserId = "UserId";
        private static string _UserName = "UserName";


        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public static string UserId { get { return _UserId; } }
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public static string UserName { get { return _UserName; } }
        /// <summary>
        /// Gets the selected template.
        /// </summary>
        /// <value>
        /// The selected template.
        /// </value>
    }
}