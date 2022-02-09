using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Features
{
    /// <summary>
    /// Encryption strings interface.
    /// </summary>
    public interface IEncryptionSettings
    {
        #region Members

        /// <summary>
        /// Key used when a password is encrypted.
        /// </summary>
        string Key { get; set; }

        #endregion
    }

    /// <summary>
    /// Encryption strings.
    /// </summary>
    public class EncryptionSettings : IEncryptionSettings
    {
        #region Members

        /// <summary>
        /// Key used when a password is encrypted.
        /// </summary>
        public string Key { get; set; }

        #endregion
    }
}
