using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Catering_API.SignalR
{
    public interface IHubClient
    {
        Task InformClient(string message);
    }
}
