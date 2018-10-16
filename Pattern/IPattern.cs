using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Quellatalo.Nin.TheEyes.Pattern
{
    public interface IPattern
    {
        Match GetMax(Bitmap image);
        List<Match> GetMatches(Bitmap image);
    }
}
