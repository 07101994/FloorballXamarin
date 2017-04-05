package md5d2583f8f42cd9f57caad90f29bc80c4a;


public class LeagueActivity
	extends md5d2583f8f42cd9f57caad90f29bc80c4a.FloorballActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Floorball.Droid.Activities.LeagueActivity, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LeagueActivity.class, __md_methods);
	}


	public LeagueActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LeagueActivity.class)
			mono.android.TypeManager.Activate ("Floorball.Droid.Activities.LeagueActivity, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
