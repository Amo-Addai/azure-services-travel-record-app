using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using travelrecordapp.Models;

namespace travelrecordapp.Pages
{	
	public partial class RegisterPage : ContentPage
	{	
		public RegisterPage ()
		{
			InitializeComponent ();
		}

		private async void RegisterButton_Clicked(object sender, EventArgs e)
		{
			if (PasswordEntry.Text == ConfirmPasswordEntry.Text)
			{
				User user = new User()
				{
					Email = EmailEntry.Text,
					Password = PasswordEntry.Text
				};

				await App.MobileService
						 .GetTable<User>()
						 .InsertAsync(user);

			}
			else
			{
				await DisplayAlert("Error", "Passwords don't match", "Ok");
			}

        }

    }
}

