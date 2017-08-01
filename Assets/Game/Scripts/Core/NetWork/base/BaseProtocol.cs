using System;
using System.Collections.Generic;

namespace Plates.Client.Net
{
    public interface IBaseProtocol
    {
        bool SignalConnectResult(bool bResult);
        bool SignalInputBuffer(ref List<byte> data);
        bool SignalSendCompleted();
    }
}
