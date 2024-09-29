using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;
// using .. CrossGeolocator

using travelrecordapp.Models;
using travelrecordapp.Logic;

namespace travelrecordapp.Pages
{
	// [XamlCompilation(XamlCompilationOptions.Compile)]
	// * already imported in AssemblyInfo.cs
	public partial class NewTravelPage : ContentPage
	{	
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync();

			var venues = await VenueLogic.GetVenues
				(
					position.Latitude,
					position.Longitude
				);
			VenueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			try
			{
				var selectedVenue =
					VenueListView.SelectedItem
								 as Venue;

				var firstCategory =
					selectedVenue.categories
								 .FirstOrDefault(); // todo: check exists in IList

				Post post = new Post()
				{
					Experience = ExperienceEntry.Text,
					CategoryId = firstCategory.id,
					CategoryName = firstCategory.name,
					Address = selectedVenue.location.address,
					Distance = selectedVenue.location.distance,
					Latitude = selectedVenue.location.lat,
					Longitude = selectedVenue.location.lng,
					VenueName = selectedVenue.name,
					UserId = App.user.Id // get user-id from parent App class's static user
				};

				using // insert post into sqlite-db
					(
						SQLiteConnection conn =
							new SQLiteConnection
							(
								App.DatabaseLocation
							)
					)
				{
					conn.CreateTable<Post>();

					int rows = conn.Insert(post);
					conn.Close(); // * faster runtime to close connection before working with return data

					if (rows > 0)
						await DisplayAlert(
							"Success",
							"Experience successfully inserted",
							"Ok"
						);
					else
						await DisplayAlert(
							"Failure",
							"Experience failed to be inserted",
							"Ok"
						);
				}

				// also insert post into Azure AppService's Easy table
				await App.MobileService.GetTable<Post>().InsertAsync(post);
				await DisplayAlert("Success", "Experience successfully inserted", "Ok");

			}
			catch (NullReferenceException ex)
			{
                await DisplayAlert("Failure", "Experience successfully inserted", "Ok");
            }
			catch (Exception ex)
			{
                await DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }

		}

    }
}

