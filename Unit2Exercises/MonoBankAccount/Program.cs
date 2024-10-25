﻿#region TRANSACTION DATA
const decimal MAX_INCOME = 5000;
const decimal MAX_OUTCOME = 2000;
const int EXIT_NUMBER = 7;

decimal currentMoney = 0;
string? input;
int optionInt;
bool exit = false;
List<string> movementList = new List<string>();
#endregion

Console.OutputEncoding = System.Text.Encoding.UTF8;

do
{
	Console.Clear();

	Console.WriteLine($@"
==============================================================
|| Welcome to your bank account! What do you want to do?    ||
||                                                          ||
||	1 - Money Income                                    ||
||	2 - Money Outcome                                   ||
||	3 - List all movements                              ||
||	4 - List Incomes                                    ||
||	5 - List Outcomes                                   ||
||	6 - Show current money                              ||
||	{EXIT_NUMBER} - Exit                                            ||
||                                                          ||
|| Please write the option's number:                        ||
==============================================================");

    Console.Write("=> ");
    input = Console.ReadLine()?.Trim();

	if (input != null && input.Length == 1 && input[0] >= '1' && input[0] <= '7' && int.TryParse(input, out optionInt))
	{
		optionInt = int.Parse(input);
		Console.WriteLine("Option chosen: " + optionInt);


		if (optionInt != EXIT_NUMBER)
		{
			switch (optionInt)
			{
				case 1:
					Console.WriteLine("=================================================\n" +
						"Please write how much money you want to deposit:");
                    decimal income = 0;

					do
					{
						Console.Write("=> ");
						input = Console.ReadLine()?.Trim().Replace(".", ",");

                        if (input != null && input.Length > 0 && decimal.TryParse(input, out income))
						{
							if (income <= MAX_INCOME)
							{
								currentMoney += income;
								movementList.Add("+" + income + " | " + DateTime.Now);
								Console.WriteLine($"{income}€ were added to your account.\n" +
									$"Current money: {currentMoney:0.00}€");
                            }
							else Console.WriteLine($"ERROR: The top amount you can deposit is {MAX_INCOME:0.00}€. Try again.");
						}
						else
						{
							Console.WriteLine(
								"ERROR: Please write a numeric value: ");
						}
					} while (!decimal.TryParse(input, out income) || income > MAX_INCOME);
					break;

				case 2:
					decimal outcome = 0;
					Console.WriteLine("=================================================\n" +
                        "Please write how much money you want to withdraw:");

					do
					{
                        Console.Write("=> ");
                        input = Console.ReadLine()?.Trim().Replace(".", ",");

                        if (input != null && input.Length > 0 && decimal.TryParse(input, out outcome))
						{
							outcome = Math.Abs(decimal.Parse(input));

							if (outcome <= MAX_OUTCOME)
							{
								if (outcome <= currentMoney)
								{
									currentMoney -= outcome;
									movementList.Add("-" + outcome + " | " + DateTime.Now);
									Console.WriteLine($"{outcome:0.00}€ were subtracted from account.\n" +
										$"Current money: {currentMoney:0.00}€");
								}
								else
								{
									Console.WriteLine(
										"ERROR: You cannot withdraw this amount of money. Try a lower value.\n" +
										$"Money available: {currentMoney:0.00}€\n" +
										"Amount to withdraw: ");
								}
							}
							else
							{
                                Console.WriteLine(
                                        $"ERROR: You cannot withdraw more than {MAX_OUTCOME:0.00}€. Try a lower value.\n" +
                                        "Amount to withdraw: ");
                            }
						}
						else
						{
							Console.WriteLine(
								"ERROR: Please write a numeric value: ");
						}
					} while (!decimal.TryParse(input, out outcome) || outcome > MAX_OUTCOME || outcome > currentMoney);

					break;

				case 3:
					Console.WriteLine("=================================================\n" +
                        "These are all your movements:\n" +
                        "-------------------------------------------------");

                    foreach (string movement in movementList)
					{
						Console.WriteLine(movement);
					}
					movementList.Add(">> Movement List printed | " + DateTime.Now);
					break;

				case 4:
					Console.WriteLine("=================================================\n" +
						"These are all your incomes:\n" +
						"-------------------------------------------------");

					foreach (string movement in movementList)
					{
						if (movement.StartsWith('+')) Console.WriteLine(movement);
					}
					movementList.Add(">> Income List printed | " + DateTime.Now);
					break;

				case 5:
                    Console.WriteLine("=================================================\n" +
                        "These are all your outcomes:\n" +
                        "-------------------------------------------------");

                    foreach (string movement in movementList)
					{
						if (movement.StartsWith('-')) Console.WriteLine(movement);
					}
					movementList.Add(">> Outcome List printed | " + DateTime.Now);
					break;

				case 6:
					Console.WriteLine($"Current money: {currentMoney:0.00}€");
					movementList.Add(">> Account Money consulted | " + DateTime.Now);
					break;

			}

            Console.WriteLine("Do you wish to make another operation?\n" +
                "Write 'yes' if so or type anything else to exit: ");
            Console.Write("=> ");
            input = Console.ReadLine()?.Trim();
			if (input != null && input.Length > 0 && !input.ToLower().Equals("yes"))
			{
				exit = true;
			}
			else exit = false;
		}
		else
		{
			exit = true;
			Console.WriteLine($"Current money: {currentMoney:0.00}€");
			Console.WriteLine("Thanks for using our services. Goodbye!");
		}
	}
	else
	{
		Console.WriteLine("ERROR: Invalid option, try again.");
	}

} while (!exit);

