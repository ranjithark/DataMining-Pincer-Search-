using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PincerSearch
{
    class Set
    {
       public List<Element> elements = new List<Element>();
        public Boolean isEmpty=false;
        

        public Set()
        {
             //nothing   
        }
        public Set(int id)
        {
            if (id == 0)
                isEmpty = true;
           
        }
        public void WithElement(Element id)
        {
            
                this.elements.Remove(id);
            
            
            

        }

        public Set(List<Element> tmpElemList)
        {
            // TODO: Complete member initialization
            this.elements = tmpElemList;
            if (tmpElemList.Count == 0)
            {
                isEmpty = true;
            }
            else
            {
                isEmpty = false;
            }
        }
    }
}
