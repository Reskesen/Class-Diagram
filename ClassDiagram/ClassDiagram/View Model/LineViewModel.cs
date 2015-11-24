using ClassDiagram.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagram.View_Model
{
    public class LineViewModel : BaseViewModel
    {
        public Line Line { get; set; }

        public ShapeViewModel from;
        public ShapeViewModel From { get { return from; } set { from = value; RaisePropertyChanged(); } }

        public ShapeViewModel to;
        public ShapeViewModel To { get { return to; } set { to = value; RaisePropertyChanged(); } }

        public LineViewModel(Line _line) : base()
        {
            Line = _line;
        }
    }
}
