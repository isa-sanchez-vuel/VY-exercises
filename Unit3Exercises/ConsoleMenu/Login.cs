namespace ConsoleMenu
{
	public class Login
	{
		const int MAX_LOGIN_ATTEMPTS = 5;

		int LoginAttempts;
		bool Logged;

		public Login()
		{
			LoginAttempts = 0;
			Logged = false;
		}


		public bool CheckLoginAttempts()
		{
			if (LoginAttempts <= 0) return true;
			else if (LoginAttempts > 0 && LoginAttempts < MAX_LOGIN_ATTEMPTS - 1) Menu.PrintError($"Credentials not valid. Try again.\nAttempts left: {MAX_LOGIN_ATTEMPTS - LoginAttempts}.");
			else if (MAX_LOGIN_ATTEMPTS - LoginAttempts == 1) Menu.PrintError("Credentials not valid. This is your last attempt.");
			else if (LoginAttempts >= MAX_LOGIN_ATTEMPTS) Menu.PrintError("You cannot try to login again. Call the IT service to recover your credentials.");
			return false;
		}

		public void IncrementAttempt()
		{

			LoginAttempts++;
		}
	}
}
