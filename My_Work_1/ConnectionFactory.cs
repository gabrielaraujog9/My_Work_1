using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Work_1
{
    class ConnectionFactory
    {
        public static IOrganizationService GetCrmService()
        {
            string connectionString =
            "AuthType=OAuth;" +
            "Username=Grupo3@ProjetoDevFyi.onmicrosoft.com;" +
            "Password=Projeto123;" +
            "Url=https://org33e92d13.crm2.dynamics.com/;" +
            "AppId=4282436b-b719-45a2-9557-bcd95077b24e;" +
            "RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient crmServiceClient = new CrmServiceClient(connectionString);
            return crmServiceClient.OrganizationWebProxyClient;

        }
    }
}
