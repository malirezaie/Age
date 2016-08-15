
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Age
{
	[Activity(Label = "Age.Droid", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MyActivity : Activity
	{
		int count = 1;
		TextView years;
		TextView deci;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			years = FindViewById<TextView>(Resource.Id.ageYears);
			deci = FindViewById<TextView>(Resource.Id.ageDecimal);
			//button.Text = await getAge();

			DateTime bday = new DateTime(1991, 05, 22);

			System.Timers.Timer timer = new System.Timers.Timer(10);

			years.Text = (DateTime.Now.Year - bday.Year).ToString() + ".";
				//((DateTime.Now - bday).TotalDays / 365).ToString().Split('.')[0] + ".";

			timer.Elapsed += (sender, e) =>
			{
				//TimeSpan span = DateTime.Now - bday;

				string hours = string.Format("{0:0.000000000}", ((DateTime.Now - bday).TotalDays/ 365)).Split('.')[1];//.ToString());

				RunOnUiThread(() =>
				{
					deci.Text = hours;

				});

			};
			timer.Start();

		}

	}
}

