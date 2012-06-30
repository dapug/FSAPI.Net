using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSAPI
{

    /// <summary>
    /// The FamilySearch server to target for requests
    /// </summary>
    public enum FSServer
    {
        /// <summary>
        /// Server not specified
        /// </summary>
        Null,

        /// <summary>
        /// The Partner server of FamilySearch (for certified affiliate use)
        /// </summary>
        Partner,

        /// <summary>
        /// The sandbox server of FamilySearch (for general development and test purposes) (default)
        /// </summary>
        Sandbox,

        /// <summary>
        /// The Staging server of FamilySearch
        /// </summary>
        Staging,

        /// <summary>
        /// The Live Production server of FamilySearch
        /// </summary>
        Production

    }
}
