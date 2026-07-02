using MergeIni.Model;

namespace MergeIni
{
    internal class IniWriter : IDisposable
    {
        private readonly Stream _stream;
        private readonly string _lineEnding;
        private bool _disposedValue;

        public IniWriter(Stream stream, string lineEnding)
        {
            _stream = stream;
            _lineEnding = lineEnding;
        }

        public IniWriter(string filename, string lineEnding)
            : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), lineEnding)
        {
        }

        public void Write(IniDocument document)
        {
            using var writer = new StreamWriter(_stream);

            foreach (var section in document.Sections)
            {
                writer.Write($"[{section.Name}]");
                writer.Write(this._lineEnding);

                foreach (var value in section.Values)
                {
                    writer.Write($"{value.Key}={value.Value}");
                    writer.Write(this._lineEnding);
                }

                writer.Write(this._lineEnding);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
