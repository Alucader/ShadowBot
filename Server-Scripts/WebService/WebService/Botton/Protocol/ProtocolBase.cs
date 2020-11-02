using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Botton.Protocol
{
    class ProtocolBase
    {
        /// <summary>
        /// 解码器，readbuff中从start开始的length个字节
        /// </summary>
        /// <param name="readbuff">缓冲区</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">消息长度</param>
        /// <returns></returns>
        public virtual ProtocolBase Decode(byte [] readbuff,int start,int length)
        {
            return new  ProtocolBase();
        }
        //编码器
        public virtual byte[] Encode()
        {
            return new  byte [] { };
        }
        //协议名称，用于分发消息
        public virtual string GetName()
        {
            return "";
        }
        //描述
        public virtual string GetDesc()
        {
            return "";
        }
    }
}
