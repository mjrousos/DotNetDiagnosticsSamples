using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace TargetApp
{
    public class NativeProfilePictureBad : IDisposable, IEnumerable<byte>
    {
        private const int Length = 100 * 1024;

        public IntPtr _picturePtr;

        public NativeProfilePictureBad(Guid id)
        {
            _picturePtr = Marshal.AllocHGlobal(Length);
        }

        public void Dispose()
        {
            if (_picturePtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_picturePtr);
                _picturePtr = IntPtr.Zero;
            }
        }

        public IEnumerator<byte> GetEnumerator()
        {
            return new NativeProfilePictureEnumerator(_picturePtr, Length);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal unsafe class NativeProfilePictureEnumerator : IEnumerator<byte>
        {
            byte* _bytes;
            int _length;
            int _index;

            public NativeProfilePictureEnumerator(IntPtr ptr, int length)
            {
                _length = length;
                _bytes = (byte*)ptr.ToPointer();
                _index = 0;
            }

            public byte Current => _bytes[_index];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                while (true)
                {
                    var idx = _index;
                    if (idx >= _length - 1)
                    {
                        return false;
                    }

                    if (Interlocked.CompareExchange(ref _index, idx + 1, idx) == idx)
                    {
                        return true;
                    }
                }
            }

            public void Reset()
            {
                _index = 0;
            }
        }
    }
}