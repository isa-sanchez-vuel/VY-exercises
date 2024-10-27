Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Welcome to our program!" +
	"\n-----------------------------------");

Console.WriteLine("Enter 'true' or 'false':");
bool Boolean = bool.Parse(Console.ReadLine().Trim().ToLower());

Console.WriteLine("Enter a integer number:");
decimal Integer = int.Parse(Console.ReadLine().Trim());

Console.WriteLine("Enter a decimal value (00,00 format):");
decimal Decimal = decimal.Parse(Console.ReadLine().Trim().Replace(".",","));

Console.WriteLine("Enter one character:");
char Character = char.Parse(Console.ReadLine().Substring(0,1));

Console.WriteLine("Enter a text:");
string Text = Console.ReadLine().Trim();

Console.WriteLine("Enter a date in format dd/MM/yyyy HH:mm:ss (for example 20/10/2024 14:30:45)):");
DateTime date = DateTime.Parse(Console.ReadLine().Trim());

Console.WriteLine("\n-----------------------------------\n");

Console.WriteLine($"Bool negation: {!Boolean}");
Console.WriteLine($"int divided by decimal: {Integer/Decimal:0.00}");
Console.WriteLine($"Text and character: {Character} ({Text}) {Character}");
Console.WriteLine($"Date's last second of the last day: {date.Second.ToString().Last()}");
