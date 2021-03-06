﻿namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class DelayedCallExpression : IExpression
    {
        private IExpression nameexpression;
        private IList<IExpression> argumentexpressions;

        public DelayedCallExpression(IExpression nameexpression, IList<IExpression> argumentexpressions)
        {
            this.nameexpression = nameexpression;
            this.argumentexpressions = argumentexpressions;
        }

        public IExpression NameExpression { get { return this.nameexpression; } }

        public IList<IExpression> ArgumentExpressions { get { return this.argumentexpressions; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            object namevalue = this.nameexpression.Evaluate(context, withvars);

            IList<object> arguments = new List<object>();

            foreach (var argexpr in this.argumentexpressions)
                arguments.Add(argexpr.Evaluate(context, withvars));

            if (namevalue is Atom)
                namevalue = context.GetValue(string.Format("{0}/{1}", ((Atom)namevalue).Name, this.argumentexpressions.Count));

            IFunction func = (IFunction)namevalue;

            return new DelayedCall(func, context, arguments);
        }

        public bool HasVariable()
        {
            if (this.nameexpression.HasVariable())
                return true;

            foreach (var expr in this.argumentexpressions)
                if (expr.HasVariable())
                    return true;

            return false;
        }
    }
}
