﻿using OpenUtau.Api;
using OpenUtau.Plugin.Builtin;
using Xunit;
using Xunit.Abstractions;

namespace OpenUtau.Plugins {
    public class EnArpaPlusTest : PhonemizerTestBase {
        public EnArpaPlusTest(ITestOutputHelper output) : base(output) { }

        protected override Phonemizer CreatePhonemizer() {
            return new ArpasingPlusPhonemizer();
        }

        [Theory]
        [InlineData("en_arpa-plus",
            new string[] { "good", "morning" },
            new string[] { "C4", "C4" },
            new string[] { "", "" },
            new string[] { "- g_3", "g uh_3", "uh d_3", "d m_3", "m ao_3", "ao r_3", "r n_3", "n ih_3", "ih ng_3", "ng -_3" })]
        [InlineData("en_arpa-plus",
            new string[] { "good", "morning" },
            new string[] { "A3", "F4" },
            new string[] { "", "" },
            new string[] { "- g_3", "g uh_3", "uh d_3", "d m_3", "m ao", "ao r", "r n", "n ih", "ih ng", "ng -" })]
        public void PhonemizeTest(string singerName, string[] lyrics, string[] tones, string[] colors, string[] aliases) {
            RunPhonemizeTest(singerName, lyrics, RepeatString(lyrics.Length, ""), tones, colors, aliases);
        }

        [Fact]
        public void ColorTest() {
            RunPhonemizeTest("en_arpa-plus", new NoteParams[] {
                new NoteParams {
                    lyric = "hi",
                    hint = "",
                    tone = "C4",
                    phonemes = new PhonemeParams[] {
                        new PhonemeParams {
                            alt = 0,
                            shift = 0,
                            color = "",
                        },
                        new PhonemeParams {
                            alt = 0,
                            shift = 0,
                            color = "Whisper",
                        },
                        new PhonemeParams {
                            alt = 0,
                            shift = 0,
                            color = "",
                        }
                    }
                }
            }, new string[] { "- hh_3", "hh ay_W", "ay -_3" });
        }

        [Theory]
        [InlineData("read", "", new string[] { "- r_3", "r eh_3", "eh d_3", "d -_3" })]
        [InlineData("read", "r iy d", new string[] { "- r_3", "r iy_3", "iy d_3", "d -_3" })]

        [InlineData("asdfjkl", "r iy d", new string[] { "- r_3", "r iy_3", "iy d_3", "d -_3" })]
        [InlineData("", "r iy d", new string[] { "- r_3", "r iy_3", "iy d_3", "d -_3" })]
        public void HintTest(string lyric, string hint, string[] aliases) {
            RunPhonemizeTest("en_arpa-plus", new NoteParams[] { new NoteParams { lyric = lyric, hint = hint, tone = "C4", phonemes = SamePhonemeParams(4, 0, 0, "")} }, aliases);
        }
    }
}
