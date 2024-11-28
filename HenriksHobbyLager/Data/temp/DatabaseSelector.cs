using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Data
{
    internal class DatabaseSelector
    {
        public string CurrentDatabase { get; private set; }

        public void SetDatabase(string databaseType)
        {
            CurrentDatabase = databaseType ?? throw new ArgumentNullException(nameof(databaseType));
        }
    }
}
