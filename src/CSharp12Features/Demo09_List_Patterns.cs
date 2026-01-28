namespace CSharp12Features;

// ============================================================================
// DEMO 9: List Patterns
// ============================================================================
// Pattern matching on collections - very expressive for parsing,
// validation, and command processing.
// ============================================================================

public static class Demo09_List_Patterns
{
    public static void Run()
    {
        Utilities.LogDemoTitle("DEMO 9: List Patterns");

        #region List Pattern Basics

        // Exact match
        int[] numbers = [1, 2, 3]; // collection expression

        if (numbers is [1, 2, 3]) // list pattern
        {
            Console.WriteLine("Exact match: first = 1, second = 2, third = 3");
        }

        // Match with discard and variable capture (var to capture, _ to ignore)
        if (numbers is [1, var second, _])
        {
            Console.WriteLine($"Three total elements: first = 1, second = {second}");
        }

        // Slice patterns (.. = zero or more elements)
        numbers = [1, 2, 3, 4, 5, 6];
        if (numbers is [var first, .., var last])
        {
            Console.WriteLine($"At least two elements, first = {first}, last = {last}");
        }

        // Variable capture + Slice pattern
        if (numbers is [_, .. var middleItems, _])
        {
            Console.WriteLine($"At least two elements, middle values[{middleItems.Length}] = {string.Join(", ", middleItems)}");
        }

        #endregion

        #region Practical Example - Command Parsing

        Utilities.LogDemoTitle("Practical Example - Command Parsing");

        ParseCommand("help");
        ParseCommand("verify", "--bad", "param");
        ParseCommand("verify", "--state", "VA");
        ParseCommand("verify", "--state", "VA", "--license", "123456789");
        ParseCommand("batch", "validation", "--file", "licenses.csv");
        ParseCommand("config", "set", "timeout", "30");
        ParseCommand("config", "timeout");
        ParseCommand("unknown", "cmd", "param1", "param2");

        ParseCommand_Via_If("verify", "--state", "VA");

        ParseCommand_Via_Switch("verify", "--state", "VA");

        #endregion
    }

    static void ParseCommand(params string[] args) 
    {
        Console.WriteLine($"*** Parsing Command: \"{string.Join(" ", args)}\" ***");
        Console.WriteLine();

        Action action = args switch
        {
            [] => ShowHelp,
            ["help", ..] => ShowHelp,
            ["verify", "--state", var state] => () => VerifyState(state),
            ["verify", "--state", var state, "--license", var license] => () => VerifyLicense(state, license),
            ["verify", ..] => ShowVerifyError,
            ["batch", "validation", "--file", var file] => () => BatchValidation(file),
            ["config", "set", var key, var value] => () => SetConfig(key, value),
            ["config", ..] => ShowConfigError,
            [var cmd, ..] => () => ShowUnknownCommand(cmd),
        };
        action();

        Console.WriteLine();
    }

    static void ParseCommand_Via_If(params string[] args)
    {
        Console.WriteLine($"*** Parsing Command: \"{string.Join(" ", args)}\" ***");
        Console.WriteLine();

        if (args.Length == 0)
        {
            ShowHelp();
        }
        else if (args.Length >= 1 && args[0] == "help")
        {
            ShowHelp();
        }
        else if (args.Length == 3 && args[0] == "verify" && args[1] == "--state")
        {
            string state = args[2];
            VerifyState(state);
        }
        else if (args.Length == 5 && args[0] == "verify" && args[1] == "--state" && args[3] == "--license")
        {
            string state = args[2];
            string license = args[4];
            VerifyLicense(state, license);
        }
        else if (args.Length >= 1 && args[0] == "verify")
        {
            ShowVerifyError();
        }
        else if (args.Length == 4 && args[0] == "batch" && args[1] == "validation" && args[2] == "--file")
        {
            string file = args[3];
            BatchValidation(file);
        }
        else if (args.Length == 4 && args[0] == "config" && args[1] == "set")
        {
            string key = args[2];
            string value = args[3];
            SetConfig(key, value);
        }
        else if (args.Length >= 1 && args[0] == "config")
        {
            ShowConfigError();
        }
        else
        {
            string cmd = args[0];
            ShowUnknownCommand(cmd);
        }

        Console.WriteLine();
    }

    static void ParseCommand_Via_Switch(params string[] args)
    {
        Console.WriteLine($"*** Parsing Command: \"{string.Join(" ", args)}\" ***");
        Console.WriteLine();

        switch (args.Length)
        {
            case 0:
            case >= 1 when args[0] == "help":
                ShowHelp();
                break;
            case 3 when args[0] == "verify" && args[1] == "--state":
            {
                string state = args[2];
                VerifyState(state);
                break;
            }
            case 5 when args[0] == "verify" && args[1] == "--state" && args[3] == "--license":
            {
                string state = args[2];
                string license = args[4];
                VerifyLicense(state, license);
                break;
            }
            case >= 1 when args[0] == "verify":
                ShowVerifyError();
                break;
            case 4 when args[0] == "batch" && args[1] == "validation" && args[2] == "--file":
            {
                string file = args[3];
                BatchValidation(file);
                break;
            }
            case 4 when args[0] == "config" && args[1] == "set":
            {
                string key = args[2];
                string value = args[3];
                SetConfig(key, value);
                break;
            }
            case >= 1 when args[0] == "config":
                ShowConfigError();
                break;
            default:
            {
                string cmd = args[0];
                ShowUnknownCommand(cmd);
                break;
            }
        }

        Console.WriteLine();
    }

    static void ShowHelp()
    {
        Console.WriteLine("Available Commands:");
        Console.WriteLine("  help                                           - Display this help documentation");
        Console.WriteLine("  verify --state <state>                         - Verifies states participation");
        Console.WriteLine("  verify --state <state> --license <license>     - Verifies license number");
        Console.WriteLine("  batch validation --file <file>                 - Batch validation of licenses from file");
        Console.WriteLine("  config set <key> <value>                       - Set a configuration value");
    }

    static void ShowVerifyError()
    {
        Console.WriteLine("Error: Verify command requires --state parameter, and can include --license parameter");
        Console.WriteLine("  Usage: verify --state <state_code>");
        Console.WriteLine("  Usage: verify --state <state_code> --license <license_number>");
        Console.WriteLine("  Example: verify --state VA");
        Console.WriteLine("  Example: verify --state VA --license 123456789");
    }

    static void VerifyState(string state)
    {
        Console.WriteLine($"Verifying DLDV 3.0 state participation: {state}");
        Console.WriteLine($" {state} participates in DLDV 3.0.");
    }

    static void VerifyLicense(string state, string license)
    {
        Console.WriteLine($"Validating license: {license} with state: {state}");
        Console.WriteLine($"  License {license} is a match.");
    }

    static void BatchValidation(string file)
    {
        Console.WriteLine($"Batch importing from: {file}");
        Console.WriteLine("  Reading file...");
        Console.WriteLine("  Processing records...");
        Console.WriteLine("  Batch import completed successfully");
    }

    static void SetConfig(string key, string value)
    {
        Console.WriteLine($"Setting configuration: {key} = {value}");
        Console.WriteLine("  Updating configuration store...");
        Console.WriteLine("  Configuration updated successfully");
    }

    static void ShowConfigError()
    {
        Console.WriteLine("Error: Config command requires parameters");
        Console.WriteLine("  Usage: config set <key> <value>");
        Console.WriteLine("  Example: config set timeout 30");
    }

    static void ShowUnknownCommand(string cmd)
    {
        Console.WriteLine($"Unknown command: {cmd}");
        Console.WriteLine("  Type 'help' to see available commands");
    }
}