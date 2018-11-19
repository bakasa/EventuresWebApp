using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.App.Filters;
using Eventures.App.ViewModels.Events;
using Eventures.Data;
using Eventures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eventures.App.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly EventuresDbContext dbContext;

        public EventsController(EventuresDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult All()
        {
            var allEventsViewModel = new AllEventsViewModel
            {
                Events = this.dbContext.Events.Select(e => new EventSmallDto
                {
                    Name = e.Name,
                    Place = e.Place,
                    Start = e.Start,
                    End = e.End
                }).ToList()
            };

            return View(allEventsViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [TypeFilter(typeof(LogActionFilter))]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(CreateEventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newEvent = new Event
            {
                Name = model.Name,
                Place = model.Place,
                Start = model.Start,
                End = model.End,
                TotalTickets = model.TotalTickets,
                PricePerTicket = model.PricePerTicket
            };

            this.dbContext.Events.Add(newEvent);
            try
            {
                this.dbContext.SaveChanges();
                return this.RedirectToAction("All");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}