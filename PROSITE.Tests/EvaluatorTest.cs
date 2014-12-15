using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PROSITE.Core;

namespace PROSITE.Tests
{
    [TestFixture]
    public class EvaluatorTest
    {
        private ExpressionTester _tester = new ExpressionTester();
        
        [Test]
        public void RepetitorExpr()
        {
            var expr = new CompiledExpression("A-x(0,1)Z");

            var matches = _tester.GetMatches(expr, "AHZ");
            Assert.That(matches.Count(), Is.GreaterThanOrEqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(0));            
            Assert.That(matches.First().Length, Is.EqualTo(3));

            matches = _tester.GetMatches(expr, "AZZ");
            Assert.That(matches.Count(), Is.GreaterThanOrEqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(0));
            Assert.That(matches.First().Length, Is.EqualTo(3));

            matches = _tester.GetMatches(expr, "AZG");
            Assert.That(matches.Count(), Is.GreaterThanOrEqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(0));
            Assert.That(matches.First().Length, Is.EqualTo(2));

            matches = _tester.GetMatches("Ax(0,1)ZG-", "AZZG");
            Assert.That(matches.Count(), Is.EqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(0));
            Assert.That(matches.First().Length, Is.EqualTo(4));

            matches = _tester.GetMatches("Ax(0,1)ZG-", "MAZG");
            Assert.That(matches.Count(), Is.EqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(1));
            Assert.That(matches.First().Length, Is.EqualTo(3));

            matches = _tester.GetMatches("A-{N}(1,2)Z", "MAHZG");
            Assert.That(matches.Count(), Is.EqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(1));
            Assert.That(matches.First().Length, Is.EqualTo(3));

            matches = _tester.GetMatches("A-{N}(1,2)Z", "MAHBZG");
            Assert.That(matches.Count(), Is.EqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(1));
            Assert.That(matches.First().Length, Is.EqualTo(4));

            matches = _tester.GetMatches("A-[HNZ](1,3)ZG", "HAHNZGB");
            Assert.That(matches.Count(), Is.EqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(1));
            Assert.That(matches.First().Length, Is.EqualTo(5));

            matches = _tester.GetMatches("A-[HNZ](1,3)ZG", "HANZGB");
            Assert.That(matches.Count(), Is.EqualTo(1));
            Assert.That(matches.First().Position, Is.EqualTo(1));
            Assert.That(matches.First().Length, Is.EqualTo(4));
        }
    }
}
