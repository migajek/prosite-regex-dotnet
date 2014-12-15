using System;
using System.Collections.Generic;
using System.Linq;

namespace PROSITE.Core
{    
    // TODO: http://www.bioinformatics.org/sms/iupac.html ?
    public class AminoAcidHelper
    {
        public static IList<char> AminoAcidLetters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToList();
    }
}