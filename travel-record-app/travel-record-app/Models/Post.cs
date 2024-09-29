using System;
using SQLite;

namespace travelrecordapp.Models
{
	public class Post
	{
		[PrimaryKey, AutoIncrement]
		public string Id { get; set; } // string Id used in both sqlite.db & Azure AppService's Easy tables
		// public int Id { get; set; } // int Id used in sqlite.db only

		[MaxLength(250)]
		public string Experience { get; set; }

		public string VenueName { get; set; }

		public string CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Address { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public int Distance { get; set; }

		public string UserId { get; set; }

	}
}

