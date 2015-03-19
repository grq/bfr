using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class ReccurentForm
    {
        private string from;

        private string to;

        public ReccurentForm(string from, string to)
        {
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException("from", "Argument should be defined");
            }
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("to", "Argument should be defined");
            }
            else if (to.Length != 1)
            {
                throw new ArgumentException("to", "Length should be exactly '1'");
            }
            this.from = from;
            this.to = to;
        }

        public ReccurentForm(string sequence, int start, int length)
        {
            this.from = sequence.Substring(start, length);
            this.to = sequence.Substring(start + length, 1);
        }

        public override bool Equals(object obj)
        {
            ReccurentForm comparer = (ReccurentForm)obj;
            return this.from == comparer.from && this.to != comparer.to;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", this.from, this.to);
        }
    }
}
