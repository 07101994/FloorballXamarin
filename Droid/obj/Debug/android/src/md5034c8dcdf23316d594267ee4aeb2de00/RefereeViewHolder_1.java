package md5034c8dcdf23316d594267ee4aeb2de00;


public class RefereeViewHolder_1
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Floorball.Droid.ViewHolders.RefereeViewHolder`1, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RefereeViewHolder_1.class, __md_methods);
	}


	public RefereeViewHolder_1 (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == RefereeViewHolder_1.class)
			mono.android.TypeManager.Activate ("Floorball.Droid.ViewHolders.RefereeViewHolder`1, Floorball.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
