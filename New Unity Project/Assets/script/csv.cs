using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class csv
{
	/*
	 * --reader--
	 * reader = csv.reader(csvfile, delimiter=',')
	 * for row in reader.rows:
	 *     print(row["first_name"], row["last_name"])  # John, Cleese
	 *     
	 * --writer--
	 * writer = csv.writer(csvfile, delimiter=',')
	 * writer.writeheader(['姓名', '身高', '體重'])
	 * writer.writerow(['令狐沖', 175, 60])
	*/
	public class reader
	{
		protected string csvfile;
		protected char delimiter;
		public readonly Dictionary<string, int> header = new Dictionary<string, int>();
		public List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>(); // rows uses the same key in header

		protected virtual void NotCsv()
		{
			throw new Exception("File is not a csv file, or is empty.");
		}

		public reader(string csvfile, char delimiter = ',') //constructor
		{
			this.csvfile = csvfile;
			this.delimiter = delimiter;

			try
			{
				using (StreamReader file = new StreamReader(csvfile))
				{
					string line;
					Dictionary<string, string> row = new Dictionary<string, string>();
					#region Header
					if ((line = file.ReadLine()) != null)
					{
						string[] key = line.Split(delimiter);
						foreach (string k in key)
						{
							header.Add(k, header.Count);
						}
					}
					else
					{
						NotCsv();
					}

					#endregion
					#region rows
					while ((line = file.ReadLine()) != null)
					{
						string[] value = line.Split(delimiter);
						foreach (string k in header.Keys)
						{
							row.Add(k, value[header[k]]);
						}
						rows.Add(row);
					}
					#endregion
				}
			}
			catch(FileNotFoundException)
			{
				NotCsv();
			}
		}
	}

	public class writer : reader
	{
		bool is_new_file = false;
		public writer(string csvfile, char delimiter = ',') : base(csvfile, delimiter) {}

		protected override void NotCsv() { is_new_file = true; }

		public void writeheader(string[] keys) // only called when create new csv
		{
			if (is_new_file)
			{
				is_new_file = false;
				foreach (string k in keys)
				{
					header.Add(k, header.Count);
				}
				writerow(keys); // write header at the first line
			}
			else
			{
				throw new Exception("Can not change csv header that has already existed.");
			}
		}

		public void writerow(string[] value)
		{
			if (!is_new_file)
			{
				FileStream fs;
				if (File.Exists(csvfile))
				{
					fs = new FileStream(csvfile, FileMode.Append);
				}
				else
				{
					fs = new FileStream(csvfile, FileMode.CreateNew);
				}

				using (StreamWriter sw = new StreamWriter(fs))
				{
					var sb = new System.Text.StringBuilder();
					foreach (string v in value)
					{
						sb.Append(v);
						sb.Append(delimiter);
					}
					sw.WriteLine(sb.ToString());
				}

				fs.Close();
			}
			else
			{
				throw new Exception("Csv has no header. Please write header first.");
			}
		}
		
	}

}
