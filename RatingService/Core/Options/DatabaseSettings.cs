using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DatabaseSettings
{
    /// <summary>
    /// Database settings strings interface.
    /// </summary>
    public interface IDatabaseSettings
    {
        #region Members

        /// <summary>
        /// Contains string to connect to database.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Contains database name.
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// Contains user collection name.
        /// </summary>
        string UserCollection { get; set; }

        /// <summary>
        /// Contains movie collection name.
        /// </summary>
        string MovieCollection { get; set; }

        /// <summary>
        /// Contains review collection name.
        /// </summary>
        string ReviewCollection { get; set; }

        #endregion
    }

    /// <summary>
    /// Database settings strings.
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        #region Members

        /// <summary>
        /// Contains string to connect to database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Contains database name.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Contains user collection name.
        /// </summary>
        public string UserCollection { get; set; }

        /// <summary>
        /// Contains movie collection name.
        /// </summary>
        public string MovieCollection { get; set; }

        /// <summary>
        /// Contains review collection name.
        /// </summary>
        public string ReviewCollection { get; set; }

        #endregion
    }
}
