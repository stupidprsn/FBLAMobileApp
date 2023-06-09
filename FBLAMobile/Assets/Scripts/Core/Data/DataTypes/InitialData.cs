using System;

/// <summary>
///     Initial data that is loaded
///     when the app is opened.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/14/2023
/// </remarks>
[Serializable]
public class InitialData
{
    /// <summary>
    ///     Is the user signed in.
    /// </summary>
    public bool SignedIn { get; set; }

    /// <summary>
    ///     If the user is signed in, what
    ///     is their account ID.
    /// </summary>
    public byte AccountID { get; set; }

    /// <summary>
    ///     Constructor that saves sign in.
    /// </summary>
    /// <param name="id">The account to remember.</param>
    public InitialData(byte id)
    {
        SignedIn = true;
        AccountID = id;
    }

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public InitialData() 
    {
        SignedIn = false;
    }
}