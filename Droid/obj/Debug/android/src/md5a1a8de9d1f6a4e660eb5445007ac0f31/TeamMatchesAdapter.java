package md5a1a8de9d1f6a4e660eb5445007ac0f31;


public class TeamMatchesAdapter
	extends md5a1a8de9d1f6a4e660eb5445007ac0f31.MatchesAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Floorball.Droid.Adapters.TeamMatchesAdapter, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TeamMatchesAdapter.class, __md_methods);
	}


	public TeamMatchesAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TeamMatchesAdapter.class)
			mono.android.TypeManager.Activate ("Floorball.Droid.Adapters.TeamMatchesAdapter, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
