using System;
using System.Xml;

namespace RSSFeeds
{
	/// <summary>
	/// Summary description for RSSFeedGenerator.
	/// </summary>
	public class RSSFeedGenerator
	{
		XmlTextWriter writer;

		public RSSFeedGenerator(System.IO.Stream stream, System.Text.Encoding encoding)
		{
			writer = new XmlTextWriter(stream, encoding);
			writer.Formatting = Formatting.Indented;
		}

		public RSSFeedGenerator( System.IO.TextWriter w)
		{
			writer = new XmlTextWriter(w);
			writer.Formatting = Formatting.Indented;
		}

		public void WriteStartDocument()
		{
			writer.WriteStartDocument();
			writer.WriteStartElement("rss");
			writer.WriteAttributeString("version", "2.0");
		}

		public void WriteEndDocument()
		{
			writer.WriteEndElement();
			writer.WriteEndDocument();
		}

		public void Close()
		{
			writer.Flush();
			writer.Close();
		}

		public void WriteStartChannel(string title, string link, string desc, string copyright, string webMaster)
		{
			writer.WriteStartElement("channel");
			writer.WriteElementString("title", title);
			writer.WriteElementString("link", link);
			writer.WriteElementString("description", desc);
			writer.WriteElementString("language", "en-us");
			writer.WriteElementString("copyright", copyright);
			writer.WriteElementString("generator", "RSS Generator v1.0");
			writer.WriteElementString("webMaster", webMaster);
			writer.WriteElementString("lastBuildDate", DateTime.Now.ToString("r"));
			writer.WriteElementString("ttl", "20");
		}

		public void WriteEndChannel()
		{
			writer.WriteEndElement();
		}

		public void WriteItem(string title, string link, string desc, string author, 
			DateTime publishedDate, string subject, string category)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", title);
			writer.WriteElementString("link", link);
			writer.WriteElementString("description", desc);
			writer.WriteElementString("author", author);
			writer.WriteElementString("link", publishedDate.ToString("r"));
			writer.WriteElementString("category", category);
			writer.WriteElementString("subject", subject);
			writer.WriteEndElement();
		}
	}
}
