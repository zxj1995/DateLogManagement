using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileContextSearch.ResearchList
{
    class ResearchItem : SubResearchItem
    {
        public ResearchItem()
        {
             SubList = new List<SubResearchItem>();
        }
       public string Name
        {
            get;
            set;
        }
        public List<SubResearchItem> SubList
        {
            get;
            set;
        }
    }
}
