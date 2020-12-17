using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChristmassCardGenerator.Models;

namespace ChristmassCardGenerator.ViewModels.Controllers
{
    public class HomeControllerViewModel
    {
        public class SendCardViewModel
        {
            public Card Card { get; set; }
            public List<IGrouping<ContactTypes, EmailList>> List { get; set; }
        }
    }
}
