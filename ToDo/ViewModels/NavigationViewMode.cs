using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Events;
using ToDo.Extensions;

namespace ToDo.ViewModels
{
    public class NavigationViewMode : BindableBase, INavigationAware
    {
        private readonly IContainerProvider container;
        public readonly IEventAggregator aggregator;

        public NavigationViewMode(IContainerProvider container)
        {
            this.container = container;
            aggregator = container.Resolve<IEventAggregator>();
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public void UpdateLoading(bool IsOpen)
        {
            aggregator.UpdateLoading(new UpdateModel() { IsOpen = IsOpen });

        }
    }
}
