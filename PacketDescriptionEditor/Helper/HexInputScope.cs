using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacketDescriptionEditor.Helper
{
    public class HexInputScope : InputScope
    {
        public HexInputScope()
        {
            base.RegularExpression = "^[0-9A-F]+$";
        }
    }
}
