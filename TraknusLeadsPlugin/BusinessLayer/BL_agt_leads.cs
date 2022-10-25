using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace TraknusLeadsPlugin.BusinessLayer
{
    class BL_agt_leads
    {
        #region Constants
        private const string _classname = "BL_agt_leads";
        private string _entityname_account = "agt_leads";
        private string _attrname_leadsnumber = "agt_name";

        private string _attrname_statusleads = "agt_statusleads";


        #endregion

        public void PreCreate_Leads(IOrganizationService organizationservice, IPluginExecutionContext pluginExecutionContext, ITracingService tracer)
        {
            try
            {
                Entity entity = (Entity)pluginExecutionContext.InputParameters["Target"];


                #region user
                //ambil user login(owner) pembuat leads
                var userid = pluginExecutionContext.UserId;
                Entity systemuser = organizationservice.Retrieve("systemuser", userid , new ColumnSet(true));
                var positionidref = systemuser.Contains("positionid") ? systemuser.GetAttributeValue<EntityReference>("positionid") : null;

                if(positionidref == null)
                {
                    return;
                }

                Entity positiondata = organizationservice.Retrieve("position", positionidref.Id, new ColumnSet(true));
                var positionname = positiondata.Contains("name") ? positiondata.GetAttributeValue<string>("name") : null;

                if(string.IsNullOrEmpty(positionname))
                {
                    return;
                }

                QueryExpression userQuery = new QueryExpression("systemuser");
                userQuery.ColumnSet.AddColumns("positionid");
                EntityCollection listentitiesUser = organizationservice.RetrieveMultiple(userQuery);
                var user = listentitiesUser.Entities.FirstOrDefault();

                //throw new InvalidPluginExecutionException("$tes " + user.GetAttributeValue<>);
                #endregion



                var tahunleads = DateTime.Now.Year % 100;
                tahunleads.ToString("yy");

                QueryExpression query = new QueryExpression("agt_leads");
                query.ColumnSet.AddColumns("agt_name");
                EntityCollection listentities = organizationservice.RetrieveMultiple(query);

                int hitung = listentities.Entities.Count;
                int duplikat = listentities.Entities.Count;
                int hitungdua = hitung + 1;
                int hitungduaduplikat = hitung + 1;

                //while (hitungdua.Equals(duplikat))
                //{
                //    hitungdua = hitung + 1;
                //}
                string tostring = hitungdua.ToString().PadLeft(5, '0');
                //if(tostring.Equals(tostring))
                //{
                //    int hitunglagi = hitungdua + 1;
                //    tostring = hitunglagi.ToString().PadLeft(5, '0');
                //}





                //if (statusleadss == 4)
                if (positionname.Equals("Admin Contact Center"))
                {
                    /*
                    while (tostring.Equals(tostring))
                    {
                        int hitunglagi = hitungdua + 1;
                        tostring = hitunglagi.ToString().PadLeft(5, '0');
                    }
                    */

                    while (hitungdua.Equals(hitungduaduplikat))
                    {
                        hitungdua = hitung + 1;
                    }
                    tostring = hitungdua.ToString().PadLeft(5, '0');
                    //if (tostring.Equals(tostring))
                    //{
                    //    int hitunglagi = hitungdua + 1;
                    //    tostring = hitunglagi.ToString().PadLeft(5, '0');
                    //}

                    entity.Attributes.Add("agt_name", "TCC / " + tahunleads + " / " + tostring);
                    

                }
                else if(positionname.Equals("Parts Department Head"))
                {
                    if (tostring.Equals(tostring))
                    {
                        int hitunglagi = hitungdua + 1;
                        tostring = hitunglagi.ToString().PadLeft(5, '0');
                    }
                    entity.Attributes.Add("agt_name", "PDD / " + tahunleads + " / " + tostring);
                }
                else if (positionname.Equals("Key Account"))
                {
                    if (tostring.Equals(tostring))
                    {
                        int hitunglagi = hitungdua + 1;
                        tostring = hitunglagi.ToString().PadLeft(5, '0');
                    }

                    entity.Attributes.Add("agt_name", "KA / " + tahunleads + " / " + tostring);
                }
                else if (positionname.Equals("Service Administration Officer"))
                {
                    if (tostring.Equals(tostring))
                    {
                        int hitunglagi = hitungdua + 1;
                        tostring = hitunglagi.ToString().PadLeft(5, '0');
                    }
                    entity.Attributes.Add("agt_name", "ADS / " + tahunleads + " / " + tostring);
                }
                /*
                else if (positionname.Equals("Business Consultant"))
                {
                    entity.Attributes.Add("agt_name", "TES ADMIN CRM / " + tahunleads + " / " + tostring);
                } 
                */
                else
                {
                    if (tostring.Equals(tostring))
                    {
                        int hitunglagi = hitungdua + 1;
                        tostring = hitunglagi.ToString().PadLeft(5, '0');
                    }
                    entity.Attributes.Add("agt_name", "- / " + tahunleads + " / " + tostring);
                }


            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(_classname + ".PreCreate_Leads: " + ex.Message.ToString());
            }
        }
    }
}
