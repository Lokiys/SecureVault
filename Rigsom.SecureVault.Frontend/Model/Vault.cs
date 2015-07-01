using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Collections.ObjectModel;

namespace Rigsom.SecureVault.Frontend.Model
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    class Vault
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public ObservableCollection<SavedData> SavedData { get; set; }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        public SecureString Password { get; set; }
    }
}
