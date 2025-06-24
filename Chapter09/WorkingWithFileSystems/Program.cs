using Spectre.Console; //to use table 

#region Handling cross-platform environments and filesystems 

SectionTitle("Handling cross-platform envonments and filesystems");

//create a spectre console table 
Table table = new();

//add two columns with markup for colors 
table.AddColumn("[blue]MEMBER[/]");
table.AddColumn("[blue]VALUE[/]");

//add rows
table.AddRow("Path.PathSeperator", PathSeparator.ToString());
table.AddRow("Path.DirectorySeparatorChar", DirectorySeparatorChar.ToString());
table.AddRow("Directory.GetCurrentDirectory()", GetCurrentDirectory());
table.AddRow("Environment.CurrentDirectory", CurrentDirectory);
table.AddRow("Environment.SystemDirectory", SystemDirectory);
table.AddRow("Path.GetTempPath()", GetTempPath());
table.AddRow("");
table.AddRow("GetFolderPath(SpecialFolder", "");
table.AddRow("   .System", GetFolderPath(SpecialFolder.System));
table.AddRow("   .ApplicationData", GetFolderPath(SpecialFolder.ApplicationData));
table.AddRow("   .MyDocuments", GetFolderPath(SpecialFolder.MyDocuments));
table.AddRow("   .Personal", GetFolderPath(SpecialFolder.Personal));

//remeber the table to the console
AnsiConsole.Write(table);

/*
 The Environment type has many other useful members that we did not use in this code, 
incluiding the OSVersion and ProcessorCount props

Windows uses backslashes (\) as directory separators, Mac and Linux use forward slashes 
(/). Do not assume what character is used in your code when combining paths; use 
Path.DirectorySeparatorChar or Path.PathSeparator instead.
 */
#endregion

//DRIVES
SectionTitle("Managing Drives");
Table drives = new();

drives.AddColumn("[blue]NAME[/]");
drives.AddColumn("[blue]TYPE[/]");
drives.AddColumn("[blue]FORMAT[/]");
drives.AddColumn(new TableColumn("[blue]SIZE (BYTES)[/]").RightAligned());
drives.AddColumn(new TableColumn("[blue]FREE SPACE (BYTES)[/]").RightAligned());

foreach(DriveInfo drive in DriveInfo.GetDrives())
{
    if (drive.IsReady) 
    { 
    drives.AddRow(drive.Name, drive.DriveType.ToString(),
        drive.DriveFormat,
        drive.TotalSize.ToString("N0"),
        drive.AvailableFreeSpace.ToString("N0"));
    }
    else
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(), 
            string.Empty, string.Empty, string.Empty);

    }
}

AnsiConsole.Write(drives);
//Check that a drive is ready before reading props such as TotalSize and
//AvailableFreeSpace or you will see an exception thrown with removable drivers

//MANAGING DIRECTORIES
SectionTitle("Managing Directories");

string newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "New Folder");

WriteLine($"Working with; {newFolder}");

//we must explicitly say which Exists method to use 
//because we statically imported both Path and Directory
WriteLine($"Does it exist? {Path.Exists(newFolder)}");

WriteLine("Creating it...");
CreateDirectory(newFolder);

//Let's use the Directory.Exists method this time 
WriteLine($"Does it exist? {Directory.Exists(newFolder)}");
Write("Confirm the directory exists, and then press any ley...");
ReadKey(intercept: true);

WriteLine("Deleting it...");
Delete(newFolder, recursive: true);
WriteLine($"Does it exist? {Directory.Exists(newFolder)}");

/*
 in .NET6 and earlier, only the Directory class had an Exists method, In .NET 7 
or later the Path class also has an exists method. Bot can be used to check 
 */

//MANAGING FILES
SectionTitle("Managing Files");

//Define a directory path to output filers starting in the users folder
string dir = Combine(GetFolderPath(SpecialFolder.Personal), "OutputFiles");

CreateDirectory(dir);

//define file paths 
string textFile = Combine(dir, "dummy.txt");
string backupFile = Combine(dir, "dummy.bak");

WriteLine($"Working with: {textFile}");

WriteLine($"Does it exist? {File.Exists(textFile)}");

//create a new file and write a line to it 
StreamWriter textWriter = File.CreateText(textFile);
textWriter.WriteLine("Hello C#!");
textWriter.Close(); //close file and release resources 
WriteLine($"Does it exist? {File.Exists(textFile)}");

//copy the file, and overwrite if it already exists
File.Copy(sourceFileName: textFile,
    destFileName: backupFile,
    overwrite: true);

WriteLine($"does {backupFile} exist? {File.Exists(backupFile)}");

Write("confirm the files exist, and then press any key...");
ReadKey(intercept: true);

//delete the files
File.Delete(textFile);
WriteLine($"Does it exist? {File.Exists(textFile)}");

//read from the text file backup 
WriteLine($"Reading content from {backupFile}");
StreamReader textReader = File.OpenText(backupFile);
WriteLine(textReader.ReadLine());
textReader.Close(); //close file and release resources

//MANAGEING PATHS
SectionTitle("Managing Paths");

WriteLine($"Folder name: {GetDirectoryName(textFile)}");
WriteLine($"File name: {GetFileName(textFile)}");
WriteLine("FIle Name without Extension: {0}", 
    GetFileNameWithoutExtension(textFile));
WriteLine($"File Extension: {GetExtension(textFile)}");
WriteLine($"Random File Name: {GetRandomFileName()}");
WriteLine($"Temporary File Name: {GetTempFileName()}");

/*
 GetTempFileName creates a zero-byte file and returns its name, ready for you 
to use. GetRandomFileName just returns a filename; it doesnt create the file
*/

//GETTING FILE INFORMATION 
SectionTitle("Getting File Information");

FileInfo info = new(backupFile);
WriteLine($"{backupFile}:");
WriteLine($"  COntains: {info.Length} bytes");
WriteLine($"  Last accessed: {info.LastAccessTime}");
WriteLine($"  Has readonly set to {info.IsReadOnly}");

//R/W with Streams (ABstract and Concrete streams)

//System.IO FileStream Bytes Stored in the filesystem 
//System.IO MemoryStream bytes stored in the mem in the process 
//System.Net.Sockets NetworkStream bytes stored at a network location 

//System.Security.Cryptography CryptoStream this encrypts or decrypts the stream 
//System.IO.Compression GzipStream, DeflateStream These compress and decompress the stream 
//System.Net.Security AuthenticatedStream This sends credentials accross the stream 

//HELPER STREAMS
//System.IO StreamReader This reads from the underlying stream as plain text 
//System.IO StreamWriter This writes to the underlying stream as plain text 

//SystemIO BinaryReader This reads from streams as .NET types. For example,
//                      the ReadDecimal method reads the next 16 bytes from
//                      the underlying stream as a decimal value and the
//                      REadInt32 method reads the next 4 bytes as an int value 

//System.IO BinaryWriter This mwrites to streams as.net types. For example, the write method
//                      with a decimal parameter writes 16 bytes to the underlying stream,
//                      and the Write method with an int para writes 4 bytes

//System.Xml XmlReader This reads from the underlying stream using the XML format 
//System.Xml XmlWriter This writes to the underlying stream using the XML format

//StREAM PIPPING 

/*
 WriteLine("Hello") => StreamWriter => CrytoStream => GzipStream => FileStream [G7x]
 */