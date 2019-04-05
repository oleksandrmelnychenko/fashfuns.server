﻿using System.Collections.Generic;

namespace FashFuns.Domain.EntityHelpers
{
    public sealed class IdentityResponse
    {
        public bool Succeeded { get; set; }

        public List<ErrorItem> Errors { get; set; } = new List<ErrorItem>();
    }
}
