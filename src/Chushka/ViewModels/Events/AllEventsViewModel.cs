using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.App.ViewModels.Events
{
    public class AllEventsViewModel
    {
        public AllEventsViewModel()
        {
            this.Events = new List<EventSmallDto>();
        }

        public ICollection<EventSmallDto> Events { get; set; }
    }
}