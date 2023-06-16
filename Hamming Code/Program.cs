using HammingCode;
using System.Collections;

while (true)
{
    Console.WriteLine("Generate:");
    BitArray testBitArray1 = new BitArray(4);
    for (int i = 3; i >= 0; i--)
    {
        ConsoleKeyInfo key = Console.ReadKey();
        testBitArray1.Set(i, key.KeyChar == '1');
    }
    Console.WriteLine();
    Console.WriteLine(BitArrayMoreOption.ConvertToStr(HammingGenerate.Generate(testBitArray1)));
    Console.WriteLine("Correct:");
    BitArray testBitArray2 = new BitArray(8);
    for (int i = 7; i >= 0; i--)
    {
        ConsoleKeyInfo key = Console.ReadKey();
        testBitArray2.Set(i, key.KeyChar == '1');
    }
    Console.WriteLine();
    Console.WriteLine(BitArrayMoreOption.ConvertToStr(HammingCorrect.Correct(testBitArray2)));
    Console.WriteLine("Extracted Data : " + BitArrayMoreOption.ConvertToStr(HammingCorrect.ExtractCorrectedData(testBitArray2)));
    Console.WriteLine("Error Code : " + HammingCorrect.ErrorCode(testBitArray2).ToString());
    Console.WriteLine(HammingCorrect.AnalyseError(testBitArray2));
    Console.ReadKey();
    Console.Clear();
}
