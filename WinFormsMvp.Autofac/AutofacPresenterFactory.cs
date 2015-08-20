using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvp;
using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using WinFormsMvp.Binder;

namespace WinFormsMvp.Autofac
{
    public class AutofacPresenterFactory : IPresenterFactory
    {
        protected IContainer Container;

        protected Dictionary<IPresenter, ILifetimeScope> PresenterScopes = new Dictionary<IPresenter, ILifetimeScope>(); 

        public AutofacPresenterFactory(IContainer container)
        {
            Container = container;
        }

        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            var scope = Container.BeginLifetimeScope();

            var presenter = scope.Resolve(presenterType, new NamedParameter("view", viewInstance)) as IPresenter;

            if (presenter != null)
            {
                PresenterScopes.Add(presenter, scope);
            }

            return presenter;
        }

        public void Release(IPresenter presenter)
        {
            // TODO need to figure out the scope of the objects here.

            if (PresenterScopes.ContainsKey(presenter))
            {
                var scope = PresenterScopes[presenter];
                PresenterScopes.Remove(presenter);

                scope.Dispose();
            }

            var disposablePresenter = presenter as IDisposable;
            if (disposablePresenter != null)
            {
                disposablePresenter.Dispose();
            }

        }
    }
}
