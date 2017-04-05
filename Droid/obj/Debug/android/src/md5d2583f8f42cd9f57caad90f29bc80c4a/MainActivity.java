package md5d2583f8f42cd9f57caad90f29bc80c4a;


public class MainActivity
	extends md5d2583f8f42cd9f57caad90f29bc80c4a.FloorballActivity
	implements
		mono.android.IGCUserPeer,
		android.content.SharedPreferences.OnSharedPreferenceChangeListener,
		android.support.v7.preference.PreferenceFragmentCompat.OnPreferenceStartScreenCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onPostCreate:(Landroid/os/Bundle;Landroid/os/PersistableBundle;)V:GetOnPostCreate_Landroid_os_Bundle_Landroid_os_PersistableBundle_Handler\n" +
			"n_onConfigurationChanged:(Landroid/content/res/Configuration;)V:GetOnConfigurationChanged_Landroid_content_res_Configuration_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onSharedPreferenceChanged:(Landroid/content/SharedPreferences;Ljava/lang/String;)V:GetOnSharedPreferenceChanged_Landroid_content_SharedPreferences_Ljava_lang_String_Handler:Android.Content.ISharedPreferencesOnSharedPreferenceChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onPreferenceStartScreen:(Landroid/support/v7/preference/PreferenceFragmentCompat;Landroid/support/v7/preference/PreferenceScreen;)Z:GetOnPreferenceStartScreen_Landroid_support_v7_preference_PreferenceFragmentCompat_Landroid_support_v7_preference_PreferenceScreen_Handler:Android.Support.V7.Preferences.PreferenceFragmentCompat/IOnPreferenceStartScreenCallbackInvoker, Xamarin.Android.Support.v7.Preference\n" +
			"";
		mono.android.Runtime.register ("Floorball.Droid.Activities.MainActivity, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("Floorball.Droid.Activities.MainActivity, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onPostCreate (android.os.Bundle p0, android.os.PersistableBundle p1)
	{
		n_onPostCreate (p0, p1);
	}

	private native void n_onPostCreate (android.os.Bundle p0, android.os.PersistableBundle p1);


	public void onConfigurationChanged (android.content.res.Configuration p0)
	{
		n_onConfigurationChanged (p0);
	}

	private native void n_onConfigurationChanged (android.content.res.Configuration p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public void onSharedPreferenceChanged (android.content.SharedPreferences p0, java.lang.String p1)
	{
		n_onSharedPreferenceChanged (p0, p1);
	}

	private native void n_onSharedPreferenceChanged (android.content.SharedPreferences p0, java.lang.String p1);


	public boolean onPreferenceStartScreen (android.support.v7.preference.PreferenceFragmentCompat p0, android.support.v7.preference.PreferenceScreen p1)
	{
		return n_onPreferenceStartScreen (p0, p1);
	}

	private native boolean n_onPreferenceStartScreen (android.support.v7.preference.PreferenceFragmentCompat p0, android.support.v7.preference.PreferenceScreen p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
