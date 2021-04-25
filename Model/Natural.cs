using System;
using System.Text;

namespace ThePieceEngine.Model
{
    /// <summary>
    /// Represents a Natural number of 512 bits
    /// </summary>
    public struct Natural
    {
        ulong a, b, c, d, e, f, g, h;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(h.ToString());
            sb.Append(" ");
            sb.Append(g.ToString());
            sb.Append(" ");
            sb.Append(f.ToString());
            sb.Append(" ");
            sb.Append(e.ToString());
            sb.Append(" ");
            sb.Append(d.ToString());
            sb.Append(" ");
            sb.Append(c.ToString());
            sb.Append(" ");
            sb.Append(b.ToString());
            sb.Append(" ");
            sb.Append(a.ToString());
            return sb.ToString();
        }

        public static implicit operator Natural(ulong k)
        {
            Natural n = new Natural();
            n.a = k;
            return n;
        }
        
        public static implicit operator Natural(int k)
        {
            if (k < 0)
                throw new Exception("Natural need be positive");
            return (uint)k;
        }
    
        public static Natural operator +(Natural n, Natural m)
        {
            Natural r = new Natural();
            ulong carry = 0;
            
            r.a = unchecked(n.a + m.a);
            carry = r.a < n.a ? 1ul : 0;

            r.b = unchecked(n.b + m.b + carry);
            carry = r.b - carry < n.b ? 1ul : 0;

            r.c = unchecked(n.c + m.c + carry);
            carry = r.c - carry < n.c ? 1ul : 0;

            r.d = unchecked(n.d + m.d + carry);
            carry = r.d - carry < n.d ? 1ul : 0;

            r.e = unchecked(n.e + m.e + carry);
            carry = r.e - carry < n.e ? 1ul : 0;

            r.f = unchecked(n.f + m.f + carry);
            carry = r.f - carry < n.f ? 1ul : 0;

            r.g = unchecked(n.g + m.g + carry);
            carry = r.g - carry < n.g ? 1ul : 0;

            r.h = unchecked(n.h + m.h + carry);
            
            return r;
        }

        public static Natural operator *(Natural n, Natural m)
        {
            Natural r = new Natural();
            ulong opbase = (ulong)uint.MaxValue + 1;
            uint carry = 0;

            Span<uint> sn = stackalloc uint[16]
            {
                (uint)(n.a % opbase),
                (uint)(n.a / opbase),
                (uint)(n.b % opbase),
                (uint)(n.b / opbase),
                (uint)(n.c % opbase),
                (uint)(n.c / opbase),
                (uint)(n.d % opbase),
                (uint)(n.d / opbase),
                (uint)(n.e % opbase),
                (uint)(n.e / opbase),
                (uint)(n.f % opbase),
                (uint)(n.f / opbase),
                (uint)(n.g % opbase),
                (uint)(n.g / opbase),
                (uint)(n.h % opbase),
                (uint)(n.h / opbase)
            };

            Span<uint> sm = stackalloc uint[16]
            {
                (uint)(m.a % opbase),
                (uint)(m.a / opbase),
                (uint)(m.b % opbase),
                (uint)(m.b / opbase),
                (uint)(m.c % opbase),
                (uint)(m.c / opbase),
                (uint)(m.d % opbase),
                (uint)(m.d / opbase),
                (uint)(m.e % opbase),
                (uint)(m.e / opbase),
                (uint)(m.f % opbase),
                (uint)(m.f / opbase),
                (uint)(m.g % opbase),
                (uint)(m.g / opbase),
                (uint)(m.h % opbase),
                (uint)(m.h / opbase)
            };

            Span<uint> sr = stackalloc uint[32];

            for (int i = 0; i < 16; i++)
            {
                carry = 0;
                for (int j = 0; j < 16; j++)
                {
                    sr[i + j] += carry + sn[i] * sm[j];
                    carry = (uint)(sr[i + j] / opbase);
                    sr[i + j] = (uint)(sr[i + j] % opbase);
                }
                sr[i + 16] = carry;
            }

            r.a = sr[0] + sr[1] * opbase;
            r.b = sr[2] + sr[3] * opbase;
            r.c = sr[4] + sr[5] * opbase;
            r.d = sr[6] + sr[7] * opbase;
            r.e = sr[8] + sr[9] * opbase;
            r.f = sr[10] + sr[11] * opbase;
            r.g = sr[12] + sr[13] * opbase;
            r.h = sr[14] + sr[15] * opbase;

            return r;
        }
    }
}