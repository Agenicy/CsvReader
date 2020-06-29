A simple script in Unity to read/write csv file.

*You can't use this script to delete old csv data.*

# How To Use
Just see the .cs file under `CsvReader\New Unity Project\Assets\script\CsvHandler.cs`, and you'll know how to use.

## Read

* create reader

        var new_reader = new csv.reader("filepath/filename.csv");

* read

    You can use `new_reader.rows` to get all data. Just like this:

        
        foreach(var row in new_reader.rows)
		{
			print( row["Name"] +' '+  row["Age"]);
		}

    The first line of Csv file will be seen as `KEYS` (like "Name" and "Age" above), and `reader.rows` will ignore them.

## Write

* create writer

        var new_writer = new csv.writer("filepath/filename.csv");

* write header

    If you want to create a new file, you must to write header first.

        string[] head = { "one", "two" };
	    new_writer.writeheader(head);

    Note that you can't write header is an existed file.

* write data

    You can't write csv data in a new file, you must write header first, then you can append new data.

    You can also append new data after an old file.

		string[] n = { "hey", "hello" };
		c.writerow(n);