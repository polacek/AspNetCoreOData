// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Common;
using Microsoft.AspNetCore.OData.Formatter.Value;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNetCore.OData.Formatter.Deserialization
{
    /// <summary>
    /// This class encapsulates the state and settings that get passed to <see cref="ODataDeserializer"/>.
    /// </summary>
    public class ODataDeserializerContext
    {
        private bool? _isDeltaOfT;
        private bool? _isNoClrType;

        /// <summary>
        /// Gets or sets the type of the top-level object the request needs to be deserialized into.
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IEdmTypeReference"/> of the top-level object the request needs to be deserialized into.
        /// </summary>
        public IEdmTypeReference ResourceEdmType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ODataPath"/> of the request.
        /// </summary>
        public ODataPath Path { get; set; }

        /// <summary>
        /// Gets or sets the EDM model associated with the request.
        /// </summary>
        public IEdmModel Model { get; set; }

        /// <summary>
        /// Gets or sets the HTTP Request whose response is being serialized.
        /// </summary>
        public HttpRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TimeZoneInfo"/>.
        /// </summary>
        public TimeZoneInfo TimeZone { get; set; }

        internal bool IsDeltaOfT
        {
            get
            {
                if (!_isDeltaOfT.HasValue)
                {
                    _isDeltaOfT = ResourceType != null && TypeHelper.IsGenericType(ResourceType) && ResourceType.GetGenericTypeDefinition() == typeof(Delta<>);
                }

                return _isDeltaOfT.Value;
            }
        }

        internal bool IsNoClrType
        {
            get
            {
                if (!_isNoClrType.HasValue)
                {
                    _isNoClrType = TypeHelper.IsTypeAssignableFrom(typeof(IEdmObject), ResourceType) ||
                        typeof(ODataUntypedActionParameters) == ResourceType;
                }

                return _isNoClrType.Value;
            }
        }

        internal IEdmTypeReference GetEdmType(Type type)
        {
            if (ResourceEdmType != null)
            {
                return ResourceEdmType;
            }

            return EdmLibHelper.GetExpectedPayloadType(type, Path, Model);
        }
    }
}
