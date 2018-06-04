﻿using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace protect_inf_LR1

{
    public class MD5
    {
        byte[] input;
        uint[] X = new uint[16];
        public uint A, B, C, D;
        
        protected readonly static uint[] T = new uint[64]
            {   0xd76aa478,0xe8c7b756,0x242070db,0xc1bdceee,
                0xf57c0faf,0x4787c62a,0xa8304613,0xfd469501,
                0x698098d8,0x8b44f7af,0xffff5bb1,0x895cd7be,
                0x6b901122,0xfd987193,0xa679438e,0x49b40821,
                0xf61e2562,0xc040b340,0x265e5a51,0xe9b6c7aa,
                0xd62f105d,0x2441453, 0xd8a1e681,0xe7d3fbc8,
                0x21e1cde6,0xc33707d6,0xf4d50d87,0x455a14ed,
                0xa9e3e905,0xfcefa3f8,0x676f02d9,0x8d2a4c8a,
                0xfffa3942,0x8771f681,0x6d9d6122,0xfde5380c,
                0xa4beea44,0x4bdecfa9,0xf6bb4b60,0xbebfbc70,
                0x289b7ec6,0xeaa127fa,0xd4ef3085,0x4881d05,
                0xd9d4d039,0xe6db99e5,0x1fa27cf8,0xc4ac5665,
                0xf4292244,0x432aff97,0xab9423a7,0xfc93a039,
                0x655b59c3,0x8f0ccc92,0xffeff47d,0x85845dd1,
                0x6fa87e4f,0xfe2ce6e0,0xa3014314,0x4e0811a1,
                0xf7537e82,0xbd3af235,0x2ad7d2bb,0xeb86d391};
        
        public string Value
        {
            get
            {
                return ReverseByte( A ).ToString( "X" ) + " " +         //X шестнадцатиричный формат строки
                       ReverseByte( B ).ToString( "X" ) + " " +
                       ReverseByte( C ).ToString( "X" ) + " " +
                       ReverseByte( D ).ToString( "X" );
            }
            set
            {
                A = 0x67452301;
                B = 0xEFCDAB89;
                C = 0x98BADCFE;
                D = 0X10325476;
                input = Encoding.UTF8.GetBytes( value );
                GetHash();
            }
        }

        /// <summary>
        /// Дополнение исходного введенного массива до %512=0
        /// </summary>
        void Create512()
        {
            long dopSize = 0;
            long currentSize = input.Length * 8 + 64;

            dopSize = 512 - currentSize % 512;

            if (dopSize != 0)
            {
                byte[] bt = new byte[dopSize / 8];
                bt[0] = 0x80;
                for (int i = 1; i < bt.Length; i++) bt[i] = 0;
                input = input.Concat( bt ).ToArray();
            }
            input = input.Concat( BitConverter.GetBytes( currentSize - 64 ) ).ToArray();
        }      
        
        /// <summary>
        /// Получение хэша 
        /// </summary>
        protected void GetHash()
        {
            Create512();

            uint N = (uint)( input.Length * 8 ) / 32;// кол-во блоков по 32 бита

            for (uint i = 0; i < N / 16; i++)
            {
                CopyBlock( input, i );
                PerformTransformation( ref A, ref B, ref C, ref D );
            }
        }

        protected void TransF( ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i )
        {
            a = b + RotateLeft( ( a + ( ( b & c ) | ( ~( b ) & d ) ) + X[k] + T[i - 1] ), s  );
        }

        protected void TransG( ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i )
        {
            a = b + RotateLeft( ( a + ( ( b & d ) | ( c & ~d ) ) + X[k] + T[i - 1] ), s );
        }

        protected void TransH( ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i )
        {
            a = b + RotateLeft( ( a + ( b ^ c ^ d ) + X[k] + T[i - 1] ), s );
        }

        protected void TransI( ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i )
        {
            a = b + RotateLeft( ( a + ( c ^ ( b | ~d ) ) + X[k] + T[i - 1] ), s );
        }

        protected void PerformTransformation( ref uint A, ref uint B, ref uint C, ref uint D )
        {
            uint AA, BB, CC, DD;

            AA = A;
            BB = B;
            CC = C;
            DD = D;

            /* Round 1 
				* [ABCD  0  7  1]  [DABC  1 12  2]  [CDAB  2 17  3]  [BCDA  3 22  4]
				* [ABCD  4  7  5]  [DABC  5 12  6]  [CDAB  6 17  7]  [BCDA  7 22  8]
				* [ABCD  8  7  9]  [DABC  9 12 10]  [CDAB 10 17 11]  [BCDA 11 22 12]
				* [ABCD 12  7 13]  [DABC 13 12 14]  [CDAB 14 17 15]  [BCDA 15 22 16]
				*  * */
            TransF( ref A, B, C, D, 0, 7, 1 );   TransF( ref D, A, B, C, 1, 12, 2 );   TransF( ref C, D, A, B, 2, 17, 3 );   TransF( ref B, C, D, A, 3, 22, 4 );
            TransF( ref A, B, C, D, 4, 7, 5 );   TransF( ref D, A, B, C, 5, 12, 6 );   TransF( ref C, D, A, B, 6, 17, 7 );   TransF( ref B, C, D, A, 7, 22, 8 );
            TransF( ref A, B, C, D, 8, 7, 9 );   TransF( ref D, A, B, C, 9, 12, 10 );  TransF( ref C, D, A, B, 10, 17, 11 ); TransF( ref B, C, D, A, 11, 22, 12 );
            TransF( ref A, B, C, D, 12, 7, 13 ); TransF( ref D, A, B, C, 13, 12, 14 ); TransF( ref C, D, A, B, 14, 17, 15 ); TransF( ref B, C, D, A, 15, 22, 16 );
            /** Round 2
				**[ABCD  1  5 17]  [DABC  6  9 18]  [CDAB 11 14 19]  [BCDA  0 20 20]
				*[ABCD  5  5 21]  [DABC 10  9 22]  [CDAB 15 14 23]  [BCDA  4 20 24]
				*[ABCD  9  5 25]  [DABC 14  9 26]  [CDAB  3 14 27]  [BCDA  8 20 28]
				*[ABCD 13  5 29]  [DABC  2  9 30]  [CDAB  7 14 31]  [BCDA 12 20 32]
			*/
            TransG( ref A, B, C, D, 1, 5, 17 );  TransG( ref D, A, B, C, 6, 9, 18 );  TransG( ref C, D, A, B, 11, 14, 19 ); TransG( ref B, C, D, A, 0, 20, 20 );
            TransG( ref A, B, C, D, 5, 5, 21 );  TransG( ref D, A, B, C, 10, 9, 22 ); TransG( ref C, D, A, B, 15, 14, 23 ); TransG( ref B, C, D, A, 4, 20, 24 );
            TransG( ref A, B, C, D, 9, 5, 25 );  TransG( ref D, A, B, C, 14, 9, 26 ); TransG( ref C, D, A, B, 3, 14, 27 );  TransG( ref B, C, D, A, 8, 20, 28 );
            TransG( ref A, B, C, D, 13, 5, 29 ); TransG( ref D, A, B, C, 2, 9, 30 );  TransG( ref C, D, A, B, 7, 14, 31 );  TransG( ref B, C, D, A, 12, 20, 32 );
            /*  Round 3
				* [ABCD  5  4 33]  [DABC  8 11 34]  [CDAB 11 16 35]  [BCDA 14 23 36]
				* [ABCD  1  4 37]  [DABC  4 11 38]  [CDAB  7 16 39]  [BCDA 10 23 40]
				* [ABCD 13  4 41]  [DABC  0 11 42]  [CDAB  3 16 43]  [BCDA  6 23 44]
				* [ABCD  9  4 45]  [DABC 12 11 46]  [CDAB 15 16 47]  [BCDA  2 23 48]
			 * */
            TransH( ref A, B, C, D, 5, 4, 33 );  TransH( ref D, A, B, C, 8, 11, 34 );  TransH( ref C, D, A, B, 11, 16, 35 ); TransH( ref B, C, D, A, 14, 23, 36 );
            TransH( ref A, B, C, D, 1, 4, 37 );  TransH( ref D, A, B, C, 4, 11, 38 );  TransH( ref C, D, A, B, 7, 16, 39 );  TransH( ref B, C, D, A, 10, 23, 40 );
            TransH( ref A, B, C, D, 13, 4, 41 ); TransH( ref D, A, B, C, 0, 11, 42 );  TransH( ref C, D, A, B, 3, 16, 43 );  TransH( ref B, C, D, A, 6, 23, 44 );
            TransH( ref A, B, C, D, 9, 4, 45 );  TransH( ref D, A, B, C, 12, 11, 46 ); TransH( ref C, D, A, B, 15, 16, 47 ); TransH( ref B, C, D, A, 2, 23, 48 );
            /*Round  4
				*[ABCD  0  6 49]  [DABC  7 10 50]  [CDAB 14 15 51]  [BCDA  5 21 52]
				*[ABCD 12  6 53]  [DABC  3 10 54]  [CDAB 10 15 55]  [BCDA  1 21 56]
				*[ABCD  8  6 57]  [DABC 15 10 58]  [CDAB  6 15 59]  [BCDA 13 21 60]
				*[ABCD  4  6 61]  [DABC 11 10 62]  [CDAB  2 15 63]  [BCDA  9 21 64]
						 * */
            TransI( ref A, B, C, D, 0, 6, 49 );  TransI( ref D, A, B, C, 7, 10, 50 );  TransI( ref C, D, A, B, 14, 15, 51 ); TransI( ref B, C, D, A, 5, 21, 52 );
            TransI( ref A, B, C, D, 12, 6, 53 ); TransI( ref D, A, B, C, 3, 10, 54 );  TransI( ref C, D, A, B, 10, 15, 55 ); TransI( ref B, C, D, A, 1, 21, 56 );
            TransI( ref A, B, C, D, 8, 6, 57 );  TransI( ref D, A, B, C, 15, 10, 58 ); TransI( ref C, D, A, B, 6, 15, 59 );  TransI( ref B, C, D, A, 13, 21, 60 );
            TransI( ref A, B, C, D, 4, 6, 61 );  TransI( ref D, A, B, C, 11, 10, 62 ); TransI( ref C, D, A, B, 2, 15, 63 );  TransI( ref B, C, D, A, 9, 21, 64 );

            A += AA;
            B += BB;
            C += CC;
            D += DD;
        }


        public static uint RotateLeft( uint uiNumber, ushort shift )   { return ( ( uiNumber >> (32 - shift) ) | ( uiNumber << shift ) ); }// циклический сдвиг влево

        public static uint ReverseByte( uint uiNumber )
        {
            return ( ( ( uiNumber & 0x000000ff ) << 24 ) |
                       ( uiNumber >> 24 ) |
                    (  ( uiNumber & 0x00ff0000 ) >> 8 ) |
                    (  ( uiNumber & 0x0000ff00 ) << 8 ) );
        }

        protected void CopyBlock( byte[] bMsg, uint block )
        {
            block = block << 6;
            for (uint j = 0; j < 61; j += 4)
            {
                X[j >> 2] = ( ( (uint)bMsg[block + ( j + 3 )] ) << 24 ) |
                            ( ( (uint)bMsg[block + ( j + 2 )] ) << 16 ) |
                            ( ( (uint)bMsg[block + ( j + 1 )] ) << 8  ) |
                            ( ( (uint)bMsg[block + ( j     )] ) );
            }
        }
    }
}