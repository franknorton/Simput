using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simput
{
    public static class ControlCodeParser
    {
        public static bool TryParse(char character, out ControlCode code)
        {
            try
            {
                code = Parse(character);
                return true;
            }
            catch
            {
                code = ControlCode.Ctrl_A;
                return false;
            }
        }

        public static ControlCode Parse(char character)
        {
            if(character == 0x01) { return ControlCode.Ctrl_A; }
            if(character == 0x02) { return ControlCode.Ctrl_B; }
            if(character == 0x03) { return ControlCode.Ctrl_C; }
            if(character == 0x04) { return ControlCode.Ctrl_D; }
            if(character == 0x05) { return ControlCode.Ctrl_E; }
            if(character == 0x06) { return ControlCode.Ctrl_F; }
            if(character == 0x07) { return ControlCode.Ctrl_G; }
            if(character == 0x08) { return ControlCode.Ctrl_H; }
            if(character == 0x09) { return ControlCode.Ctrl_I; }
            if(character == 0x0A) { return ControlCode.Ctrl_J; }
            if(character == 0x0B) { return ControlCode.Ctrl_K; }
            if(character == 0x0C) { return ControlCode.Ctrl_L; }
            if(character == 0x0D) { return ControlCode.Ctrl_M; }
            if(character == 0x0E) { return ControlCode.Ctrl_N; }
            if(character == 0x0F) { return ControlCode.Ctrl_O; }
            if(character == 0x10) { return ControlCode.Ctrl_P; }
            if(character == 0x11) { return ControlCode.Ctrl_Q; }
            if(character == 0x12) { return ControlCode.Ctrl_R; }
            if(character == 0x13) { return ControlCode.Ctrl_S; }
            if(character == 0x14) { return ControlCode.Ctrl_T; }
            if(character == 0x15) { return ControlCode.Ctrl_U; }
            if(character == 0x16) { return ControlCode.Ctrl_V; }
            if(character == 0x17) { return ControlCode.Ctrl_W; }
            if(character == 0x18) { return ControlCode.Ctrl_X; }
            if(character == 0x19) { return ControlCode.Ctrl_Y; }
            if(character == 0x1A) { return ControlCode.Ctrl_Z; }
            if(character == 0x1B) { return ControlCode.Ctrl_LeftBrace; }
            if(character == 0x1C) { return ControlCode.Ctrl_Backslash; }
            if(character == 0x1D) { return ControlCode.Ctrl_RightBrace; }
            if(character == 0x1E) { return ControlCode.Ctrl_Caret; }
            if(character == 0x1F) { return ControlCode.Ctrl_Underscore; }
            throw new Exception("Could not parse control character from char: " + character);

        }
    }
}
