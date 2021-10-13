using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Work_1.Model
{
    class Oportunidade
    {
        public string TableName = "opportunity";
        public Guid OpportunityId { get; set; }
        public IOrganizationService Service { get; set; }
        public Oportunidade(IOrganizationService service, Guid opportunityId)
        {
            this.Service = service;
            this.OpportunityId = opportunityId;
        }
        public EntityCollection RetriveMultipleOpportunity()
        {
            QueryExpression queryOpportunity = new QueryExpression(this.TableName);
            queryOpportunity.ColumnSet.AddColumns("name","parentaccountid", "totallineitemamount", "discountamount");
            queryOpportunity.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, OpportunityId);

           

            return this.Service.RetrieveMultiple(queryOpportunity);
        }
        public void UpdateOpportunity(Money valorDetalhado, double valorDesconto)
        {
            Entity opportunity = new Entity(this.TableName);
            opportunity.Id = OpportunityId;
            opportunity["discountamount"] = (((double)valorDetalhado.Value) * valorDesconto)/100;
            this.Service.Update(opportunity);
        }
    }
}
