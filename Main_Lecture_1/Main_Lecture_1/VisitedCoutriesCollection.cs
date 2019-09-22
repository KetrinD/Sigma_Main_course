using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_1
{
    class VisitedCoutriesCollection<TmyType> : IEnumerable<TmyType>, IEnumerator<TmyType>
    {
        private List<TmyType> coutries;
        private int current = -1;
        private TmyType _current;

        public void AddCountry(TmyType country)
        {
            if (coutries == null)
            {
                coutries = new List<TmyType> () { country };
                return;
            }
            else
            {
                coutries.Add(country);
            }
        }

        public void RemoveCountry(TmyType country)
        {
            if (coutries == null)
            {
                Console.WriteLine("There is no country in your List");
            }
            else
            {
                coutries.Remove(country);
            }
        }


        public void RemoveCountryAtPosition(int a)
        {
            if (coutries == null)
            {
                Console.WriteLine("There is no country in your List");
            }
            else
            {
                coutries.RemoveAt(a);
            }
        }

        public object Current
        {
            get { return coutries[current]; }
        }

        TmyType IEnumerator<TmyType>.Current => _current;

        public IEnumerator GetEnumerator()
        {
            current = -1;
            return this;
        }

        public bool MoveNext()
        {
            if (coutries == null)
            {
                return false;
            }
            if (coutries.Count - 1 > current)
            {
                current++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        IEnumerator<TmyType> IEnumerable<TmyType>.GetEnumerator()
        {
            current = -1;
            return this;
        }

        public void Dispose()
        {
            //this.coutries = null;
        }
    }
}


