using Naninovel;
using Runtime;

public class CustomParser : ScriptParser
{
    protected override GenericTextLineParser GenericTextLineParser { get; } = new CustomGenericLineParser();
}
