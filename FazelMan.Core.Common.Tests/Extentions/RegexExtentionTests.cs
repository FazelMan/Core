using System;
using System.Collections.Generic;
using System.Text;
using FazelMan.Core.Common.Enums;
using FazelMan.Core.Common.Extentions;
using FazelMan.Core.Tests;
using NUnit.Framework;

namespace FazelMan.Core.Common.Tests.Extentions
{
    [TestFixture]
    public class RegexExtentionTests
    {
        [Test]
        public void Must_true_phoneNumber_regex()
        {
            "09334443365".IsValidRegex(RegexType.IranPhoneNumber).ShouldBeTrue();
            "+989334443365".IsValidRegex(RegexType.IranPhoneNumber).ShouldBeTrue();
        }

        [Test]
        public void Must_true_email_regex()
        {
            "fazelman@ymail.com".IsValidRegex(RegexType.Email).ShouldBeTrue();
        }
    }
}
