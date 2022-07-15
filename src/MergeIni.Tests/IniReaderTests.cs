using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MergeIni.Tests
{
    [TestClass]
    public class IniReaderTests
    {
        [TestMethod]
        public void CanReadIni()
        {
            using var stream = @"[ServerSettings]
MaxTributeDinos=250
MaxTributeItems=150
TributeItemExpirationSeconds=31536000
TributeDinoExpirationSeconds=31536000
TributeCharacterExpirationSeconds=31536000
DifficultyOffset=1.0
OverrideOfficialDifficulty=5.0

[/Script/ShooterGame.ShooterGameUserSettings]
MasterAudioVolume=1.000000
MusicAudioVolume=1.000000
SFXAudioVolume=1.000000
VoiceAudioVolume=1.000000
UIScaling=1.000000
UIQuickbarScaling=0.650000
CameraShakeScale=0.650000

[ScalabilityGroups]
sg.ResolutionQuality=100
sg.ViewDistanceQuality=3
sg.AntiAliasingQuality=3
sg.ShadowQuality=3
sg.PostProcessQuality=3
sg.TextureQuality=3
sg.EffectsQuality=3
sg.TrueSkyQuality=3
sg.GroundClutterQuality=3
sg.IBLQuality=1
sg.HeightFieldShadowQuality=3
sg.GroundClutterRadius=10000

[/Script/Engine.GameSession]
MaxPlayers=8

[MessageOfTheDay]
Duration=5
Message=Welcome to the Exilers server
".ToStream();

            using var subject = new IniReader(stream);
            var actual = subject.Read();

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Sections);
            Assert.AreEqual(5, actual.Sections.Count);

            // SCOPE : section[0]
            {
                var section = actual.Sections[0];
                Assert.IsNotNull(section);
                Assert.AreEqual("ServerSettings", section.Name);
                Assert.IsNotNull(section.Values);
                Assert.AreEqual(7, section.Values.Count);

                // SCOPE : value[0]
                {
                    var value = section.Values[0];
                    Assert.AreEqual("MaxTributeDinos", value.Key);
                    Assert.AreEqual("250", value.Value);
                }

                // SCOPE : value[6]
                {
                    var value = section.Values[6];
                    Assert.AreEqual("OverrideOfficialDifficulty", value.Key);
                    Assert.AreEqual("5.0", value.Value);
                }

            }

            // SCOPE : section[2]
            {
                var section = actual.Sections[2];
                Assert.IsNotNull(section);
                Assert.AreEqual("ScalabilityGroups", section.Name);
                Assert.IsNotNull(section.Values);
                Assert.AreEqual(12, section.Values.Count);

                // SCOPE : value[0]
                {
                    var value = section.Values[0];
                    Assert.AreEqual("sg.ResolutionQuality", value.Key);
                    Assert.AreEqual("100", value.Value);
                }

                // SCOPE : value[11]
                {
                    var value = section.Values[11];
                    Assert.AreEqual("sg.GroundClutterRadius", value.Key);
                    Assert.AreEqual("10000", value.Value);
                }

            }

            // SCOPE : section[4]
            {
                var section = actual.Sections[4];
                Assert.IsNotNull(section);
                Assert.AreEqual("MessageOfTheDay", section.Name);
                Assert.IsNotNull(section.Values);
                Assert.AreEqual(2, section.Values.Count);

                // SCOPE : value[0]
                {
                    var value = section.Values[0];
                    Assert.AreEqual("Duration", value.Key);
                    Assert.AreEqual("5", value.Value);
                }

                // SCOPE : value[1]
                {
                    var value = section.Values[1];
                    Assert.AreEqual("Message", value.Key);
                    Assert.AreEqual("Welcome to the Exilers server", value.Value);
                }

            }
        }
    }
}