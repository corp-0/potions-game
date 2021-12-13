using System;
using System.Text;
using GodotXUnitApi;
using PotionsGame.Core.Utils;
using Xunit;

namespace Tests.Unitary.Subject
{
    public class TestSubject
    {
        [GodotFact]
        public void Subject_Should_Be_Generic_And_Work_With_More_Than_One_Type()
        {
            var subjectInt = new Subject<int>(0);
            Assert.Equal(0, subjectInt.Value);

            var subjectString = new Subject<string>(string.Empty);
            Assert.Equal(string.Empty, subjectString.Value);
            
            var subjectBool = new Subject<bool>(false);
            Assert.False(subjectBool.Value);
            
            var subjectFloat = new Subject<float>(0f);
            Assert.Equal(0f, subjectFloat.Value);
            
            var subjectDouble = new Subject<double>(0d);
            Assert.Equal(0d, subjectDouble.Value);
            
            var subjectLong = new Subject<long>(0L);
            Assert.Equal(0L, subjectLong.Value);
            
            var subjectDecimal = new Subject<decimal>(0m);
            Assert.Equal(0m, subjectDecimal.Value);
            
            var subjectChar = new Subject<char>('\0');
            Assert.Equal('\0', subjectChar.Value);
            
            var subjectByte = new Subject<byte>(0);
            Assert.Equal(0, subjectByte.Value);
            
            var subjectSByte = new Subject<sbyte>(0);
            Assert.Equal(0, subjectSByte.Value);
            
            var subjectShort = new Subject<short>(0);
            Assert.Equal(0, subjectShort.Value);
            
            var subjectUShort = new Subject<ushort>(0);
            Assert.Equal(0, subjectUShort.Value);
            
            var subjectIntPtr = new Subject<IntPtr>(IntPtr.Zero);
            Assert.Equal(IntPtr.Zero, subjectIntPtr.Value);
        }
        
        [GodotFact]
        public void Subject_Value_Should_Be_Different_From_Default_When_Changed()
        {
            var subject = new Subject<int>(0);
            subject.Value = 1;
            
            Assert.NotEqual(0, subject.Value);
        }
        
        [GodotFact]
        public void Subject_Should_Notify_Subscribers_When_Value_Changes()
        {
            var sb = new StringBuilder();
            var subject = new Subject<int>(0);

            void Onchange(int value) => sb.Append("changed");
            subject.Subscribe(Onchange);
            subject.Value = 1;
            Assert.Equal("changed", sb.ToString());
        }
        
        [GodotFact]
        public void Subject_Should_Notify_Changed_Value_To_Subscribers_When_Value_Changes()
        {
            var sb = new StringBuilder();
            var subject = new Subject<bool>(false);

            void Onchange(bool value) => sb.Append($"changed to {value}");
            subject.Subscribe(Onchange);
            subject.Value = true;
            Assert.Equal("changed to True", sb.ToString());
        }
        
        [GodotFact]
        public void Subject_Should_Notify_All_Subscribers_When_Value_Changes()
        {
            var sb = new StringBuilder();
            var subject = new Subject<int>(0);

            void Onchange(int value) => sb.Append("changed");
            void Onchange2(int value) => sb.Append("changed2");
            subject.Subscribe(Onchange);
            subject.Subscribe(Onchange2);
            subject.Value = 1;
            Assert.Equal("changedchanged2", sb.ToString());
        }
        
        [GodotFact]
        public void Subject_Should_Not_Notify_Subscribers_If_Value_Did_Not_Change()
        {
            var sb = new StringBuilder();
            var subject = new Subject<int>(0);

            void Onchange(int value) => sb.Append("changed");
            subject.Subscribe(Onchange);
            Assert.Equal(string.Empty, sb.ToString());
        }

        [GodotFact]
        public void Subject_Should_Notify_Subscribers_If_Value_Changed_Even_If_It_Is_The_Same_Value()
        {
            var sb = new StringBuilder();
            var subject = new Subject<int>(0);
            
            void Onchange(int value) => sb.Append("changed");
            subject.Subscribe(Onchange);
            subject.Value = 0;
            Assert.Equal("changed", sb.ToString());
        }
    }
}