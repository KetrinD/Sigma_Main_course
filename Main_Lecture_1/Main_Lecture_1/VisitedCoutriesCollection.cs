using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_1
{
    class VisitedCoutriesCollection <TmyType>: IEnumerable<TmyType>, IEnumerator<TmyType>
    {
        private List<Coutry> coutries;
        private int current = 0;

        public void AddCountry(Coutry country)
        {
            if (coutries == null)
            {
                coutries = new List<Coutry>() { country };
            }
            else
            {
                coutries.Add(country);
            }
        }

        public void RemoveCountry(Coutry country)
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

        public object Current
        {
            get { return coutries[current];}
        }

        TmyType IEnumerator<TmyType>.Current => throw new NotImplementedException();

        public IEnumerator GetEnumerator()
        {
            current = -1;
            return this;
        }

        public bool MoveNext()
        {
            if (coutries.Count > current)
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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.coutries = null;
        }
    }



}
