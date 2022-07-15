using MergeIni.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace MergeIni.Tests
{
    [TestClass]
    public class IniWriterTests
    {
        [TestMethod]
        public void CanWriteIni()
        {
            #region Input Data
            var input = new IniDocument();
            var section_ServerSettings = new Section("ServerSettings");
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("MaxTributeDinos", "250"));
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("MaxTributeItems", "150"));
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("TributeItemExpirationSeconds", "31536000"));
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("TributeDinoExpirationSeconds", "31536000"));
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("TributeCharacterExpirationSeconds", "31536000"));
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("DifficultyOffset", "1.0"));
            section_ServerSettings.Values.Add(new KeyValuePair<string, string>("OverrideOfficialDifficulty", "5.0"));
            input.Sections.Add(section_ServerSettings);

            var section_ShooterGameUserSettings = new Section("/Script/ShooterGame.ShooterGameUserSettings");
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("MasterAudioVolume", "1.000000"));
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("MusicAudioVolume", "1.000000"));
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("SFXAudioVolume", "1.000000"));
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("VoiceAudioVolume", "1.000000"));
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("UIScaling", "1.000000"));
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("UIQuickbarScaling", "0.650000"));
            section_ShooterGameUserSettings.Values.Add(new KeyValuePair<string, string>("CameraShakeScale", "0.650000"));
            input.Sections.Add(section_ShooterGameUserSettings);

            var section_ScalabilityGroups = new Section("ScalabilityGroups");
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.ResolutionQuality", "100"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.ViewDistanceQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.AntiAliasingQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.ShadowQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.PostProcessQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.TextureQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.EffectsQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.TrueSkyQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.GroundClutterQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.IBLQuality", "1"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.HeightFieldShadowQuality", "3"));
            section_ScalabilityGroups.Values.Add(new KeyValuePair<string, string>("sg.GroundClutterRadius", "10000"));
            input.Sections.Add(section_ScalabilityGroups);

            var section_GameSession = new Section("/Script/Engine.GameSession");
            section_GameSession.Values.Add(new KeyValuePair<string, string>("MaxPlayers", "8"));
            input.Sections.Add(section_GameSession);

            var section_MessageOfTheDay = new Section("MessageOfTheDay");
            section_MessageOfTheDay.Values.Add(new KeyValuePair<string, string>("Duration", "5"));
            section_MessageOfTheDay.Values.Add(new KeyValuePair<string, string>("Message", "Welcome to the Exilers server"));
            input.Sections.Add(section_MessageOfTheDay);
            #endregion

            using var ms = new MemoryStream();
            using var subject = new IniWriter(ms);
            subject.Write(input);
            var buffer = ms.ToArray();
            using var reader = new StreamReader(new MemoryStream(buffer));
            var actual = reader.ReadToEnd();
            AssertCustom.AreEqual(@"[ServerSettings]
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

", actual);
        }
    }
}