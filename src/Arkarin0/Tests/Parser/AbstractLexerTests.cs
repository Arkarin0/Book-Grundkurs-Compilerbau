using Xunit;
using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;

namespace Arkarin0.CodeAnalysis.Tests
{
    public class AbstractLexerTests
    {
        private class AbstractLexer : Arkarin0.CodeAnalysis.AbstractLexer
        {
            public AbstractLexer() : base(SourceText.From(""))
            {
            }
            public AbstractLexer(SourceText text) : base(text)
            {
            }

            new public void Start()=> base.Start();

            new public void AddError(SyntaxDiagnosticInfo error)=> base.AddError(error);
            new public bool HasErrors=> base.HasErrors;
            new public SyntaxDiagnosticInfo[] GetErrors() => base.GetErrors();
        }


        [Fact()]
        public void StartResetsErrorsTest()
        {
            var obj= new AbstractLexer();

            obj.AddError(TestHelper.CreateSyntaxDiagnosticInfo());
            obj.Start();
            var errors= obj.GetErrors();

            Assert.Null(errors);
        }

        [Fact()]
        public void HasErrorsTest()
        {
            var obj = new AbstractLexer();
            Assert.False(obj.HasErrors);

            obj.AddError(TestHelper.CreateSyntaxDiagnosticInfo());
            Assert.True(obj.HasErrors);
        }

        [Fact()]
        public void AddAndGetErrorsTest()
        {
            var obj = new AbstractLexer();
            var error = TestHelper.CreateSyntaxDiagnosticInfo();
            
            
            Assert.Null(obj.GetErrors());

            obj.AddError(error);
            var errors= obj.GetErrors();

            Assert.NotNull(errors);
            Assert.NotEmpty(errors);
            Assert.Contains(error, errors);
        }

        [Fact()]
        public void GetErrors_ReturnsNull_WhenNoErrorsExistTest()
        {
            var obj = new AbstractLexer();
            Assert.Null(obj.GetErrors());            
        }

        [Fact()]
        public void AddError_DoNotAddWhenErrorIsNullTest()
        {
            var obj = new AbstractLexer();
            var error = TestHelper.CreateSyntaxDiagnosticInfo();
            Assert.Null(obj.GetErrors());

            obj.AddError(null);
            var errors = obj.GetErrors();

            Assert.Null(errors);            
        }


    }
}