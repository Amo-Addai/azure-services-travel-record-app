using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps; // todo
using SQLite;
// using .. CrossGeolocator, Plugin, Position (in .Maps)

using travelrecordapp.Models;

namespace travelrecordapp.Pages
{	
	public partial class MapPage : ContentPage
	{	
		public MapPage ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			var locator = CrossGeolocator.Current;

			// relocate map to user-center when user-location changes
			locator.PositionChanged += Locator_PositionChanged;

			// listen for location changes (draws more battery-power)
			await locator.StartListeningAsync
			(
				0, // minTime
				100.0, // minDistance
				true // includeHeading
            );

            var position = await locator.GetPositionAsync();

            this.MoveToPosition(position);

			List<Post> posts;

			using
				( // get posts from sqlite-db & display in map
					SQLiteConnection conn =
						new SQLiteConnection
							(
								App.DatabaseLocation
							)
				)
			{
				conn.CreateTable<Post>();
				posts = conn.Table<Post>()
							.ToList();

				DisplayInMap(posts);
			}

            // get posts from Azure AppService's Easy table and display in map
            posts = await App.MobileService
							 .GetTable<Post>()
							 .Where(
								p => p.UserId == App.user.Id
							 );
            DisplayInMap(posts);

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

			// clean up geo-locator
			var locator = CrossGeolocator.Current;
			// remove relocation handler
			locator.PositionChanged -= Locator_PositionChanged;
			// stop listening for user-location changes
			await locator.StopListeningAsync();
        }

        private void Locator_PositionChanged
		(
			object sender,
			Plugin.Geolocator.Abstractions.PositionEventArgs
			e
		)
		{
			this.MoveToPosition(e.Position);
		}

		private void MoveToPosition(Maps.Position position)
		{
			var center = new Maps.Position
			(
				position.Latitute,
				position.Longitude
			);

			var span = new Maps.MapSpan
			(
				center,
				2,
				2
			);

            LocationsMap.MoveToRegion(span);
        }

        private void DisplayInMap(List<Post> posts)
        {
			Position position;
			Pin pin;

            foreach (Post post in posts)
			{
				try
				{
					position =
						new Position
							(
								post.Latitude,
								post.Longitude
							);
					pin = new Pin()
					{
						Type = PinType.SavedPin,
						Position = position,
						Label = post.VenueName,
						Address = post.Address
					};
					LocationsMap.Pins
								.Add(pin);
				}
				catch (NullReferenceException ex)
				{

				}
				catch (Exception ex)
				{

				}
			}
        }

    }
}

