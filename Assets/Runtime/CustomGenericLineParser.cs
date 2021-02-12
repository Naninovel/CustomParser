using Naninovel;
using Naninovel.Commands;
using Naninovel.Parsing;

namespace Runtime
{
    public class CustomGenericLineParser : Naninovel.GenericTextLineParser
    {
        private float printSpeed = -1;

        protected override GenericTextScriptLine Parse (GenericTextLine lineModel)
        {
            // Extract print speed from the model.
            if (lineModel.AuthorIdentifier?.Text.Contains("-") ?? false)
            {
                ParseUtils.TryInvariantFloat(lineModel.AuthorIdentifier.Text.GetAfter("-"), out printSpeed);
                lineModel.AuthorIdentifier.Text = lineModel.AuthorIdentifier.Text.GetBeforeLast("-");
            }
            return base.Parse(lineModel);
        }

        protected override void AddCommand (Naninovel.Command command)
        {
            if (printSpeed > 0 && command is PrintText print)
                print.RevealSpeed = printSpeed / 100;
            base.AddCommand(command);
        }
    }
}
