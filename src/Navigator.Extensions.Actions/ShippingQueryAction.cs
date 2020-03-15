﻿using Navigator.Abstraction;
using Navigator.Actions;

namespace Navigator.Extensions.Actions
{
    public abstract class ShippingQueryAction : Action
    {
        public override string Type => ActionType.ShippingQuery;
    }
}