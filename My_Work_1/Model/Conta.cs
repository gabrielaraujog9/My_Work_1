using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Work_1.Model
{
    class Conta
    {
        public string TableName = "account";

        public Guid accountId { get; set; }
        public IOrganizationService Service { get; set; }
        public Conta(IOrganizationService service, Guid accountId)
        {
            this.Service = service;
            this.accountId = accountId;
        }
        public EntityCollection RetriveMultipleAccountByNivelDoCliente()
        {
            QueryExpression queryAccount = new QueryExpression(this.TableName);
            queryAccount.ColumnSet.AddColumns("fyi_niveldocliente2");
            queryAccount.Criteria.AddCondition("accountid", ConditionOperator.Equal, accountId);

            queryAccount.AddLink("fyi_niveldocliente", "fyi_niveldocliente2", "fyi_niveldoclienteid", JoinOperator.Inner);
            queryAccount.LinkEntities[0].Columns.AddColumns("fyi_name", "fyi_valordodesconto");
            queryAccount.LinkEntities[0].EntityAlias = "nivel";


            return this.Service.RetrieveMultiple(queryAccount);
        }
    }
}
