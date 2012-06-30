using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSAPI
{
    internal sealed class FSUri
    {
        internal static string PartnerDomain    =   "http://www.dev.usys.org";
        internal static string SandboxDomain    =   "https://sandbox.familysearch.org";
        internal static string StagingDomain    =   "https://identbeta.familysearch.org";
        internal static string ProductionDomain =   "https://ident.familysearch.org";

        internal static string OAuth2_AuthPath = "/cis-web/oauth2/v3/authorization";
        internal static string OAuth2_TokenPath = "/cis-web/oauth2/v3/token";

        internal static string IdentityV2Path = "/identity/v2";

        internal static string ActiveDomain = SandboxDomain;


        internal const string FSNamespaceXsi = "http://www.w3.org/2001/XMLSchema-instance";//xmlns:xsi=
    }
}
