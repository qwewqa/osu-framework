﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Framework.Graphics.Visualisation
{
    internal class TitleBar : CompositeDrawable
    {
        private readonly Drawable movableTarget;

        public TitleBar(string title, Drawable movableTarget)
        {
            this.movableTarget = movableTarget;

            RelativeSizeAxes = Axes.X;
            Size = new Vector2(1, 25);

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.BlueViolet,
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = title,
                    Alpha = 0.8f,
                },
            };
        }

        protected override bool OnDragStart(DragStartEvent e) => true;

        protected override bool OnDrag(DragEvent e)
        {
            movableTarget.Position += e.Delta;
            return base.OnDrag(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e) => true;
    }
}
