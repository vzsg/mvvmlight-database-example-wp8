using System.Diagnostics;
using System.IO;
using System.Text;

namespace MvvmDatabaseExample.Database
{
    // Source: http://blogs.msdn.com/b/jgalla/archive/2011/05/27/datacontext-log-and-windows-phone.aspx
    public class DebugStreamWriter : TextWriter
    {
        private readonly StringBuilder _buffer;

        public DebugStreamWriter()
        {
            BufferSize = 256;
            _buffer = new StringBuilder(BufferSize);
        }

        private int BufferSize
        {
            get; set;
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        #region StreamWriter Overrides
        public override void Write(char value)
        {
            _buffer.Append(value);
            if (_buffer.Length >= BufferSize)
                Flush();
        }

        public override void WriteLine(string value)
        {
            Flush();

            using (var reader = new StringReader(value))
            {
                string line;
                while (null != (line = reader.ReadLine()))
                    Debug.WriteLine(line);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Flush();
        }

        public override void Flush()
        {
            if (_buffer.Length > 0)
            {
                Debug.WriteLine(_buffer);
                _buffer.Clear();
            }
        }
        #endregion
    }
}
