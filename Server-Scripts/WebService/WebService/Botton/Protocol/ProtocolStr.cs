using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Botton.Protocol
{
    #region  字符串协议
    class ProtocolStr :ProtocolBase
    {
        public string Str;
        public override ProtocolBase Decode(byte[] readbuff, int start, int length)
        {
            ProtocolStr protocol = new ProtocolStr();
            protocol.Str = Encoding.UTF8.GetString(readbuff, start, length);

            return (ProtocolBase)protocol;
        }

        public override byte[] Encode()
        {
            byte[] bt = Encoding.UTF8.GetBytes(Str);
            return bt;
        }

        public override string GetName()
        {
            if (Str.Length == 0)
                return " ";
            return Str.Split(',')[0];
        }
        public override string GetDesc()
        {
            return Str;
        }
    }
    #endregion
}
