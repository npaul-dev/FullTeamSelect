using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTeamSelect
{
    using System.Windows;
    using NUnit.Framework;

    [TestFixture]
    public class FullTeamSelectViewModelTest
    {
        private FullTeamSelectViewModel _vm;

        [SetUp]
        public void SetUp()
        {
            _vm = new FullTeamSelectViewModel();
        }

        [Test]
        public void GivenGameStarted_ThenGameStateSet()
        {
            _vm.OnStartPressed(null);

            Assert.AreNotEqual(Character.NoCharacter, _vm.FirstPlayerCurrent);
            Assert.AreNotEqual(Character.NoCharacter, _vm.SecondPlayerCurrent);

            Assert.AreEqual(9, _vm.FirstPlayerRoster.Distinct().Count());
            Assert.AreEqual(9, _vm.SecondPlayerRoster.Distinct().Count());
            Assert.IsFalse(_vm.FirstPlayerRoster.Contains(Character.NoCharacter));
            Assert.IsFalse(_vm.SecondPlayerRoster.Contains(Character.NoCharacter));
            Assert.IsEmpty(_vm.FirstPlayerWinners);
            Assert.IsEmpty(_vm.FirstPlayerGraveyard);
            Assert.IsEmpty(_vm.SecondPlayerWinners);
            Assert.IsEmpty(_vm.SecondPlayerGraveyard);
        }

        [Test]
        public void WinnerButtonsDisabledWhenGameNotStarted()
        {
            _vm.OnFirstPlayerWin(null);
            _vm.OnSecondPlayerWin(null);

            Assert.AreEqual(Character.NoCharacter, _vm.FirstPlayerCurrent);
            Assert.AreEqual(Character.NoCharacter, _vm.SecondPlayerCurrent);
        }
    }
}
