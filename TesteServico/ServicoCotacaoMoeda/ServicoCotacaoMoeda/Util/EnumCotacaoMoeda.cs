﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoCotacaoMoeda.Util
{
    public enum MoedaCotacao
    {
        AFN = 66,
        ALL = 49,
        ANG = 33,
        ARS = 3,
        AWG = 6,
        BOB = 56,
        BYN = 64,
        CAD = 25,
        CDF = 58,
        CLP = 16,
        COP = 37,
        CRC = 52,
        CUP = 8,
        CVE = 51,
        CZK = 29,
        DJF = 36,
        DZD = 54,
        EGP = 12,
        EUR = 20,
        FJD = 38,
        GBP = 22,
        GEL = 48,
        GIP = 18,
        HTG = 63,
        ILS = 40,
        IRR = 17,
        ISK = 11,
        JPY = 9,
        KES = 21,
        KMF = 19,
        LBP = 42,
        LSL = 4,
        MGA = 35,
        MGB = 26,
        MMK = 69,
        MRO = 53,
        MRU = 15,
        MUR = 7,
        MXN = 41,
        MZN = 43,
        NIO = 23,
        NOK = 62,
        OMR = 34,
        PEN = 45,
        PGK = 2,
        PHP = 24,
        RON = 5,
        SAR = 44,
        SBD = 32,
        SGD = 70,
        SLL = 10,
        SOS = 61,
        SSP = 47,
        SZL = 55,
        THB = 39,
        TRY = 13,
        TTD = 67,
        UGX = 59,
        USD = 1,
        UYU = 46,
        VES = 68,
        VUV = 57,
        WST = 28,
        XAF = 30,
        XAU = 60,
        XDR = 27,
        XOF = 14,
        XPF = 50,
        ZAR = 65,
        ZWL = 31
    }

    public class RetornaID
    {
        public static MoedaCotacao GetEnumValue(string cod)
        {
            MoedaCotacao valor = default(MoedaCotacao);

            if (Enum.TryParse<MoedaCotacao>(cod, out valor))
            {
                return valor;
            }

            return MoedaCotacao.AFN;
        }
    }
}
