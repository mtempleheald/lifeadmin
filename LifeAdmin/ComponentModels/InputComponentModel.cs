namespace LifeAdmin.ComponentModels;

public class InputComponentModel : ConditionalDisplayModel
{
    /// <summary>
    /// Each input component in a journey needs a unique identifier, used for integration and conditional rendering
    /// </summary>
    public required string? Key { get; set; }

    /// <summary>
    /// An input component must return a single value
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// A value must be contextual, the label is designed to convey that to the user
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Provide an example value to help the user understand the input expectations
    /// </summary>
    public string? Placeholder { get; set; }

    /// <summary>
    /// Provide a default error message if the input is invalid
    /// </summary>
    public string? DefaultErrorMessage { get; set; }

    /// <summary>
    /// Provide some additional information, typically behind a popover
    /// </summary>
    public string? HelpText { get; set; }

    /// <summary>
    /// Basic validation
    /// </summary>
    public bool? Required { get; set; }

    /// <summary>
    /// Distinguish between different subtypes of InputComponent
    /// </summary>
    public InputComponentType SubType { get; set; }

}

/// <summary>
/// Support all HTML input types which capture a single value<br/>
/// If we wish to favour more optimised components instead we can
/// </summary>
public enum InputComponentType
{
    Text,
    Number,
    Email,
    Tel,
    Date,
    Year,
    Month,
    Datetime, // datetime-local
    Week,
    Time,
    Colour, // color
    Url,
    Password,
    //File, // Requires special treatment
    //Range, // Requires bounds
}
