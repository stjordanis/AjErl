﻿namespace AjErl.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Expressions;
    using AjErl.Language;

    [TestClass]
    public class ReceiveExpressionTests
    {
        [TestMethod]
        public void ReceiveMessage()
        {
            Process process = new Process();
            process.Tell(new Atom("ping"));
            Process.Current = process;

            MatchBody match = new MatchBody(new Atom("ping"), new ConstantExpression("pong"));
            ReceiveExpression expr = new ReceiveExpression(new MatchBody[] { match });

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.AreEqual("pong", result);
        }
    }
}