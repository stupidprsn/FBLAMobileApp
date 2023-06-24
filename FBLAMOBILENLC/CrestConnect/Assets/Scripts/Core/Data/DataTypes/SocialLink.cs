using System;
/// <summary>
///     Stores information regarding the different social media accounts.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/20/23
/// </remarks>
[Serializable]
public class SocialLink
{
    /// <summary>
    ///     The type of social platform.
    /// </summary>
    public SocialTypes SocialType { get; private set; }
    /// <summary>
    ///     The link to the social platform.
    /// </summary>
    public string SocialURL { get; set; }

    public SocialLink(SocialTypes socialType, string url)
    {
        this.SocialType = socialType;
        this.SocialURL = url;
    }
}
