using MergeIni.Model;

namespace MergeIni
{
    internal class IniWriter : IDisposable
    {
        private readonly Stream _stream;
        private bool _disposedValue;

        public IniWriter(Stream stream)
        {
            _stream = stream;
        }

        public IniWriter(string filename)
            : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
        }

        public void Write(IniDocument document)
        {
            using var writer = new StreamWriter(_stream);

            foreach (var section in document.Sections)
            {
                writer.WriteLine($"[{section.Name}]");

                foreach (var value in section.Values)
                    writer.WriteLine($"{value.Key}={value.Value}");

                writer.WriteLine();
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
