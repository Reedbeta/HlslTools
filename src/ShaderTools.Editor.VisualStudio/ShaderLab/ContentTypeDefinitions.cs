﻿using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using ShaderTools.Editor.VisualStudio.Core;

namespace ShaderTools.Editor.VisualStudio.ShaderLab
{
    internal static class ContentTypeDefinitions
    {
        /// <summary>
        /// Definition of the primary HLSL content type.
        /// </summary>
        [Export]
        [Name(ContentTypeNames.ShaderLabContentType)]
        [BaseDefinition(ContentTypeNames.ShaderToolsContentType)]
        public static readonly ContentTypeDefinition ShaderLabContentTypeDefinition;
    }
}