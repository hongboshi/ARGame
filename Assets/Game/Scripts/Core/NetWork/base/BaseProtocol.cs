using System;
using System.Collections.Generic;

namespace SimpleFramework.Net
{
    public interface IBaseProtocol
    {
        bool SignalConnectResult(bool bResult);
        bool SignalInputBuffer(ref List<byte> data);
        bool SignalSendCompleted();
    }
}
