using System;
using System.IO;
using System.Xml;
using static System.Console;
using static System.IO.Path;
using static System.Environment;
using System.IO.Compression;

namespace WorkingWithStreams
{
    class Program
    {
        static string[] callSignals = new string[]{"Husker", "Starbuck", "Apollo", "Boomer","Bulldog", "Athena", "Helo", "Racetrack"};

        static void WorkWithText()
        {
            string textFile = Combine(CurrentDirectory, "streams.txt");
            StreamWriter text = File.CreateText(textFile);

            foreach (string item in callSignals)
            {
                text.WriteLine(item);
            }
            text.Close();

            WriteLine($"{textFile} contains {new FileInfo(textFile).Length} bytes");
            WriteLine(File.ReadAllText(textFile));
        }
        static void Main(string[] args)
        {
            //WorkWithText();
            WorkWithXml();
            WorkingWithCompression();
            WorkingWithCompression(useBrotli: false);
        }

        static void WorkWithXml()
        {
            FileStream xmlFileStream = null;
            XmlWriter xmlWriter = null;

            try
            {
                string xmlFile = Combine(CurrentDirectory, "streams.xml");
                xmlFileStream = File.Create(xmlFile);

                xmlWriter = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("callsigns");

                foreach (var item in callSignals)
                {
                    xmlWriter.WriteElementString("callsign", item);
                }
                xmlWriter.WriteEndElement();

                xmlWriter.Close();
                xmlFileStream.Close();

                WriteLine($"{xmlFile} contains {xmlFile.Length}bytes");
                WriteLine(File.ReadAllText(xmlFile));
            }
            catch (Exception ex)
            {
                
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Dispose();
                    WriteLine("The XML writer's unmanaged resources have been disposed.");
                }
                if (xmlFileStream != null)
                {
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resources have been disposed.");
                }
            }
        }

        static void WorkingWithCompression(bool useBrotli = true)
        {
            string fileExt = useBrotli ? "brotli" :"gzip";
            string gzipFilePath = Combine(CurrentDirectory,$"streams.{fileExt}");
            FileStream fileStream = File.Create(gzipFilePath);

            Stream compressor;
            if (useBrotli)
            {
                compressor = new BrotliStream(fileStream, CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(fileStream, CompressionMode.Compress);
            }
            using (compressor)
            {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");

                    foreach (string item in callSignals)
                    {
                        xmlGzip.WriteElementString("callsign",item);
                    }
                }
            }

            WriteLine("{0} contains {1:N0} bytes.",gzipFilePath, new FileInfo(gzipFilePath).Length);
            WriteLine($"The compressed contents:");
            WriteLine(File.ReadAllText(gzipFilePath));

            //Reading From a compressed File
            WriteLine("Reading from the compressed file: ");
            fileStream = File.Open(gzipFilePath, FileMode.Open);

            Stream decompressor;
            if (useBrotli)
            {
                decompressor = new BrotliStream(fileStream, CompressionMode.Decompress);
            }
            else{
                decompressor = new GZipStream(fileStream, CompressionMode.Decompress);
            }
            using (decompressor)
            {
                using(XmlReader xml = XmlReader.Create(decompressor))
                {
                    while (xml.Read()) // read the next XML node
                    {
                        // check if we are on an element node named callsign
                        if ((xml.NodeType == XmlNodeType.Element)
                        && (xml.Name == "callsign"))
                        {
                            xml.Read(); // move to the text inside element
                            WriteLine($"{xml.Value}"); // read its value
                        }
                    }
                }
            }
            
        }
    }
}
