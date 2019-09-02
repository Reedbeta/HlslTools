using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualStudio.Text.Editor.OptionsExtensionMethods;
using ShaderTools.CodeAnalysis.Options;

namespace ShaderTools.CodeAnalysis.EditorFeatures.Options
{
    using OptionSet = CodeAnalysis.Options.OptionSet;

    // Hooks up an ITextView's IEditorOptions so we can get .editorconfig settings.
    internal sealed class DocumentEditorOptions : IDocumentOptions
    {
        private readonly Microsoft.VisualStudio.Text.Editor.IEditorOptions _editorOptions;

        public DocumentEditorOptions(Microsoft.VisualStudio.Text.Editor.ITextView textView)
        {
            _editorOptions = textView.Options;
        }

        public bool TryGetDocumentOption(Document document, OptionKey option, OptionSet underlyingOptions, out object value)
        {
            // Check if the OptionKey is one of the ones we want to override with values from the IEditorOptions
            if (option.Option == Formatting.FormattingOptions.UseTabs)
            {
                value = !_editorOptions.IsConvertTabsToSpacesEnabled();
                return true;
            }
            else if (option.Option == Formatting.FormattingOptions.TabSize)
            {
                value = _editorOptions.GetTabSize();
                return true;
            }
            else if (option.Option == Formatting.FormattingOptions.IndentationSize)
            {
                value = _editorOptions.GetIndentSize();
                return true;
            }
            else if (option.Option == Formatting.FormattingOptions.NewLine)
            {
                value = _editorOptions.GetNewLineCharacter();
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
