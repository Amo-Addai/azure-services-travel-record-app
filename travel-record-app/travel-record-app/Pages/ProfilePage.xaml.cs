using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

using travel_record_app.Models;

namespace travel_record_app.Pages
{	
	public partial class ProfilePage : ContentPage
	{	
		public ProfilePage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				var postTable =
					conn
						.Table<Post>()
						.ToList();

				var categories = (
						from p in postTable
						orderby p.CategoryId
						select p.CategoryName
					).Distinct().ToList();

				/*
				var categories =
					postTable
						.OrderBy(p => p.CategoryId)
						.Select(p => p.CategoryName)
						.Distinct().ToList();
				*/

				Dictionary<string, int> categoriesCount =
					new Dictionary<string, int>();

				int count = 0;

				foreach (var category in categories)
				{
					count = (
						from post in postTable
						where post.CategoryName == category
						select post
					).ToList().Count;

					/* shorter syntax
					count =
						postTable.Where(
							p => p.CategoryName == category
						).ToList().Count;
					*/

					categoriesCount.Add(category, count);
				}

				CategoriesListView.ItemSource = categoriesCount; // Key/Value Binding

				PostCountLabel.Text = postTable.Count.ToString();
			}
        }
    }
}

