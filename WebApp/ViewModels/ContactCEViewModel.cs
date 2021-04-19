using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ContactCEViewModel
    {
        public Contact Contact { get; set; } = default!;
        public SelectList? ContactTypeSelectList { get; set; } = default!;
        public SelectList? GasStationSelectList { get; set; } = default!;
        public SelectList? RetailerSelectList { get; set; } = default!;
    }
}