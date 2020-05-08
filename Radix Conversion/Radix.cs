using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Radix_Conversion
{
    class Radix
    {
        private char[] table;
        private int size;
        private Dictionary<char, int> n_deci;

        public Radix()
        {
            ChangeTable(new char[62]
            {
                '0','1','2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','i','j',
                'k','l','m','n','o','p','q','r','s','t',
                'u','v','w','x','y','z','A','B','C','D',
                'E','F','G','H','I','J','K','L','M','N',
                'O','P','Q','R','S','T','U','V','W','X',
                'Y','Z'
            });
        }

        public Radix(char[] vs)
        {
            ChangeTable(vs);
        }

        /// <summary>
        /// 基数テーブルの変更
        /// </summary>
        /// <param name="vs"> 基数テーブル </param>
        public void ChangeTable(char[] vs)
        {
            table = vs;
            size = vs.Length;
            n_deci = new Dictionary<char, int>(size);
            for(int i = 0; i < size; i++)
            {
                n_deci.Add(vs[i], i);
            }
        }

        /// <summary>
        /// n 進数で表された文字列 s を、 m 進数に変換する
        /// </summary>
        /// <param name="s"> 変換前の文字 </param>
        /// <param name="a"> 変換前の基数 </param>
        /// <param name="b"> 変換後の基数 </param>
        /// <returns> 変換後の文字 </returns>
        public string Conversion(string s,int n,int m)
        {
            Debug.Print($"string s={s}\nint n={n}\nint m={m}\n");
            if (!Check(s, n)) return "";

            string t = "";
            BigInteger bi;
            if (n == 10)
            {
                string tmp="";
                for(int i = 0; i < s.Length; i++)
                {
                    tmp += n_deci[s[i]];
                }
                bi = BigInteger.Parse(tmp);   
            }
            else
            {
                bi = new BigInteger(0);
                BigInteger nn = new BigInteger(n);
                for(int i = 0; i < s.Length; i++)
                {
                    bi += n_deci[s[i]] * Pow(nn,s.Length-i-1);
                }
            }
            Debug.Print($"BigInteger bi={bi}\n");

            do
            {
                t += table[(int)(bi % m)];
                bi /= m;
            } while (bi != 0);

            string res = new string(t.Reverse().ToArray());
            return res;
        }

        /// <summary>
        ///  b の n 乗の値を返します
        /// </summary>
        /// <param name="b"></param>
        /// <param name="n"></param>
        /// <returns> b の n 乗の値 </returns>
        private BigInteger Pow(BigInteger b,int n)
        {
            if (n == 0) return 1;
            if (n % 2 == 1) return b * Pow(b, n - 1);
            return Pow(b * b, n / 2);
        }

        /// <summary>
        /// 文字列 s が n 進数で表せるか調べる
        /// </summary>
        /// <param name="s"> 文字列 </param>
        /// <param name="n"> 基数 </param>
        /// <returns> 表現可能ならば true </returns>
        private bool Check(string s,int n)
        {
            bool flag = true;
            for(int i = 0; i < s.Length; i++)
            {
                flag &= Contain(s[i], n);
                if (!flag) break;
            }
            return flag;
        }

        public bool Contain(char c,int n) { return n_deci.ContainsKey(c) && n_deci[c] < n; }
        public int GetTableSize() { return size; }
        public char[] GetTable() { return table; }
    }
}
