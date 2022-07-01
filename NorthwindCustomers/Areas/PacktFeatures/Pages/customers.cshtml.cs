using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;
using System.Linq;
using System.Collections.Generic;

namespace PacktFeatures.Pages
{
    public class CustomersPageModel : PageModel
    {
        private Northwind db;
        public CustomersPageModel(Northwind injectedContext)
        {
            db = injectedContext;
        }
        
        public IEnumerable<IGrouping<string, Customer>> Customers {get; set;}
        public void OnGet()
        {
            Customers = db.Customers.AsEnumerable().GroupBy(cus=>cus.Country).OrderBy(cus=>cus.Key);
        }
    }

}

