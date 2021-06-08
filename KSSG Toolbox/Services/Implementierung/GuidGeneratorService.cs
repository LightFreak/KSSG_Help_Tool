using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services.Impl
{
 internal class GuidGeneratorService : IGuidGeneratorService
  {
    public Guid GetNewGuid()
    {
      return Guid.NewGuid();
    }
  }
}
