using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Musik.Client.Android
{
	[Activity (Label = "Musik.Client.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private ListView songsList;

		protected override void OnCreate (Bundle bundle)
		{			
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Playlist);
			InitializeComponents ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.PlaylistMenu, menu);
			
			return base.OnCreateOptionsMenu (menu);
		}

		private void InitializeComponents()
		{
			songsList = FindViewById<ListView> (Resource.Id.songsList);
			var songs = new string[]{ "One","She will be loved" };
			var songsAdapter = new ArrayAdapter<string> (this, Resource.Layout.SongListItem, songs);

			songsList.Adapter = songsAdapter;		
		}
	}
}


