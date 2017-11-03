using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityUi.Core;
using System.Web;
using Moq;
using System.IO;

namespace EntityUi.Test
{
    [TestClass]
    public class EntityUiTest
    {
        private class TestClass : ViewModelBase
        {

        }

        //[TestMethod]
        public void Check_View_Model_Parent_Key_Test()
        {
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );
            

            var testClass = new TestClass();

            HttpContext.Current.Session["TestClass_Parent"] = 1;

            Assert.IsTrue(testClass.ParentId == 1);
        }
    }
}
