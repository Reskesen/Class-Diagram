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

        public ClassShape from;
        public ClassShape From { get { return from; } set { from = value; NotifyPropertyChanged(); } }

        public ClassShape to;
        public ClassShape To { get { return to; } set { to = value; NotifyPropertyChanged(); } }
    }
}
