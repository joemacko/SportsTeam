using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenFiftySports.Services
{
    class CustomerService
    {

        private readonly Guid _customerId;
        public CustomerService(Guid customerId)
        {
            _customerId = customerId;
        }


    }
}
