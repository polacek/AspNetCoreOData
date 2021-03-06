﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License.  See License.txt in the project root for license information.

using Microsoft.AspNetCore.OData.Query;
using System;
using Xunit;

namespace Microsoft.AspNetCore.OData.Tests.Query
{
    public class AllowedLogicalOperatorsTests
    {
        [Fact]
        public void None_MatchesNone()
        {
            Assert.Equal(AllowedLogicalOperators.None, AllowedLogicalOperators.All & AllowedLogicalOperators.None);
        }

        [Fact]
        public void All_Contains_AllLogicalOperators()
        {
            AllowedLogicalOperators allLogicalOperators = 0;
            foreach (AllowedLogicalOperators allowedLogicalOperator in Enum.GetValues(typeof(AllowedLogicalOperators)))
            {
                if (allowedLogicalOperator != AllowedLogicalOperators.All)
                {
                    allLogicalOperators = allLogicalOperators | allowedLogicalOperator;
                }
            }

            Assert.Equal(AllowedLogicalOperators.All, allLogicalOperators);
        }
    }
}
