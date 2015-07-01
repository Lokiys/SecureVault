using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Frontend.Model
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class Configuration
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString MasterPassword { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString MasterPasswordConfirmation { get; set; }
    }
}
