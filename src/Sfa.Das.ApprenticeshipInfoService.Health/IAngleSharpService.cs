using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.ApprenticeshipInfoService.Health
{
    public interface IAngleSharpService
    {
        IList<string> GetLinks(string fromUrl, string selector, string textInTitle);
    }
}
