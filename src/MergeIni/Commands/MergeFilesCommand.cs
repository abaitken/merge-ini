using MergeIni.Model;
using MergeIni.Options;

namespace MergeIni.Commands
{
    class MergeFilesCommand : Command
    {
        internal override int Execute(CommandLineOptions options)
        {
            var outputFile = options.OutputFile;
            if (string.IsNullOrWhiteSpace(outputFile))
            {
                ConsoleHelper.WriteError("Output file (-o) is required");
                ConsoleHelper.WriteHelpHint();
                return ExitCodes.Error;
            }

            var mergeItems = options.MergeItems.ToList();
            if (mergeItems.Count < 1)
            {
                ConsoleHelper.WriteError("At least one merge item (-m or -l) is required");
                ConsoleHelper.WriteHelpHint();
                return ExitCodes.Error;
            }

            if (!GatherMergeFiles(mergeItems, out var mergeFiles))
                return ExitCodes.Error;

            var merger = new IniDocumentMerger();
            var finalDocument = new IniDocument();

            foreach (var file in mergeFiles)
            {
                using var reader = new IniReader(file);
                var document = reader.Read();

                finalDocument = merger.Merge(finalDocument, document);
            }

#pragma warning disable CS8604 // Possible null reference argument.
            using var writer = new IniWriter(outputFile);
#pragma warning restore CS8604 // Possible null reference argument.
            writer.Write(finalDocument);
            return ExitCodes.OK;
        }

        private bool GatherMergeFiles(IEnumerable<MergeItem> mergeItems, out List<string> mergeFiles)
        {
            mergeFiles = new List<string>();

            foreach (var item in mergeItems)
            {
                if (!File.Exists(item.File))
                {
                    ConsoleHelper.WriteError($"Merge file '{item.File}' not found");
                    return false;
                }

                switch (item.Type)
                {
                    case MergeSource.DirectMerge:
                        mergeFiles.Add(item.File);
                        break;
                    case MergeSource.FileList:
                        {
                            var listDir = Path.GetDirectoryName(item.File);
                            if (listDir == null)
                                throw new InvalidOperationException($"Could not get path information for merge list file '{item.File}");

                            using var reader = new StreamReader(item.File);

                            while(!reader.EndOfStream)
                            {
                                var file = reader.ReadLine();
                                if (string.IsNullOrWhiteSpace(file))
                                    continue;
                                if (file.Trim().StartsWith(';'))
                                    continue;
                                var fullPath = Path.Combine(listDir, file);

                                if (!File.Exists(fullPath))
                                {
                                    ConsoleHelper.WriteError($"Merge file '{fullPath}', which is defined in merge list '{item.File}', was not found");
                                    return false;
                                }


                                mergeFiles.Add(fullPath);
                            }
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected MergeSource");
                }

            }

            return true;
        }
    }
}
