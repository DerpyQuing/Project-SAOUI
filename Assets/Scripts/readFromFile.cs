using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class readFromFile {

	public List<string> results = new List<string>();

	public List<string> Load(string fileName) {
		try {
			string line;
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			using (theReader) {
				do {
					line = theReader.ReadLine();
					if (line != null && line != "----------") {
						results.Add(line);
					}
				}
				while (line != null);
				theReader.Close();
				return results;
			}
		} catch (Exception e) {
			//Console.WriteLine("{0}\n", e.Message);
			return null;
		}
	}
}