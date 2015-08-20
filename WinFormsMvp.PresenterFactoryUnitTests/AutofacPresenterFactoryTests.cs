using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.ResolveAnything;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsMvp.Autofac;

namespace WinFormsMvp.PresenterFactoryUnitTests
{
    [TestClass]
    public class AutofacPresenterFactoryTests : PresenterFactoryTests
    {
        protected override Binder.IPresenterFactory BuildFactory()
        {
            var binder = new ContainerBuilder();
            binder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            return new AutofacPresenterFactory(binder.Build());
        }
    }
}
