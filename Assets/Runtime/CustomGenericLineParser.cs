using Naninovel;
using Naninovel.Commands;
using Naninovel.Parsing;

public class CustomGenericLineParser : Naninovel.GenericTextLineParser
{
    private float printSpeed;

    protected override GenericTextScriptLine Parse (GenericTextLine lineModel)
    {
        // Reset current print speed.
        printSpeed = -1;

        // Try extract print speed from the model.
        if (lineModel.AuthorIdentifier?.Text.Contains("-") ?? false)
        {
            ParseUtils.TryInvariantFloat(lineModel.AuthorIdentifier.Text.GetAfter("-"), out printSpeed);
            // Remove print speed from the author ID.
            lineModel.AuthorIdentifier.Text = lineModel.AuthorIdentifier.Text.GetBeforeLast("-");
        }
        return base.Parse(lineModel);
    }

    protected override void AddCommand (Naninovel.Command command)
    {
        // When assigned, set the speed to all the print commands in the line.
        if (printSpeed > 0 && command is PrintText print)
            print.RevealSpeed = printSpeed / 100;
        base.AddCommand(command);
    }
}
