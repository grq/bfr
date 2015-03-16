using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIMath.BooleanAlgebra
{
    public class OmegaComplexityProcessor
    {
        #region PublicFields

        public int Omega0
        {
            get
            {
                if (!this._omega0.HasValue)
                {
                    this.ProcessOmega0Omega1();
                }
                return this._omega0.Value;
            }
        }

        public List<int> Omega1
        {
            get
            {
                if (this._omega1 == null)
                {
                    this.ProcessOmega0Omega1();
                }
                return this._omega1;
            }
        }

        public int AbsoluteOmega1
        {
            get
            {
                if (!this._absoluteOmega1.HasValue)
                {
                    this.ProcessAbsoluteOmega();
                }
                return this._absoluteOmega1.Value;
            }
        }

        #endregion

        #region PrivateFields

        private int? _omega0;

        private List<int> _omega1;

        private int? _absoluteOmega1;

        private string _sequence;

        #endregion

        #region Constructors

        public OmegaComplexityProcessor(string sequence)
        {
            this._sequence = sequence;
        }

        #endregion

        #region PrivateMethods

        private void ProcessOmega0Omega1()
        {
            this._omega1 = new List<int>();
            int m = 1; // Recurrent trim
            bool mChanged; // Flag of reccurent trim changed
            do
            {
                mChanged = false; // Set flag as False
                List<string[]> recurrentForms = new List<string[]>(); // List of recurrent forms in current recurrent trim
                for (int i = 0; i < this._sequence.Length - m; i++) // For all possibly recurrent forms in this trim
                {
                    string[] reccurentForm = new string[2] { this._sequence.Substring(i, m), this._sequence.Substring(i + m, 1) }; // Creatin' recurrent form in current position
                    string[] comparer = recurrentForms.FirstOrDefault(p => p[0] == reccurentForm[0]); // Searchin' comparer for created recurrent form
                    if (comparer != null && comparer[1] != reccurentForm[1]) // If comparer founded and it isn't match current form
                    {
                        m++; // Increase recurrent trim
                        mChanged = true; // Set flag as True
                        this._omega1.Add(recurrentForms.First()[0].Length + recurrentForms.Count);
                        break; // Break the cicle
                    }
                    else
                    {
                        recurrentForms.Add(new string[2] { this._sequence.Substring(i, m), this._sequence.Substring(i + m, 1) }); // Add created recurrent form to list of all forms
                    }
                }
            }
            while (mChanged); // While trim isn't changed
            this._omega0 = m;
            this._omega1.Add(this._sequence.Length);
        }

        private void ProcessAbsoluteOmega()
        {
            if (this._omega1 == null)
            {
                this.ProcessOmega0Omega1();
            }
            this._absoluteOmega1 = 0;
            for (int i = 0; i < this._omega1.Count; i++)
            {
                this._absoluteOmega1 += (i + 1) * (_omega1[i]);
            }
        }

        #endregion
    }
}
