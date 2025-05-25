// See https://aka.ms/new-console-template for more information
int a = 10;
double b = a; //casting int into double 

double c = 9.8;
int d = (int)c;

WriteLine($"c is {c}. d is {d}.");

long e = 10;
int f = (int)e;
WriteLine($"e is {e}, f is {f}");

e = long.MaxValue;
f = (int)e;

WriteLine($"e is {e:N0}, f is {f:N0}");
