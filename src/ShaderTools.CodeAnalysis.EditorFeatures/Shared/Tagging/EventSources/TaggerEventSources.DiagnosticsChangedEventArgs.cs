﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.VisualStudio.Text;
using ShaderTools.CodeAnalysis.Diagnostics;
using ShaderTools.CodeAnalysis.Editor.Tagging;
using ShaderTools.CodeAnalysis.Text;

namespace ShaderTools.CodeAnalysis.Editor.Shared.Tagging
{
    internal partial class TaggerEventSources
    {
        private class DiagnosticsChangedEventSource : AbstractTaggerEventSource
        {
            private readonly ITextBuffer _subjectBuffer;
            private readonly IDiagnosticService _service;

            public DiagnosticsChangedEventSource(ITextBuffer subjectBuffer, IDiagnosticService service, TaggerDelay delay)
                : base(delay)
            {
                _subjectBuffer = subjectBuffer;
                _service = service;
            }

            private void OnDiagnosticsUpdated(object sender, DiagnosticsUpdatedEventArgs e)
            {
                var document = _subjectBuffer.AsTextContainer().GetOpenDocumentInCurrentContext();

                if (document != null && document.Id == e.Document.Id)
                {
                    this.RaiseChanged();
                }
            }

            public override void Connect()
            {
                _service.DiagnosticsUpdated += OnDiagnosticsUpdated;
            }

            public override void Disconnect()
            {
                _service.DiagnosticsUpdated -= OnDiagnosticsUpdated;
            }
        }
    }
}
