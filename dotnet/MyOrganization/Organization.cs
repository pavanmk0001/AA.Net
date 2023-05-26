using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        public int identity = 10;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            bool isTitleFound = false;
            //if(root.GetDirectReports().Any(c=>c.GetDirectReports().Any(c => c.GetDirectReports().Contains(new Position(title)))))
            //{
            //    isTitleFound = true;
            //}
            if (root.GetTitle() == title)
            {
                isTitleFound = true;
            }
            else
            {
                foreach (var item in root.GetDirectReports())
                {

                    if (item.GetTitle() == title)
                    {
                        isTitleFound = true;
                    }
                    if (item.GetDirectReports().Any(c => c.GetTitle() == title))
                    {
                        isTitleFound = true;
                    }
                    if (item.GetDirectReports().Any(c => c.GetDirectReports().Any(c => c.GetTitle() == title)))
                    {
                        isTitleFound = true;
                    }
                }
            }
            if (!isTitleFound)
            {
                return null;
            }
            Employee e = new Employee(identity, person);
            Position p = new(title, e);
            identity++;

            root.AddDirectReport(p);
      
            return p;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
