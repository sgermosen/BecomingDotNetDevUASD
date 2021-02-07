using EcCoach.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.Interfaces
{
  public  interface ICurrentUserFactory
    {
       CurrentUser Get { get; }
    }
}
