using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using NorthwindMobile.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using System.Text;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(PhoneDialer))]
namespace NorthwindMobile.Droid
{
	class PhoneDialer : IDialer
	{
		public bool Dial(string number)
		{
			var context = CrossCurrentActivity.Current.Activity;

			if (context == null) return false;

			var intent = new Intent(Intent.ActionCall);
			intent.SetData(Uri.Parse("tel:" + number));

			if (IsIntentAvailable(context, intent))
			{
				context.StartActivity(intent);return true;
			}
			return false;
		}

		private bool IsIntentAvailable(Context context, Intent intent)
		{
			var packageManager = context.PackageManager;

			var list = packageManager
				.QueryIntentServices(intent, 0)
				.Union(packageManager.QueryIntentActivities(intent, 0));

			if (list.Any()) return true;

			var manager = TelephonyManager.FromContext(context);

			return manager.PhoneType != PhoneType.None;
		}
	}
}