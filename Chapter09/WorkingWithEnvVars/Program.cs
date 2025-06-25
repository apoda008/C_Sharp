SectionTitle("Reading all environment variables for process");
IDictionary vars = GetEnvironmentVariables();
DictionaryToTable(vars);

SectionTitle("Reading all environment variables for machine");
IDictionary varsMachine = GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
DictionaryToTable(varsMachine);

SectionTitle("Reading all environment variables for user");
IDictionary varsUser = GetEnvironmentVariables(EnvironmentVariableTarget.User);
DictionaryToTable(varsUser);

//EXPANDING, SETTING, AND GETTING AN ENVIRONMENT VARIABLE 
/*
 * set command defines a temp env var but after defining it you must close the current 
 * shell or session and restart the shell for the env var to be read. Note that it does 
 * not use an equal sign to assign value
 */

string myComputer = "My username is %USERNAME%. My SPU is %PROCESSOR_IDENTIFIER%.";

WriteLine(ExpandEnvironmentVariables(myComputer));

string password_key = "MY_PASSWORD";

SetEnvironmentVariable(password_key, "Pa$$w0rd");

string? password = GetEnvironmentVariable(password_key);

WriteLine($"{password_key} is {password}");

/*
 * In a real world app, you might pass an argument to the console that is then used to 
 * set the process scope environment var on startup for reading later in the process 
 * lifetime
 */

string secret_key = "MY_SECRET";

string? secret = GetEnvironmentVariable(secret_key,
    EnvironmentVariableTarget.Process);
WriteLine($"Process - {secret_key} is {secret}");

secret = GetEnvironmentVariable(secret_key,
    EnvironmentVariableTarget.Machine);
WriteLine($"Machine - {secret_key} is {secret}");

secret = GetEnvironmentVariable(secret_key,
    EnvironmentVariableTarget.User);
WriteLine($"User - {secret_key} is {secret}");