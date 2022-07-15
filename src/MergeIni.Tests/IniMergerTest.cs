using MergeIni.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MergeIni.Tests
{
    [TestClass]
    public class IniMergerTest
    {
        [TestMethod]
        [DeploymentItem(@"TestData\SimpleLeft.txt", "TestData")]
        [DeploymentItem(@"TestData\SimpleRight.txt", "TestData")]
        public void CanMergeSimple()
        {
            var left = new IniReader(@"TestData\SimpleLeft.txt").Read();
            var right = new IniReader(@"TestData\SimpleRight.txt").Read();

            var subject = new IniDocumentMerger();
            var actual = subject.Merge(left, right);

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Sections);
            Assert.AreEqual(3, actual.Sections.Count);

            // SCOPE : section[0]
            {
                var section = actual.Sections[0];
                Assert.IsNotNull(section);
                Assert.AreEqual("UniqueLeftSection", section.Name);
                Assert.IsNotNull(section.Values);
                Assert.AreEqual(2, section.Values.Count);

                // SCOPE : value[0]
                {
                    var value = section.Values[0];
                    Assert.AreEqual("KeyOne", value.Key);
                    Assert.AreEqual("1", value.Value);
                }

                // SCOPE : value[1]
                {
                    var value = section.Values[1];
                    Assert.AreEqual("KeyTwo", value.Key);
                    Assert.AreEqual("2", value.Value);
                }

            }

            // SCOPE : section[1]
            {
                var section = actual.Sections[1];
                Assert.IsNotNull(section);
                Assert.AreEqual("CommonSection", section.Name);
                Assert.IsNotNull(section.Values);
                Assert.AreEqual(3, section.Values.Count);

                // SCOPE : value[0]
                {
                    var value = section.Values[0];
                    Assert.AreEqual("LeftOnlyKey", value.Key);
                    Assert.AreEqual("left", value.Value);
                }

                // SCOPE : value[1]
                {
                    var value = section.Values[1];
                    Assert.AreEqual("SharedKey", value.Key);
                    Assert.AreEqual("right", value.Value);
                }

                // SCOPE : value[2]
                {
                    var value = section.Values[2];
                    Assert.AreEqual("RightOnlyKey", value.Key);
                    Assert.AreEqual("right", value.Value);
                }
            }

            // SCOPE : section[2]
            {
                var section = actual.Sections[2];
                Assert.IsNotNull(section);
                Assert.AreEqual("UniqueRightSection", section.Name);
                Assert.IsNotNull(section.Values);
                Assert.AreEqual(2, section.Values.Count);

                // SCOPE : value[0]
                {
                    var value = section.Values[0];
                    Assert.AreEqual("KeyOne", value.Key);
                    Assert.AreEqual("1", value.Value);
                }

                // SCOPE : value[1]
                {
                    var value = section.Values[1];
                    Assert.AreEqual("KeyTwo", value.Key);
                    Assert.AreEqual("2", value.Value);
                }
            }
        }
    }

}