﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aurora.EffectsEngine.Animations
{
    public class AnimationLines : AnimationFrame
    {
        private List<AnimationLine> _lines;

        public AnimationLines(AnimationLine[] lines)
        {
            _lines = new List<AnimationLine>(lines);
        }

        public override void Draw(Graphics g)
        {
            foreach( AnimationLine line in _lines)
                line.Draw(g);
        }

        public override AnimationFrame BlendWith(AnimationFrame otherAnim, double amount)
        {
            if (!(otherAnim is AnimationLines))
            {
                throw new FormatException("Cannot blend with another type");
            }

            if(this._lines.Count != (otherAnim as AnimationLines)._lines.Count)
            {
                throw new NotImplementedException();
            }

            List<AnimationLine> newlines = new List<AnimationLine>();

            for (int line_i = 0; line_i < this._lines.Count; line_i++)
                newlines.Add(this._lines[line_i].BlendWith((otherAnim as AnimationLines)._lines[line_i], amount) as AnimationLine);

            return new AnimationLines(newlines.ToArray());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AnimationLines)obj);
        }

        public bool Equals(AnimationLines p)
        {
            return _lines.Equals(p._lines);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + _lines.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "AnimationLines [ Lines: " + _lines.Count + " ]";
        }
    }
}
