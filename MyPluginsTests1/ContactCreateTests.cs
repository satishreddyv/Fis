using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPlugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MyPlugins.Tests
{
    [TestClass()]
    public class ContactCreateTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {

            var fakedContext = new XrmFakedContext();

            Entity contact = new Entity("contact");
            //contact.Id = Guid.NewGuid();
            contact.Attributes.Add("lastname", "TestContact");

            var inputParams = new ParameterCollection();
            inputParams.Add("Target", contact);

            var outputParams = new ParameterCollection();
            inputParams.Add("id", Guid.NewGuid());

            fakedContext.ExecutePluginWith<ContactCreate>(inputParams, outputParams, null, null);


            QueryExpression query = new QueryExpression("task");
            query.ColumnSet.AllColumns = true;

            IOrganizationService fakedService = fakedContext.GetOrganizationService();

            EntityCollection collection = fakedService.RetrieveMultiple(query);


            Assert.IsTrue(collection.Entities.Count == 1);




        }


        [TestMethod()]
        public void DuplicteTest()
        {

            var fakedContext = new XrmFakedContext();

            Entity existingcontact = new Entity("contact");
            //contact.Id = Guid.NewGuid();
            existingcontact.Attributes.Add("emailaddress1", "test@test.com");

            IOrganizationService fakedService = fakedContext.GetOrganizationService();
            fakedService.Create(existingcontact);


            Entity newcontact = new Entity("contact");
            //contact.Id = Guid.NewGuid();
            newcontact.Attributes.Add("emailaddress1", "test2@test.com");

            Assert.ThrowsException<InvalidPluginExecutionException>(() =>
            fakedContext.ExecutePluginWithTarget<ContactDuplicateCheck>(newcontact, "Create", 10),"Failed");


        }
    }

    public class Example
    {
        public static int Sum(int a, int b)
        {

            return a + b;
        }
    }
}