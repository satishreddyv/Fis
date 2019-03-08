using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Url = https://dynamicslabs2.crm8.dynamics.com; Username=satish@dynamicslabs2.onmicrosoft.com; Password=Pearl@123; authtype=Office365";
            CrmServiceClient service = new CrmServiceClient(connectionString);

            //Entity contact = new Entity("contact");
            //contact.Attributes.Add("lastname", "Smith");

            //Console.WriteLine(service.Create(contact));


            //QueryByAttribute query = new QueryByAttribute("contact");
            //query.AddAttributeValue("address1_city", "Redmond");
            //query.ColumnSet = new ColumnSet();
            //query.ColumnSet.AddColumns(new string[] { "firstname", "lastname" });



            //EntityCollection collection = service.RetrieveMultiple(query);

           

            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
  <entity name='contact'>
    <attribute name='fullname' />
    <attribute name='telephone1' />
    <attribute name='contactid' />
    <order attribute='fullname' descending='false' />
    <filter type='and'>
      <condition attribute='address1_city' operator='eq' value='Redmond' />
    </filter>
  </entity>
</fetch>";


            EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));
            foreach (Entity item in collection.Entities)
            {
                if(item.Attributes.Contains("fullname"))
                Console.WriteLine(item.Attributes["fullname"].ToString());
            }

            Console.ReadLine();
        }
    }
}
