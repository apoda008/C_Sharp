using Microsoft.Win32.SafeHandles;
using System.Text;

using SafeFileHandle handle =
    File.OpenHandle(path: "coffee.txt",
    mode: FileMode.OpenOrCreate,
    access: FileAccess.ReadWrite);

string message = "Cafe 4.39";
ReadOnlyMemory<byte> buffer = new(Encoding.UTF8.GetBytes(message));
await RandomAccess.WriteAsync(handle, buffer, fileOffset: 0);

long length = RandomAccess.GetLength(handle);
Memory<byte> contentBytes = new(new byte[length]);
await RandomAccess.ReadAsync(handle, contentBytes, fileOffset: 0);
string content = Encoding.UTF8.GetString(contentBytes.ToArray());
WriteLine($"COntent of file: {content}");

/*
 In most cases today, UTF-8 is a good default, which is why it is literally 
the default encoding, that is, Encoding.Default. You should avoid using 
Encoding UTF-7 because it is not secure. Due to this C# compiler will warn 
you when you try to use UTF7. of course you might need to generate text using 
that encoding for compatibility with another system, so it need sto remain an 
option in .net
 
 */
