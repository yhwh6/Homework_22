public class Program
{
    public static void Main(string[] args)
    {
        string[] inputs = { "123.45", "-678.9", "0.123", "9,876.54321" };
        double[] expectedOutputs = { 123.45, -678.9, 0.123, 9876.54321 };

        for (int i = 0; i < inputs.Length; i++)
        {
            string input = inputs[i];
            double expectedOutput = expectedOutputs[i];

            double result = StringToDoubleConverter.Convert(input);

            Console.WriteLine($"Input: {input}, Expected Output: {expectedOutput}, Result: {result}");

            if (Math.Abs(result - expectedOutput) < 1e-6)
            {
                Console.WriteLine("Тест пройден успешно!");
            }
            else
            {
                Console.WriteLine("Тест не пройден.");
            }

            Console.WriteLine();
        }
    }
}
public class StringToDoubleConverter
{
    public static double Convert(string input)
    {
        // Проверка на пустую строку
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Входная строка не может быть пустой.");

        // Удаление пробелов в начале и конце строки
        input = input.Trim();

        // Проверка на отрицательное число
        bool isNegative = false;

        if (input[0] == '-')
        {
            isNegative = true;
            input = input.Substring(1);
        }

        // Проверка на наличие десятичной части
        bool hasFractionalPart = false;

        int decimalSeparatorIndex = input.IndexOf('.');

        if (decimalSeparatorIndex == -1)
            decimalSeparatorIndex = input.IndexOf(',');

        if (decimalSeparatorIndex != -1)
            hasFractionalPart = true;

        // Удаление разделителя десятичной части
        if (hasFractionalPart)
        {
            input = input.Remove(decimalSeparatorIndex, 1);
        }

        // Преобразование строки в число
        double result = 0.0;
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                result = result * 10 + (c - '0');
            }
        }

        // Деление на 10^количество цифр в десятичной части
        if (hasFractionalPart)
        {
            int fractionalPartLength = input.Length - decimalSeparatorIndex;
            result /= Math.Pow(10, fractionalPartLength);
        }

        // Учет отрицательного числа
        if (isNegative)
        {
            result = -result;
        }

        return result;
    }
}