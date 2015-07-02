using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Rigsom.SecureVault.Frontend.Model
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class NewData
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString SecureData { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString Password { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="password"></param>
        public NewData(SecureString password)
        {
            this.Password = password;
        }
    }
}
