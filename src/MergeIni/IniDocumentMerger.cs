using MergeIni.Model;

namespace MergeIni
{
    class IniDocumentMerger
    {
        public IniDocument Merge(IniDocument left, IniDocument right)
        {
            var result = new IniDocument();

            foreach (var section in left.Sections)
                result.Sections.Add(new Section(section.Name));

            foreach (var section in right.Sections)
            {
                if (result.Sections.Any(i => i.Name.Equals(section.Name)))
                    continue;
                result.Sections.Add(new Section(section.Name));
            }

            foreach (var section in left.Sections)
            {
                var target = result.Sections.First(i => i.Name.Equals(section.Name));
                MergeSection(target, section);
            }

            foreach (var section in right.Sections)
            {
                var target = result.Sections.First(i => i.Name.Equals(section.Name));
                MergeSection(target, section);
            }

            return result;
        }

        private void MergeSection(Section target, Section other)
        {
            var mergeList = new List<KeyValuePair<string, string>>(other.Values);

            var lastListCount = 0;

            while(mergeList.Count != 0)
            {
                if (mergeList.Count == lastListCount)
                    throw new InvalidOperationException("Endless loop protection! mergeList has not reduced in size since the last iteration");

                lastListCount = mergeList.Count;

                var item = mergeList.First();
                mergeList.RemoveAt(0);

                var targetCount = target.Values.Count(i => i.Key.Equals(item.Key));
                var otherCount = other.Values.Count(i => i.Key.Equals(item.Key));

                if (otherCount == 0)
                    throw new InvalidOperationException();

                if(targetCount == 0)
                {
                    // Does not exist in target, so add this and all matching keys to target
                    target.Values.Add(item);

                    for (int i = 0; i < mergeList.Count; i++)
                    {
                        var element = mergeList[i];
                        if (!element.Key.Equals(item.Key))
                            continue;

                        target.Values.Add(element);
                        mergeList.RemoveAt(i);
                        i--;
                    }

                    continue;
                }

                if(targetCount == 1 && otherCount == 1)
                {
                    // Replace the value
                    var index = target.Values.IndexOf(i => i.Key.Equals(item.Key));
                    target.Values[index] = item;
                    continue;
                }

                throw new InvalidOperationException("Cannot reconcile multiple duplicate keys between source and target");
            }
        }
    }
}
