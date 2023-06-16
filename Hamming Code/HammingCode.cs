using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammingCode
{
    public static class BitArrayMoreOption
    {
        public static int Weight(BitArray bitArray)
        {
            int weight = bitArray.Cast<bool>().Count(x => x);
            return weight;
        }
        public static bool ParityWeight(BitArray bitArray)
        {
            return Weight(bitArray) % 2 == 1;
        }
        public static string ConvertToStr(BitArray bitArray)
        {
            string response = "";
            for (int i = bitArray.Count - 1; i >= 0; i--)
            {
                response += (bitArray[i]) ? "1" : "0";
            }
            return response;
        }
        public static int ConvertToInt(BitArray bitArray)
        {
            int value = 0;
            for (int i = 0; i < bitArray.Count; i++)
            {
                value += (bitArray[i]) ? (int)Math.Pow(2, i) : 0;
            }
            return value;
        }
    }
    public static class HammingGenerate
    {
        public static BitArray[] GetGenerationMatrix()
        {
            BitArray[] GenerationMatrix = new BitArray[7];
            GenerationMatrix[6] = new BitArray(new bool[] { false, false, false, true });
            GenerationMatrix[5] = new BitArray(new bool[] { false, false, true, false });
            GenerationMatrix[4] = new BitArray(new bool[] { false, true, false, false });
            GenerationMatrix[3] = new BitArray(new bool[] { false, true, true, true });
            GenerationMatrix[2] = new BitArray(new bool[] { true, false, false, false });
            GenerationMatrix[1] = new BitArray(new bool[] { true, false, true, true });
            GenerationMatrix[0] = new BitArray(new bool[] { true, true, false, true });
            return GenerationMatrix;
        }
        public static BitArray[] GetP3GenerationMatrix()
        {
            BitArray[] P3GenerationMatrix = new BitArray[8];
            P3GenerationMatrix[7] = new BitArray(new bool[] { true, true, true, true, true, true, true });
            P3GenerationMatrix[6] = new BitArray(new bool[] { false, false, false, false, false, false, true });
            P3GenerationMatrix[5] = new BitArray(new bool[] { false, false, false, false, false, true, false });
            P3GenerationMatrix[4] = new BitArray(new bool[] { false, false, false, false, true, false, false });
            P3GenerationMatrix[3] = new BitArray(new bool[] { false, false, false, true, false, false, false });
            P3GenerationMatrix[2] = new BitArray(new bool[] { false, false, true, false, false, false, false });
            P3GenerationMatrix[1] = new BitArray(new bool[] { false, true, false, false, false, false, false });
            P3GenerationMatrix[0] = new BitArray(new bool[] { true, false, false, false, false, false, false });
            return P3GenerationMatrix;
        }
        public static BitArray Generate(BitArray data)
        {
            BitArray Hamming1 = new BitArray(7);
            for (int i = 0; i < 7; i++)
            {
                BitArray bitArray = GetGenerationMatrix()[i].And(data);
                Hamming1.Set(i, BitArrayMoreOption.ParityWeight(bitArray));
            }
            BitArray Hamming2 = new BitArray(8);
            for (int i = 0; i < 8; i++)
            {
                BitArray bitArray = GetP3GenerationMatrix()[i].And(Hamming1);
                Hamming2.Set(i, BitArrayMoreOption.ParityWeight(bitArray));
            }
            return Hamming2;
        }
    }
    public static class HammingCorrect
    {
        public static BitArray[] GetCorrectionMatrix()
        {
            BitArray[] CorrectionMatrix = new BitArray[4];
            CorrectionMatrix[3] = new BitArray(new bool[] { true, true, true, true, true, true, true, true });
            CorrectionMatrix[2] = new BitArray(new bool[] { false, false, false, true, true, true, true, false });
            CorrectionMatrix[1] = new BitArray(new bool[] { false, true, true, false, false, true, true, false });
            CorrectionMatrix[0] = new BitArray(new bool[] { true, false, true, false, true, false, true, false });
            return CorrectionMatrix;
        }
        public static int ErrorCode(BitArray data)
        {
            BitArray errorCode = new BitArray(4);
            for (int i = 0; i < 4; i++)
            {
                BitArray bitArray = GetCorrectionMatrix()[i].And(data);
                errorCode.Set(i, BitArrayMoreOption.ParityWeight(bitArray));
            }
            return BitArrayMoreOption.ConvertToInt(errorCode);
        }
        public static string AnalyseError(BitArray data)
        {
            if (ErrorCode(data) == 0) return "Correct!";
            if (ErrorCode(data) < 8) return "2 Error Detected!";
            if (ErrorCode(data) == 8) return "Error was in 7 index!";
            return $"Error was in {ErrorCode(data) - 9} index!";
        }
        public static BitArray ExtractData(BitArray data)
        {
            BitArray ExtractedData = new BitArray(4);
            ExtractedData.Set(0, data[2]);
            ExtractedData.Set(1, data[4]);
            ExtractedData.Set(2, data[5]);
            ExtractedData.Set(3, data[6]);
            return ExtractedData;
        }
        public static BitArray ExtractCorrectedData(BitArray data)
        {
            return ExtractData(Correct(data));
        }
        public static BitArray Correct(BitArray data)
        {
            int errorCode = ErrorCode(data);
            if (errorCode <= 7)
            {
                return new BitArray(data);
            }
            BitArray Corrected = new BitArray(data);
            if (errorCode == 8)
            {
                Corrected.Set(7, !data[7]);
            }
            else
            {
                Corrected.Set(errorCode - 9, !data[errorCode - 9]);
            }
            return Corrected;
        }
    }
}
