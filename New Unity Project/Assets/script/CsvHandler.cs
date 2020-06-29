using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsvHandler : MonoBehaviour
{
	[SerializeField]
	Text t;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void write()
	{
		var c = new csv.writer("./myfile.csv");
		string[] s = { "one", "two" };
		c.writeheader(s);
		string[] n = { "hey", "hello" };
		c.writerow(n);
	}

	public void read()
	{
		var r = new csv.reader("./myfile.csv");
		foreach(var row in r.rows)
		{
			t.text += row["one"] +' '+  row["two"] + '\n';
		}
	}
}
