using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

using CookBookAPI.Filters;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CookBookAPI.Tests.Filters
{
    [TestFixture]
    public class ValidateModelStateAttributeFixture
    {
        [Test]
        public void ActionContextIsNull_ShouldThrowException()
        {
            var sut = new ValidateModelStateAttribute();

            Assert.Throws<ArgumentNullException>(() => sut.OnActionExecuting(null));
        }

        [Test]
        public void ModelStateIValid_ShouldNotSetResponse()
        {
            var fixture = new Fixture();

            var sut = new ValidateModelStateAttribute();
            var actionContext = new HttpActionContext();

            sut.OnActionExecuting(actionContext);

            var response = actionContext.Response;
            Assert.IsNull(response);
        }

        [Test]
        public void ModelStateIsNotValid_ShouldSetResponseToBadRequest()
        {
            var fixture = new Fixture();

            var sut = new ValidateModelStateAttribute();
            var actionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext { Request = new HttpRequestMessage() }
            };
            actionContext.ModelState.AddModelError(fixture.Create<string>(), fixture.Create<string>());

            sut.OnActionExecuting(actionContext);

            var response = actionContext.Response;
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
