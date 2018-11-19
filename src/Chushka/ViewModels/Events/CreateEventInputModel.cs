using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.App.ViewModels.Events
{
    public class CreateEventInputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Place { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TotalTickets { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }
    }
}
