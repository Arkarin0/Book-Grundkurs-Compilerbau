using Xunit;
using Arkarin0.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkarin0.CodeAnalysis.Tests
{
    public class GreenNodeTests
    {
        public class GreenNode : CodeAnalysis.GreenNode
        {
            public GreenNode(ushort kind) : base(kind)
            {
            }

            public GreenNode(ushort kind, int fullWidth) : base(kind, fullWidth)
            {
            }

            public GreenNode(ushort kind, DiagnosticInfo[] diagnostics) : base(kind, diagnostics)
            {
            }

            public GreenNode(ushort kind, DiagnosticInfo[] diagnostics, int fullWidth) : base(kind, diagnostics, fullWidth)
            {
            }

            public override string Language => throw new NotImplementedException();

            public override string KindText => throw new NotImplementedException();

            public override CodeAnalysis.GreenNode SetDiagnostics(DiagnosticInfo[] diagnostics)
            {
                return new GreenNode((ushort)this.RawKind, diagnostics);
            }
        }

        private GreenNode CreateValidInstance(DiagnosticInfo[] diagnostics = null)
        {
            ushort kind = 0;

            if (diagnostics == null) return new GreenNode(kind);
            else return new GreenNode(kind, diagnostics);
        }

        [Fact()]
        public void CtorTest()
        {
            ushort kind = 10;
            var obj= new GreenNode(kind);
            Assert.Equal(kind, obj.RawKind);

            kind = 13;
            int length = 12;
            obj = new GreenNode(kind, length);
            Assert.Equal(kind, obj.RawKind);
            Assert.Equal(length, obj.FullWidth);
        }



        [Fact()]
        public void Ctor_WithDiagnosticInfoTest()
        {
            ushort kind = 10;
            var obj = new GreenNode(kind,null);
            Assert.Equal(kind, obj.RawKind);
            Assert.NotEqual(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            kind = 11;
            obj = new GreenNode(kind, new DiagnosticInfo[] {new DiagnosticInfo(10)});
            Assert.Equal(kind, obj.RawKind);
            Assert.Equal(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);



            kind = 13;
            int length = 12;
            obj = new GreenNode(kind, null,length);
            Assert.Equal(kind, obj.RawKind);
            Assert.Equal(length, obj.FullWidth);
            Assert.NotEqual(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);

            kind = 14;
            length = 13;
            obj = new GreenNode(kind, new DiagnosticInfo[] { new DiagnosticInfo(10) }, length);
            Assert.Equal(kind, obj.RawKind);
            Assert.Equal(length, obj.FullWidth);
            Assert.Equal(GreenNode.NodeFlags.ContainsDiagnostics, obj.Flags);
        }

        [Theory()]
        [InlineData(GreenNode.NodeFlags.IsNotMissing,GreenNode.NodeFlags.None)]
        [InlineData(GreenNode.NodeFlags.ContainsDiagnostics, GreenNode.NodeFlags.None)]
        [InlineData(GreenNode.NodeFlags.ContainsDiagnostics, GreenNode.NodeFlags.IsNotMissing)]
        public void SetFlagsTest(GreenNode.NodeFlags flags,GreenNode.NodeFlags init= CodeAnalysis.GreenNode.NodeFlags.None)
       {
            var obj = CreateValidInstance();
            obj.SetFlags(init);
            var expected = flags | init;

            obj.SetFlags(flags);

            Assert.Equal(expected, obj.Flags);
        }

        [Theory()]
        [InlineData(GreenNode.NodeFlags.IsNotMissing, GreenNode.NodeFlags.InheritMask)]
        [InlineData(GreenNode.NodeFlags.ContainsDiagnostics, GreenNode.NodeFlags.InheritMask)]
        public void ClearFlagsTest(GreenNode.NodeFlags flags, GreenNode.NodeFlags init = CodeAnalysis.GreenNode.NodeFlags.InheritMask)
        {
            var obj = CreateValidInstance();
            obj.SetFlags(init);
            var expected = init & ~flags;

            obj.ClearFlags(flags);

            Assert.Equal(expected, obj.Flags);
        }

        [Fact()]
        public void IsMissingTest()
        {
            var obj = CreateValidInstance();
            
            obj.SetFlags( CodeAnalysis.GreenNode.NodeFlags.IsNotMissing);
            Assert.False(obj.IsMissing);

            obj.ClearFlags(CodeAnalysis.GreenNode.NodeFlags.IsNotMissing);
            Assert.True(obj.IsMissing);
        }

        [Fact()]
        public void ContainsDiagnosticsTest()
        {
            var obj = CreateValidInstance();

            obj.SetFlags(CodeAnalysis.GreenNode.NodeFlags.ContainsDiagnostics);
            Assert.True(obj.ContainsDiagnostics);

            obj.ClearFlags(CodeAnalysis.GreenNode.NodeFlags.ContainsDiagnostics);
            Assert.False(obj.ContainsDiagnostics);
        }

        [Fact()]
        public void GetDiagnostics_IsEmptyArrayTest()
        {
            var obj = CreateValidInstance();

            var actual= obj.GetDiagnostics();

            Assert.Empty(actual);
        }

        [Fact()]
        public void GetDiagnostics_IsNotEmptyArrayTest()
        {
            var expected = new DiagnosticInfo[] { new DiagnosticInfo(10) };
            var obj = CreateValidInstance(diagnostics: expected);

            var actual = obj.GetDiagnostics();

            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void SetDiagnosticsTest()
        {
            var obj = new GreenNode(10);
            var diagnostic = new DiagnosticInfo(10);

            Assert.Empty(obj.GetDiagnostics());

            obj = (GreenNode)obj.SetDiagnostics(new[] { diagnostic });

            Assert.Contains(diagnostic, obj.GetDiagnostics());
        }

        [Fact()]
        public void AddError_WhenNoErrorsExistsTest()
        {
            var obj = new GreenNode(10);
            var diagnostic = new DiagnosticInfo(10);

            Assert.Empty(obj.GetDiagnostics());

            obj = (GreenNode)obj.AddError(diagnostic);

            Assert.Contains(diagnostic, obj.GetDiagnostics());
        }

        [Fact()]
        public void AddError_WhenOtherErrorsExistsTest()
        {
            var diagnostics = new[] {new  DiagnosticInfo(1), new DiagnosticInfo(2) };
            var obj = new GreenNode(10, diagnostics);
            var diagnostic = new DiagnosticInfo(10);

            Assert.Equal(diagnostics, obj.GetDiagnostics());

            obj = (GreenNode)obj.AddError(diagnostic);

            Assert.Contains(diagnostic, obj.GetDiagnostics());
        }

        [Fact()]
        public void GetValueTest()
        {
            var obj= CreateValidInstance();
            Assert.Null(obj.GetValue());
        }

        [Fact()]
        public void GetValueTextTest()
        {
            var obj = CreateValidInstance();
            Assert.Equal(string.Empty, obj.GetValueText());
        }
    }
}