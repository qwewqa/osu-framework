﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Testing;

namespace osu.Framework.Desktop.Tests.Visual
{
    [TestFixture]
    internal class TestCaseTextFlow : TestCase
    {
        public override string Description => @"Test word-wrapping and paragraphs";

        public TestCaseTextFlow()
        {
            FillFlowContainer flow;

            Children = new Drawable[]
            {
                new ScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        flow = new FillFlowContainer
                        {
                            Anchor = Anchor.TopLeft,
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Direction = FillDirection.Vertical,
                        }
                    }
                }
            };

            FillFlowContainer paragraphContainer;
            TextFlowContainer textFlowContainer;
            flow.Add(paragraphContainer = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Width = 0.5f,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    textFlowContainer = new TextFlowContainer
                    {
                        FirstLineIndent = 5,
                        ContentIndent = 10,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    }
                }
            });

            textFlowContainer.AddText("the considerably swift vermilion reynard bounds above the slothful mahogany hound.", t => t.Colour = Color4.Yellow);
            textFlowContainer.AddText("\nTHE ", t => t.Colour = Color4.Red);
            textFlowContainer.AddText("CONSIDERABLY", t => t.Colour = Color4.Pink);
            textFlowContainer.AddText(" SWIFT VERMILION REYNARD BOUNDS ABOVE THE SLOTHFUL MAHOGANY HOUND!!", t => t.Colour = Color4.Red);
            textFlowContainer.AddText("\n\n0123456789!@#$%^&*()_-+-[]{}.,<>;'\\\\", t => t.Colour = Color4.Blue);
            var textSize = 48f;
            textFlowContainer.AddParagraph("Multiple Text Sizes", t =>
            {
                t.TextSize = textSize;
                textSize -= 12f;
            });
            textFlowContainer.AddText("\nI'm a paragraph\nnewlines are cool", t => t.Colour = Color4.Beige);
            textFlowContainer.AddText(" (and so are inline styles!)", t => t.Colour = Color4.Yellow);
            textFlowContainer.AddParagraph("There's 2 line breaks\n\ninside this paragraph!", t => t.Colour = Color4.GreenYellow);
            textFlowContainer.AddParagraph("Make\nTextFlowContainer\ngreat\nagain!", t => t.Colour = Color4.Red);

            paragraphContainer.Add(new TextFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Text =
@"osu! is a freeware rhythm game developed by Dean ""peppy"" Herbert, originally for Microsoft Windows. The game has also been ported to macOS, iOS, Android, and Windows Phone.[1] Its game play is based on commercial titles including Osu! Tatakae! Ouendan, Elite Beat Agents, Taiko no Tatsujin, beatmania IIDX, O2Jam, and DJMax.

osu! is written in C# on the .NET Framework. On August 28, 2016, osu!'s source code was open-sourced under the MIT License. [2] [3] Dubbed as ""Lazer"", the project aims to make osu! available to more platforms and transparent. [4] The community includes over 9 million registered users, with a total of 6 billion ranked plays.[5]"
            });

            paragraphContainer.Add(new TestCaseCustomText
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Placeholders = new Drawable[]
                {
                    new LineBaseBox
                    {
                        Colour = Color4.Purple,
                        LineBaseHeight = 25f,
                        Size = new Vector2(25, 25)
                    }.WithEffect(new OutlineEffect
                    {
                        Strength = 20f,
                        PadExtent = true,
                        BlurSigma = new Vector2(5f),
                        Colour = Color4.White
                    })
                },
                Text = "Test icons [RedBox] interleaved\n[GreenBox] with other [0] text, also [[0]] escaping stuff is possible."
            });

            AddStep(@"resize paragraph 1", () => { paragraphContainer.Width = 1f; });
            AddStep(@"resize paragraph 2", () => { paragraphContainer.Width = 0.6f; });
            AddStep(@"header inset", () => { textFlowContainer.FirstLineIndent += 2; });
            AddStep(@"body inset", () => { textFlowContainer.ContentIndent += 4; });
            AddToggleStep(@"Zero paragraph spacing", state => textFlowContainer.ParagraphSpacing = state ? 0 : 0.5f);
            AddToggleStep(@"Non-zero line spacing", state => textFlowContainer.LineSpacing = state ? 1 : 0);
        }

        private class LineBaseBox : Box, IHasLineBaseHeight
        {
            public float LineBaseHeight { get; set; }
        }

        private class TestCaseCustomText : CustomizableTextContainer
        {
            public TestCaseCustomText()
            {
                AddIconFactory("RedBox", makeRedBox);
                AddIconFactory("GreenBox", makeGreenBox);
            }

            private Drawable makeGreenBox() => new LineBaseBox
            {
                Colour = Color4.Green,
                LineBaseHeight = 25f,
                Size = new Vector2(25, 20)
            };

            private Drawable makeRedBox() => new LineBaseBox
            {
                Colour = Color4.Red,
                LineBaseHeight = 10f,
                Size = new Vector2(25, 25)
            };
        }
    }
}
