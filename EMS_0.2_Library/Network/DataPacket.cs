﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EMS_Library.Network
{
    public class DataPacket
    {
        public readonly DataPacketHeader _header;
        public readonly string StringData;
        readonly byte[] _byteData;
        /// <summary>
        /// בנאי שמקבל זרם נתונים ובונה מהם חבילת מידע
        /// Reconstructs recieved data as DataPacket
        /// </summary>
        /// <param name="stream"></param>
        public DataPacket(NetworkStream stream)
        {
            try { 
                byte[] temp = new byte[]{
                    (byte)stream.ReadByte(),
                    (byte)stream.ReadByte(),
                    (byte)stream.ReadByte(),
                    (byte)stream.ReadByte()
                };
                int length;
                try { length = BitConverter.ToInt32(temp, 0); }
                catch (ArgumentOutOfRangeException) { length = int.MaxValue; Console.WriteLine($"DataPacket length {BitConverter.ToInt64(temp, 0)}! setting to {int.MaxValue}"); }
                _header = new DataPacketHeader(length, (byte)stream.ReadByte());
                _byteData = new byte[_header.DataIntLength];
                stream.Read(_byteData, 0, _header.DataIntLength);

                StringData = Encoding.ASCII.GetString(_byteData, 0, _header.DataIntLength);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// בנאי שמקבל סטרינג ובונה ממנו חבילת מידע
        /// Constructs a Data Packet to be sent to the server
        /// </summary>
        /// <param name="header"></param>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        public DataPacket(string data, byte func)
        {
            if (data.Length > Math.Pow(255,4)-20) throw new Exception("Data is too long! 4294967296 max! Lenght was " + data.Length);
            if (data.Length == 0) data = " ";
            _header = new DataPacketHeader(data.Length, func);
            StringData = data;
            _byteData = Encoding.ASCII.GetBytes(data);
        }
        public DataPacket(DataPacket data)
        {
            _header = data._header;
            _byteData = data._byteData;
            StringData = data.StringData;
        }
        public byte[] Write() => _header.GetHeader().Concat(_byteData).ToArray();
        /// <summary>
        /// Returns a Copy of the byte array.
        /// </summary>
        public byte[] ByteData
        {
            get
            {
                byte[] hold = new byte[_byteData.Length];
                _byteData.CopyTo(hold, 0);
                return hold;
            }
        }

        public int GetTotalSize() => _header.DataIntLength + _header.HeaderSize;
        public override string ToString()
        {
            return _header + " " + StringData;
        }
    }
}
