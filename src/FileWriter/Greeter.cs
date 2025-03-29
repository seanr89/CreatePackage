namespace FileWriter;

/// <summary>
/// A simple class that provides a greeting.
/// </summary>
public class Greeter
{
    /// <summary>
    /// Returns a standard "Hello, World!" greeting.
    /// </summary>
    /// <returns>The greeting string.</returns>
    // public static string Greet()
    // {
    //     return "Hello, World!";
    // }

    /// <summary>
    /// Returns a personalized greeting.
    /// </summary>
    /// <param name="name">The name of the person to greet.</param>
    /// <returns>A personalized greeting string.</returns>
    public static string Greet(string name)
    {
        return $"Hello, {name}!";
    }
}
