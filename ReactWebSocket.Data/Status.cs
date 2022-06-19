using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactWebSocket.Data
{
    public enum Status
    {
        Available,
        TakenByOtherUser,
        TakenByCurrentUser,
        Done
    }
}
