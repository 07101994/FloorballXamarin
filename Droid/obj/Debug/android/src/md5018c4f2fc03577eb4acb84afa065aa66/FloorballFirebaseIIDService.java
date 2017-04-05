package md5018c4f2fc03577eb4acb84afa065aa66;


public class FloorballFirebaseIIDService
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("Floorball.Droid.Utils.Notification.FloorballFirebaseIIDService, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FloorballFirebaseIIDService.class, __md_methods);
	}


	public FloorballFirebaseIIDService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == FloorballFirebaseIIDService.class)
			mono.android.TypeManager.Activate ("Floorball.Droid.Utils.Notification.FloorballFirebaseIIDService, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

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
