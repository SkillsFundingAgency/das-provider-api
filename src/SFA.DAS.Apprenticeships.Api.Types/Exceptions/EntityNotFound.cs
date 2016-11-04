using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Apprenticeships.Api.Types.Exceptions
{
    public class EntityNotFound : ApplicationException
    {
        public EntityNotFound(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
