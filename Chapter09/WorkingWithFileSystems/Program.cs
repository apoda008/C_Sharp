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

