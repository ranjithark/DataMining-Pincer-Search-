using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PincerSearch
{
    class Element
    {
        public List<int> elemList = new List<int>();
        public int count = 0;
        public Element()
        {
        }
       
        public Element(List<int> list)
        {

            this.elemList = list;  
        }
    }
}
