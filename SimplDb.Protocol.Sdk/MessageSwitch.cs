using SimpleDb.Common;
using System;
using System.Runtime.InteropServices;

namespace SimplDb.Protocol.Sdk
{
    class SimpledbMessageSwitch
    {
        /// <summary>
        /// 将结构体转换为byte数组
        /// </summary>
        /// <typeparam name="T"> 泛型T</typeparam>
        /// <param name="structObj">结构体对象</param>
        /// <returns></returns>
        public static byte[] StructToBytes<T>(T structObj) where T : struct
        {
            // 获取结构体对象的字节数
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];
            // 申请内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体内容拷贝到上一步申请的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            // 将数据拷贝到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            // 释放申请的内存
            Marshal.FreeHGlobal(structPtr);

            return bytes;
        }

        public static T BytesToStruct<T>(byte[] bytes) where T : struct
        {
            T obj = new T();

            int size = Marshal.SizeOf(obj);

            // 如果结构体对象的字节数大于所给byte数组的长度，则返回空
            if (size > bytes.Length)
            {
                return (default(T));
            }

            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, structPtr, size);
            object tempObj = Marshal.PtrToStructure(structPtr, obj.GetType());
            Marshal.FreeHGlobal(structPtr);

            return (T)tempObj;
        }

        public static byte[] CommandToBytes<T>(T command) where T:ICommand
        {
            Type commandType = command.GetType();
            byte[] data = new byte[Marshal.SizeOf(command)];

            int offset = 0;
            object fieldValue;
            TypeCode typeCode;
            
            int fiedLen = 0;
            foreach (var field in commandType.GetFields())
            {
                fieldValue = field.GetValue(commandType); // Get value
                typeCode = Type.GetTypeCode(fieldValue.GetType());  // get type
                byte[] temp;
                //计算字段的长度
                if (typeCode == TypeCode.Object)
                {
                    fiedLen = ((byte[])fieldValue).Length;
                    var lenByte = BitConverter.GetBytes(fiedLen);
                    Array.Copy(lenByte, 0, data, offset, lenByte.Length);
                    offset += lenByte.Length;
                    Array.Copy(((byte[])fieldValue), 0, data, offset, fiedLen);
                    offset += fiedLen;
                    continue;
                }
                else
                {
                    fiedLen = Marshal.SizeOf(fieldValue);
                    Array.Copy(BitConverter.GetBytes(fiedLen), 0, data, offset, fiedLen);
                    offset += fiedLen;
                }

                switch (typeCode)
                {
                    case TypeCode.Single: // float
                        {
                            temp = BitConverter.GetBytes((Single)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(Single));
                            break;
                         }
                    case TypeCode.Int32:
                        {
                            temp = BitConverter.GetBytes((Int32)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(Int32));
                            break;
                        }
                    case TypeCode.UInt32:
                        {
                            temp = BitConverter.GetBytes((UInt32)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(UInt32));
                            break;
                        }
                    case TypeCode.Int16:
                        {
                            temp = BitConverter.GetBytes((Int16)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(Int16));
                            break;
                        }
                    case TypeCode.UInt16:
                        {
                            temp = BitConverter.GetBytes((UInt16)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(UInt16));
                            break;
                        }
                    case TypeCode.Int64:
                        {
                            temp = BitConverter.GetBytes((Int64)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(Int64));
                            break;
                        }
                    case TypeCode.UInt64:
                        {
                            temp = BitConverter.GetBytes((UInt64)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(UInt64));
                            break;
                        }
                    case TypeCode.Double:
                        {
                            temp = BitConverter.GetBytes((Double)fieldValue);                            
                            Array.Copy(temp, 0, data, offset, sizeof(Double));
                            break;
                        }
                    case TypeCode.Byte:
                        {
                            data[offset] = (Byte)fieldValue;
                            break;
                        }
                    default:
                        break;
                }
                
                offset += fiedLen;
                
            }
            return data;
        }

    }
}
