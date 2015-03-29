using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

class B64EncodeDecodeUtil
{
	// STAThreadAttribute is for using the clipboard; see:
	// https://msdn.microsoft.com/en-us/library/kz40084e%28v=vs.110%29.aspx
	[STAThreadAttribute]
	static public int Main(string [] args)
	{
		int ret = 0;

		if (args.Length < 1 || args.Length > 2)
		{
			ret = 1; // Help needed due to unexpected number of arguments
			Usage();
		}
		else
		{
			try
			{

				if (args.Length == 2 && args[0] == "-e")
				{
					Console.WriteLine(Encode(args[1]));
				}
				else if (args.Length == 2 && args[0] == "-d")
				{
					OutputRaw(Decode(args[1]));
				}
				else if (args.Length == 1 && args[0] == "-ei")
				{
					Console.WriteLine(Encode(Console.ReadLine()));
				}
				else if (args.Length == 1 && args[0] == "-di")
				{
					OutputRaw(Decode(Console.ReadLine()));
				}
				else if (args.Length == 1 && args[0] == "-ec")
				{
					String text = Clipboard.GetText(TextDataFormat.Text);
					Clipboard.SetText(Encode(text), TextDataFormat.Text);
				}
				else if (args.Length == 1 && args[0] == "-dc")
				{
					String text = Clipboard.GetText(TextDataFormat.Text);
					// Some research might turn up better options than this?
					text = Encoding.UTF8.GetString(Decode(text));
					Clipboard.SetText(text, TextDataFormat.Text);
				}
				else if (args.Length == 1 && args[0] == "-h")
				{
					// Help explicitly requested
					Usage();
				}
				else
				{
					ret = 1; // Help needed due invalid arguments
					Usage();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error transforming text: {0}", e.Message);
				ret = 1;
			}
		}

		return ret;
	}

	static public void Usage()
	{
		Console.WriteLine("Help:");
		Console.WriteLine("  b64 -h");
		Console.WriteLine("");

		Console.WriteLine(
			"Encode and decode a value supplied on the command line:");
		Console.WriteLine("  b64 -e plaintext");
		Console.WriteLine("  b64 -d base64");
		Console.WriteLine("");

		Console.WriteLine("Encode and decode from/to the clipboard:");
		Console.WriteLine("  b64 -ec");
		Console.WriteLine("  b64 -dc");
		Console.WriteLine("");

		Console.WriteLine("Encode and decode standard input to console");
		Console.WriteLine("  b64 -ei");
		Console.WriteLine("  b64 -di");
	}

	static public string Encode(string text)
	{
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
	}

	static public byte [] Decode(string base64)
	{
		return Convert.FromBase64String(base64);
	}

	static public void OutputRaw(byte [] bytes)
	{
		// http://stackoverflow.com/questions/111387/how-to-stream-binary-data-to-standard-output-in-net
		Stream rawCon = Console.OpenStandardOutput();
		rawCon.Write(bytes, 0, bytes.Length);
	}
}
