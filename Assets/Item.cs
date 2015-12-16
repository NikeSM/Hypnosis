	public class Item 
{
	private string _name;
	private int _id;
	private bool _buy;
	private bool _load;
	private string _path;
	public Item()
	{
		_name = "";
		_id = 0;
		_buy = false;
		_load = false;

	}
	public Item(string n, int id, bool buy, bool load, string path)
	{
		_name = n;
		_id = id;
		_buy = buy;
		_load = load;
		_path = path;
	}
	public string Name
	{
		get{return _name;}
		set{_name = value;}
	}
	public int Id
	{
		get{return _id;}
		set{_id = value;}
	}
	public bool Buy
	{
		get{return _buy;}
		set{_buy = value;}
	}
	public bool Load
	{
		get{return _load;}
		set{_load = value;}
	}

	public string Content
	{
		get{return _path;}
		set{_path = value;}
	}
}