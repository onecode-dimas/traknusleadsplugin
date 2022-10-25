using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using TraknusLeadsPlugin.BusinessLayer;

namespace TraknusLeadsPlugin
{
    public class PreLeadCreate_AgitEnhancement : Plugin
    {
        public PreLeadCreate_AgitEnhancement()
            : base(typeof(PreLeadCreate_AgitEnhancement))
        {
            base.RegisteredEvents.Add(new Tuple<int, string, string, Action<LocalPluginContext>>(20, "Create", "agt_leads",
                new Action<LocalPluginContext>(ExecutePreLeadCreate_AgitEnhancement)));

        }

        protected void ExecutePreLeadCreate_AgitEnhancement(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            ITracingService tracer = localContext.TracingService;
            BL_agt_leads _BL_leads = new BL_agt_leads();
            _BL_leads.PreCreate_Leads(localContext.OrganizationService, localContext.PluginExecutionContext, tracer);
        }
    }
}
