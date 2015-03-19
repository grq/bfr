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

        public IEnumerable<int> Omega1
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
                    this.ProcessAbsoluteOmega1();
                }
                return this._absoluteOmega1.Value;
            }
        }

        public IEnumerable<int> Omega2
        {
            get
            {
                if (this._omega2 == null)
                {
                    this.ProcessOmega2Omega3();
                }
                return this._omega2;
            }
        }

        public int AbsoluteOmega2
        {
            get
            {
                if (!this._absoluteOmega2.HasValue)
                {
                    this.ProcessAbsoluteOmega2();
                }
                return this._absoluteOmega2.Value;
            }
        }

        public IEnumerable<IEnumerable<int>> Omega3
        {
            get
            {
                if (this._omega3 == null)
                {
                    this.ProcessOmega2Omega3();
                }
                return this._omega3;
            }
        }

        public int AbsoluteOmega3
        {
            get
            {
                if (!this._absoluteOmega3.HasValue)
                {
                    this.ProcessAbsoluteOmega3();
                }
                return this._absoluteOmega3.Value;
            }
        }

        #endregion

        #region PrivateFields

        private string _sequence;

        private int? _omega0;

        private List<int> _omega1;

        private int? _absoluteOmega1;

        private List<int> _omega2;

        private int? _absoluteOmega2;

        private List<List<int>> _omega3;

        private int? _absoluteOmega3;

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
            this._omega0 = 1;
            this._omega1 = new List<int>();
            while (this._omega1.LastOrDefault() != this._sequence.Length)
            {
                List<ReccurentForm> forms = new List<ReccurentForm>();
                for (int start = 0; start < this._sequence.Length - this._omega0; start++)
                {
                    ReccurentForm form = new ReccurentForm(this._sequence, start, this._omega0.Value);
                    if (forms.Contains(form))
                    {
                        this._omega1.Add(start + this._omega0.Value);
                        break;
                    }
                    forms.Add(form);
                }
                if (this._omega1.Count != this._omega0)
                {
                    this._omega1.Add(this._sequence.Length);
                }
                else
                {
                    this._omega0++;
                }
            }
        }

        private void ProcessAbsoluteOmega1()
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

        private void ProcessOmega2Omega3()
        {
            this._omega2 = new List<int>();
            this._omega3 = new List<List<int>>();
            while (this._omega2.LastOrDefault() != 1)
            {
                int length = this._omega2.Count + 1;
                int changes = 1;
                int lastChange = 0;
                List<int> omega3Changes = new List<int>(); 
                List<ReccurentForm> currentForms = new List<ReccurentForm>();
                for (int start = 0; start < this._sequence.Length - length; start++)
                {
                    ReccurentForm form = new ReccurentForm(this._sequence, start, length);
                    if (currentForms.Contains(form))
                    {
                        changes++;
                        omega3Changes.Add(start - lastChange);
                        lastChange = start;
                        currentForms = new List<ReccurentForm>();
                    }
                    currentForms.Add(form);
                }
                omega3Changes.Add(_sequence.Length - length - lastChange);
                this._omega2.Add(changes);
                this._omega3.Add(omega3Changes);
            }
        }

        private void ProcessAbsoluteOmega2()
        {
            if (this._omega2 == null)
            {
                this.ProcessOmega2Omega3();
            }
            this._absoluteOmega2 = 0;
            for (int i = 0; i < this._omega2.Count; i++)
            {
                this._absoluteOmega2 += (i + 1) * (_omega2[i]);
            }
        }

        private void ProcessAbsoluteOmega3()
        {
            if (this._omega3 == null)
            {
                this.ProcessOmega2Omega3();
            }
            this._absoluteOmega3 = 0;
            for (int i = 0; i < this._omega3.Count; i++)
            {
                int levelWeight = 0;
                for (int j = 0; j < this._omega3[i].Count; j++)
                {
                    levelWeight += (j + 1) * this._omega3[i][j];
                }
                this._absoluteOmega3 += (i + 1) * levelWeight;
            }
        }

        #endregion
    }
}
