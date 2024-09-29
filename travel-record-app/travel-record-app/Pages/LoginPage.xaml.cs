using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using travelrecordapp.Models;

namespace travelrecordapp.Pages
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage ()
		{
			InitializeComponent ();

            Type assembly = typeof(LoginPage);

            IconImage.Source =
                ImageSource.FromResource
                    (
                        "travel-record-app.Assets.Images.travel-bag.png",
                        assembly
                    );
        }

        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                // get user from App.MobileService (Azure AppServices app)
                var user = (await App.MobileService.GetTable<User>()
                    .Where(u => u.Email == EmailEntry.Text)
                    .ToListAsync()).FirstOrDefault();

                if (user != null)
                { // todo: ensure user-var data from AppServices Easy table has same props as User model
                    App.user = user;

                    if (user.Password == PasswordEntry.Text)
                        await Navigation.PushAsync(new Home_Page());
                    else
                        await DisplayAlert("Error", "Email or password are incorrect", "Ok");
                }
                else
                    await DisplayAlert("Error", "There was an error logging you in", "Ok");

            }
        }

        void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}

