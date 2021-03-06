﻿namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DivideExpression : BinaryExpression
    {
        public DivideExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue is int)
                if (rightvalue is int)
                    return (double)((int)leftvalue) / (int)rightvalue;
                else
                    return (int)leftvalue / (double)rightvalue;
            else if (rightvalue is int)
                return (double)leftvalue / (int)rightvalue;
            else
                return (double)leftvalue / (double)rightvalue;
        }
    }
}
