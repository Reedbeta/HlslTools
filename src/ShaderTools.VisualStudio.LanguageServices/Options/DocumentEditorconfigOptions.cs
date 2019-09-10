using System;
using System.Collections.Immutable;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.OptionsExtensionMethods;
using ShaderTools.CodeAnalysis;
using ShaderTools.CodeAnalysis.Formatting;
using ShaderTools.CodeAnalysis.Options;
using ShaderTools.Utilities;

namespace ShaderTools.VisualStudio.LanguageServices.Implementation.Options
{
    // Hooks up an ITextView's IEditorOptions so we can get .editorconfig settings.
    internal sealed class DocumentEditorconfigOptions : IDocumentOptions
    {
        private readonly WeakReference<ITextBuffer> _weakTextBuffer;

        public DocumentEditorconfigOptions(ITextBuffer textBuffer)
        {
            _weakTextBuffer = new WeakReference<ITextBuffer>(textBuffer);
        }

        public bool TryGetDocumentOption(Document document, Microsoft.CodeAnalysis.Options.OptionKey option, OptionSet underlyingOptions, out object value)
        {
            // @reedbeta TODO: Can/should we look up the workspace from the Document somehow, rather than using PrimaryWorkspace?
            // @reedbeta TODO: Should we look up the ITextBuffer from the Document each time this method is called, rather than caching it?

            var workspace = PrimaryWorkspace.Workspace as VisualStudioWorkspace;
            var textViews = workspace?.GetTextViewsForBuffer(_weakTextBuffer.GetTarget()) ?? ImmutableArray<ITextView>.Empty;
            if (textViews.IsDefaultOrEmpty)
            {
                value = null;
                return false;
            }

            // Just take the first view's options.
            // Is it ever possible for editorconfig options to differ between views of the same file? I can't think why they would.
            // MSDN implies that editorconfig options are also available directly on the ITextBuffer, but I can't figure out where.
            var editorOptions = textViews[0].Options;

            // Check if the OptionKey is one of the ones we want to override with values from the IEditorOptions
            if (option.Option == FormattingOptions.UseTabs)
            {
                value = !editorOptions.IsConvertTabsToSpacesEnabled();
                return true;
            }
            else if (option.Option == FormattingOptions.TabSize)
            {
                value = editorOptions.GetTabSize();
                return true;
            }
            else if (option.Option == FormattingOptions.IndentationSize)
            {
                value = editorOptions.GetIndentSize();
                return true;
            }
            else if (option.Option == FormattingOptions.NewLine)
            {
                value = editorOptions.GetNewLineCharacter();
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
