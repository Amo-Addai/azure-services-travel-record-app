using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace travelrecordapp.Pages
{	
	public partial class Home_Page : ContentPage
    { // cannot use 'HomePage' class name, as a sub-class for ContentPage
		// CS0263: Partial declarations of 'HomePage' must not specify different base classes

        public Home_Page ()
		{
			InitializeComponent ();
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}

