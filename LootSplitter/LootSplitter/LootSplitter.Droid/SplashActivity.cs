using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LootSplitter.Droid
{
    [Activity(Theme="@style/LootSplitterTheme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnResume()
        {
            base.OnResume();
            Thread.Sleep(2000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}