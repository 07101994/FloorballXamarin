package md5276ac96709abf0493e63a74fffdd832f;


public class SettingsFragment
	extends android.support.v7.preference.PreferenceFragmentCompat
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreatePreferences:(Landroid/os/Bundle;Ljava/lang/String;)V:GetOnCreatePreferences_Landroid_os_Bundle_Ljava_lang_String_Handler\n" +
			"n_onNavigateToScreen:(Landroid/support/v7/preference/PreferenceScreen;)V:GetOnNavigateToScreen_Landroid_support_v7_preference_PreferenceScreen_Handler\n" +
			"";
		mono.android.Runtime.register ("Floorball.Droid.Fragments.SettingsFragment, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SettingsFragment.class, __md_methods);
	}


	public SettingsFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SettingsFragment.class)
			mono.android.TypeManager.Activate ("Floorball.Droid.Fragments.SettingsFragment, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreatePreferences (android.os.Bundle p0, java.lang.String p1)
	{
		n_onCreatePreferences (p0, p1);
	}

	private native void n_onCreatePreferences (android.os.Bundle p0, java.lang.String p1);


	public void onNavigateToScreen (android.support.v7.preference.PreferenceScreen p0)
	{
		n_onNavigateToScreen (p0);
	}

	private native void n_onNavigateToScreen (android.support.v7.preference.PreferenceScreen p0);

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
