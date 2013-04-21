﻿namespace AjErl.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetAtom()
        {
            Lexer lexer = new Lexer("ok");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("ok", token.Value);
        }

        [TestMethod]
        public void GetAtomWithSurroundingSpaces()
        {
            Lexer lexer = new Lexer("  ok   ");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("ok", token.Value);
        }

        [TestMethod]
        public void GetVariable()
        {
            Lexer lexer = new Lexer("X");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.Type);
            Assert.AreEqual("X", token.Value);
        }

        [TestMethod]
        public void GetUnderscoreNameAsVariable()
        {
            Lexer lexer = new Lexer("_ok");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.Type);
            Assert.AreEqual("_ok", token.Value);
        }

        [TestMethod]
        public void GetEqualAsOperator()
        {
            Lexer lexer = new Lexer("=");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);
        }

        [TestMethod]
        public void GetPointAsOperator()
        {
            Lexer lexer = new Lexer(".");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(".", token.Value);
        }

        [TestMethod]
        public void GetSimpleMatch()
        {
            Lexer lexer = new Lexer("X=ok.");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Variable, token.Type);
            Assert.AreEqual("X", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Atom, token.Type);
            Assert.AreEqual("ok", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Separator, token.Type);
            Assert.AreEqual(".", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
