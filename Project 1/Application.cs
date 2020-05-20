using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu;

namespace RentApp
{
    class Application
    {
        public void Run()
        {
            IWelcomeScreen sc = new WelcomeScreen();
            sc.WriteWelcomeMessage();



        }
    }
}
