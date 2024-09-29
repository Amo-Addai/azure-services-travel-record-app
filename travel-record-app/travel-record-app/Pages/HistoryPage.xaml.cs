using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;

using travelrecordapp.Models;

namespace travelrecordapp.Pages
{	
	public partial class HistoryPage : ContentPage
	{	
		public HistoryPage ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			List<Post> posts;

            using
			(
				SqliteConnection conn =
					new SqliteConnection
					(
						App.DatabaseLocation
					)
			)
			{
                conn.CreateTable<Post>(); // todo: will only create-table if it doesn't already exist

                posts = conn.Table<Post>()
								.ToList();
                // conn.Close(); // * conn is closed by default after using-scope

                // set PostListView's ItemsSource with sqlite-db Post-Table data
                PostListView.ItemsSource = posts; // set ListView's data-context (data-binding for its ItemTemplate > DataTemplate)
            }

			posts = await App.MobileService
							 .GetTable<Post>()
							 .Where(
								p => p.UserId == App.user.Id
							 )
							 .ToListAsync();

            // set PostListView's ItemsSource with Azure AppService Post-Table data
            PostListView.ItemsSource = posts;

		}

	}
}

