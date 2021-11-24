﻿/*
 * MIT License
 *
 * Copyright (c) 2018 Clark Yang
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in 
 * the Software without restriction, including without limitation the rights to 
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
 * of the Software, and to permit persons to whom the Software is furnished to do so, 
 * subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;

namespace Loxodon.Framework.Net.Connection
{
    public class BinaryWriter : IDisposable
    {
        private const int DEFAULT_BUFFER_SIZE = 4192;
        private Stream output;
        private bool leaveOpen;
        private bool isBigEndian;
        private byte[] buffer;

        public BinaryWriter(Stream output, bool leaveOpen) : this(output, leaveOpen, true)
        {
        }

        public BinaryWriter(Stream output, bool leaveOpen, bool isBigEndian)
        {
            this.output = output;
            this.leaveOpen = leaveOpen;
            this.isBigEndian = isBigEndian;
            this.buffer = new byte[DEFAULT_BUFFER_SIZE];
        }

        public virtual Stream BaseStream { get { return this.output; } }

        public virtual bool IsBigEndian
        {
            get { return this.isBigEndian; }
            set { this.isBigEndian = value; }
        }

        public virtual void Write(byte value)
        {
            output.WriteByte(value);
        }

        public virtual void Write(ushort value)
        {
            if (isBigEndian)
            {
                buffer[1] = (byte)value;
                buffer[0] = (byte)(value >> 8);
            }
            else
            {
                buffer[0] = (byte)value;
                buffer[1] = (byte)(value >> 8);
            }
            output.Write(buffer, 0, 2);
        }

        public virtual void Write(short value)
        {
            if (isBigEndian)
            {
                buffer[1] = (byte)value;
                buffer[0] = (byte)(value >> 8);
            }
            else
            {
                buffer[0] = (byte)value;
                buffer[1] = (byte)(value >> 8);
            }
            output.Write(buffer, 0, 2);
        }

        public virtual void Write(uint value)
        {
            if (isBigEndian)
            {
                buffer[3] = (byte)value;
                buffer[2] = (byte)(value >> 8);
                buffer[1] = (byte)(value >> 16);
                buffer[0] = (byte)(value >> 24);
            }
            else
            {
                buffer[0] = (byte)value;
                buffer[1] = (byte)(value >> 8);
                buffer[2] = (byte)(value >> 16);
                buffer[3] = (byte)(value >> 24);
            }
            output.Write(buffer, 0, 4);
        }

        public virtual void Write(int value)
        {
            if (isBigEndian)
            {
                buffer[3] = (byte)value;
                buffer[2] = (byte)(value >> 8);
                buffer[1] = (byte)(value >> 16);
                buffer[0] = (byte)(value >> 24);
            }
            else
            {
                buffer[0] = (byte)value;
                buffer[1] = (byte)(value >> 8);
                buffer[2] = (byte)(value >> 16);
                buffer[3] = (byte)(value >> 24);
            }
            output.Write(buffer, 0, 4);
        }

        public virtual void Write(ulong value)
        {
            if (isBigEndian)
            {
                buffer[7] = (byte)value;
                buffer[6] = (byte)(value >> 8);
                buffer[5] = (byte)(value >> 16);
                buffer[4] = (byte)(value >> 24);
                buffer[3] = (byte)(value >> 32);
                buffer[2] = (byte)(value >> 40);
                buffer[1] = (byte)(value >> 48);
                buffer[0] = (byte)(value >> 56);
            }
            else
            {
                buffer[0] = (byte)value;
                buffer[1] = (byte)(value >> 8);
                buffer[2] = (byte)(value >> 16);
                buffer[3] = (byte)(value >> 24);
                buffer[4] = (byte)(value >> 32);
                buffer[5] = (byte)(value >> 40);
                buffer[6] = (byte)(value >> 48);
                buffer[7] = (byte)(value >> 56);
            }
            output.Write(buffer, 0, 8);
        }

        public virtual void Write(long value)
        {
            if (isBigEndian)
            {
                buffer[7] = (byte)value;
                buffer[6] = (byte)(value >> 8);
                buffer[5] = (byte)(value >> 16);
                buffer[4] = (byte)(value >> 24);
                buffer[3] = (byte)(value >> 32);
                buffer[2] = (byte)(value >> 40);
                buffer[1] = (byte)(value >> 48);
                buffer[0] = (byte)(value >> 56);
            }
            else
            {
                buffer[0] = (byte)value;
                buffer[1] = (byte)(value >> 8);
                buffer[2] = (byte)(value >> 16);
                buffer[3] = (byte)(value >> 24);
                buffer[4] = (byte)(value >> 32);
                buffer[5] = (byte)(value >> 40);
                buffer[6] = (byte)(value >> 48);
                buffer[7] = (byte)(value >> 56);
            }
            output.Write(buffer, 0, 8);
        }

        public virtual void Write(float value)
        {
            uint tmpValue = ToUInt32(value);
            buffer[0] = (byte)tmpValue;
            buffer[1] = (byte)(tmpValue >> 8);
            buffer[2] = (byte)(tmpValue >> 16);
            buffer[3] = (byte)(tmpValue >> 24);
            output.Write(buffer, 0, 4);
        }

        public virtual void Write(double value)
        {
            ulong tmpValue = ToUInt64(value);
            buffer[0] = (byte)tmpValue;
            buffer[1] = (byte)(tmpValue >> 8);
            buffer[2] = (byte)(tmpValue >> 16);
            buffer[3] = (byte)(tmpValue >> 24);
            buffer[4] = (byte)(tmpValue >> 32);
            buffer[5] = (byte)(tmpValue >> 40);
            buffer[6] = (byte)(tmpValue >> 48);
            buffer[7] = (byte)(tmpValue >> 56);
            output.Write(buffer, 0, 8);
        }

        public virtual void Write(byte[] buffer, int offset, int count)
        {
            output.Write(buffer, offset, count);
        }

        public virtual Task WriteAsync(byte[] buffer, int offset, int count)
        {
            return output.WriteAsync(buffer, offset, count);
        }

        public virtual async Task WriteAsync(IByteBuffer buffer, int count)
        {
            if (buffer is ByteBuffer buf)
            {
                int offset = buf.ArrayOffset + buf.ReaderIndex;
                await output.WriteAsync(buf.Array, offset, count);
                buf.ReaderIndex += count;
            }
            else
            {
                int n = 0;
                while (n < count)
                {
                    int len = Math.Min(this.buffer.Length, count - n);
                    buffer.ReadBytes(this.buffer, 0, len);
                    await output.WriteAsync(this.buffer, 0, len);
                    n += len;
                }
            }
        }

        public virtual void Flush()
        {
            output.Flush();
        }

        public virtual Task FlushAsync()
        {
            return output.FlushAsync();
        }

        [SecuritySafeCritical]
        protected unsafe uint ToUInt32(float value)
        {
            return *(uint*)&value;
        }

        [SecuritySafeCritical]
        protected unsafe ulong ToUInt64(double value)
        {
            return *(ulong*)&value;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (!leaveOpen && output != null)
                {
                    this.output.Close();
                    this.output = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
