﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Text;
using ShaderTools.CodeAnalysis;

namespace ShaderTools.VisualStudio.LanguageServices.Implementation.F1Help
{
    internal interface IHelpContextService : ILanguageService
    {
        string Language { get; }
        string Product { get; }

        Task<string> GetHelpTermAsync(Document document, TextSpan textSpan, CancellationToken cancellationToken);
    }
}
