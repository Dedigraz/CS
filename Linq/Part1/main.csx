/* int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

var lowNum = from num in numbers
            where num < 5
            select num;

Console.WriteLine("Number < 5");

foreach (var item in lowNum)
{
    Console.WriteLine(item);
} */

string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

var shortDigits = digits.Where((digit, index) => digit.Length < index);

Console.WriteLine("Short digits:");
foreach (var d in shortDigits)
{
    Console.WriteLine($"The word {d} is shorter than its value.");
}

Console.WriteLine();


int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

var numsInPlace = numbers.Select((num, index) => (Num: num, InPlace: (num == index)));

Console.WriteLine("Number: In-place?");
foreach (var n in numsInPlace)
{
    Console.WriteLine($"{n.Num}: {n.InPlace}");
}