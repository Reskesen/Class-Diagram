using ClassDiagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.Model
{

    public class Line : NotifyBase
    {

        private ClassShape from;
        public ClassShape From { get { return from; } set { from = value; NotifyPropertyChanged(); } }

        private ClassShape to;
        public ClassShape To { get { return to; } set { to = value; NotifyPropertyChanged(); } }
    }
}
