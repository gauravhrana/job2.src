using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayCare.Components
{
    public class RegularExpressionConstants
    {
        //The regular expression used in the preceding code example constrains an input name field to alphabetic characters 
        // (lowercase and uppercase), space characters, the single quotation mark (or apostrophe) for names such 
        // as O'Dell, and the period or dot character. In addition, the field length is constrained to 40 characters.
        public static string ValidateAlphaWithLengthLimit(int minimum, int maximum)
        {
            return @"^[a-zA-Z'.\s]{" + minimum + "," + maximum + "}$";
        }
    }
}