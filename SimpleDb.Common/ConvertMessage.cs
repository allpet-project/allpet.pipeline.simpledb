using SimpleDb.Common.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Common
{
    public class ConvertMessage
    {
        public  static ICommand ConvertMessageObj(byte[] data,Method method )
        {            
            int index = 0;
            byte[] tlendata = new byte[4];
            Array.Copy(data, index, tlendata,0, 4);
            index += 4;
            int tlen = BitConverter.ToInt32(tlendata,0);
            byte[] tableid = new byte[tlen];
            Array.Copy(data, index, tableid, 0, tlen);
            index += tlen;

            ICommand command = null;

            byte[] lendata = new byte[4];
            switch (method)
            {
                case Method.CreateTable:
                    var message = new Message.CreatTableCommand();
                    message.TableId = tableid;                    
                    
                    Array.Copy(data, index, lendata, 0, 4);
                    index += 4;
                    int dlen = BitConverter.ToInt32(lendata, 0);
                    message.Data = new byte[dlen];
                    Array.Copy(data, index, message.Data, 0, dlen);

                    //command = message;
                    break; 
                case Method.GetDirect:
                    var getCommand = new Message.GetDirectCommand();
                    getCommand.TableId = tableid;

                    Array.Copy(data, index, lendata, 0, 4);
                    index += 4;
                    int keylen = BitConverter.ToInt32(lendata, 0);
                    getCommand.Key = new byte[keylen];
                    Array.Copy(data, index, getCommand.Key, 0, keylen);

                    command = getCommand;
                    break;
                case Method.PutDirect:
                    var putCommand = new Message.GetDirectCommand();
                    putCommand.TableId = tableid;

                    Array.Copy(data, index, lendata, 0, 4);
                    index += 4;
                    int putkeylen = BitConverter.ToInt32(lendata, 0);
                    putCommand.Key = new byte[putkeylen];
                    Array.Copy(data, index, putCommand.Key, 0, putkeylen);

                    command = putCommand;
                    break;
            }
            return command;
        }

    }
}
