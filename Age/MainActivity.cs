using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System;
using Android.Appwidget;
using Android.Content;
using Java.Util;
//using Java.Util;

namespace Age
{
	[BroadcastReceiver(Label = "@string/app_name")]
	[IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE","com.mahdia.age.widget.UPDATE_AGE" })]
	[MetaData("android.appwidget.provider", Resource = "@xml/widgetproviderinfo")]
	public class MainActivity : AppWidgetProvider
	{
		int count = 1;
		TextView years;
		TextView deci;
		public static string UPDATE_AGE = "com.mahdia.age.widget.UPDATE_AGE";


		public override void OnUpdate(Android.Content.Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
		{
			DateTime bday = new DateTime(1991, 05, 22);

			int count = appWidgetIds.Length;

			for (int i = 0; i < count; i++)
			{
				int widgetId = appWidgetIds[i];

				string numberYrs = (DateTime.Now.Year - bday.Year).ToString()+".";
				string numberDec = string.Format("{0:0.000000000}", ((DateTime.Now - bday).TotalDays / 365)).Split('.')[1];//.ToString());

				RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.Main);

				remoteViews.SetTextViewText(Resource.Id.ageYears, numberYrs);
				remoteViews.SetTextViewText(Resource.Id.ageDecimal, numberDec);

				Intent intent = new Intent(context, typeof(MainActivity));

				intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
				intent.PutExtra(AppWidgetManager.ExtraAppwidgetId, widgetId);

				PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);


				          
				appWidgetManager.UpdateAppWidget(widgetId, remoteViews);

			}

		}

		PendingIntent createAgeIntent(Context context)
		{

			Intent intent = new Intent(UPDATE_AGE);
			//intent.SetAction(Java.Lang.JavaSystem.CurrentTimeMillis().ToString());
			PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
			return pendingIntent;
		}

		public override void OnEnabled(Context context)
		{
			base.OnEnabled(context);

			AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

			Calendar calendar = Calendar.GetInstance(Locale.Us);

			calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();

			calendar.Add(CalendarField.Millisecond, 10);

			alarmManager.SetExact(AlarmType.Rtc, 10, createAgeIntent(context));

		}

		public override void OnDisabled(Context context)
		{
			base.OnDisabled(context);
			AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
			alarmManager.Cancel(createAgeIntent(context));
		}

		public override void OnReceive(Context context, Intent intent)
		{
			base.OnReceive(context, intent);

			if (UPDATE_AGE.Equals(intent.Action))
			{
				Console.WriteLine($"age Update    {Class.Name}"); 

				ComponentName thisAppWidget = new ComponentName(context.PackageName, Class.Name);

				AppWidgetManager appWidgetManager = AppWidgetManager.GetInstance(context);
				int[] ids = appWidgetManager.GetAppWidgetIds(thisAppWidget);


				foreach (var i in ids)
				{
					Console.WriteLine($"ids:{i}"); 
					updateAppWidget(context, appWidgetManager, i);
				}


			}

		}

		public static void updateAppWidget(Context context, AppWidgetManager appWidgetManager, int appWidgetId)
		{
			DateTime bday = new DateTime(1991, 05, 22);

			string numberYrs = (DateTime.Now.Year - bday.Year).ToString() + ".";
			string numberDec = string.Format("{0:0.00000000}", ((DateTime.Now - bday).TotalDays / 365)).Split('.')[1];//.ToString());

			RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.Main);

			remoteViews.SetTextViewText(Resource.Id.ageYears, numberYrs);
			remoteViews.SetTextViewText(Resource.Id.ageDecimal, numberDec);

			appWidgetManager.UpdateAppWidget(appWidgetId, remoteViews);

		}


		/*
		async protected override void OnCreate(Bundle savedInstanceState)
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
		*/


	}
}


