namespace FullTeamSelect
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class FullTeamSelectViewModel : Bindable
    {
        private const int MAXCHARACTERS = 10;
        //private EventHandler _commandCanExecuteChangedHandler;
        private static readonly Random Random = new Random(42);

        public FullTeamSelectViewModel()
        {
            GameOver = true;
            GameStarted = false;
            ShowFirstPlayer = true;
            ShowSecondPlayer = true;

            FirstPlayerCurrent = Character.NoCharacter;
            FirstPlayerRoster = new ObservableCollection<Character>();
            FirstPlayerWinners = new ObservableCollection<Character>();
            FirstPlayerGraveyard = new ObservableCollection<Character>();
            SecondPlayerCurrent = Character.NoCharacter;
            SecondPlayerRoster = new ObservableCollection<Character>();
            SecondPlayerWinners = new ObservableCollection<Character>();
            SecondPlayerGraveyard = new ObservableCollection<Character>();

            StartCommand = new DelegateCommand(OnStartPressed, IsGameOver);
            FirstPlayerWinnerCommand = new DelegateCommand(OnFirstPlayerWin, IsGameStarted);
            SecondPlayerWinnerCommand = new DelegateCommand(OnSecondPlayerWin, IsGameStarted);

            //this.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged(nameof(FirstPlayerCurrent)));
        }

        private bool IsGameOver(object obj)
        {
            return true;
        }

        private bool IsGameStarted(object obj)
        {
            return true;
        }

        public Character FirstPlayerCurrent
        {
            get => Get<Character>();
            set => Set(value);
        }

        public Character SecondPlayerCurrent
        {
            get => Get<Character>();
            set => Set(value);
        }

        public ObservableCollection<Character> FirstPlayerRoster
        {
            get => Get<ObservableCollection<Character>>();
            set => Set(value);
        }

        public ObservableCollection<Character> FirstPlayerWinners
        {
            get => Get<ObservableCollection<Character>>();
            set => Set(value);
        }

        public ObservableCollection<Character> FirstPlayerGraveyard
        {
            get => Get<ObservableCollection<Character>>();
            set => Set(value);
        }

        public ObservableCollection<Character> SecondPlayerRoster
        {
            get => Get<ObservableCollection<Character>>();
            set => Set(value);
        }

        public ObservableCollection<Character> SecondPlayerWinners
        {
            get => Get<ObservableCollection<Character>>();
            set => Set(value);
        }

        public ObservableCollection<Character> SecondPlayerGraveyard
        {
            get => Get<ObservableCollection<Character>>();
            set => Set(value);
        }

        public bool GameOver
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool GameStarted
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool ShowFirstPlayer
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool ShowSecondPlayer
        {
            get => Get<bool>();
            set => Set(value);
        }

        public DelegateCommand StartCommand { get; }

        public DelegateCommand SecondPlayerWinnerCommand { get; }

        public DelegateCommand FirstPlayerWinnerCommand { get; }

        internal void OnStartPressed(object obj)
        {
            FirstPlayerRoster.Clear();
            FirstPlayerWinners.Clear();
            FirstPlayerGraveyard.Clear();
            SecondPlayerRoster.Clear();
            SecondPlayerWinners.Clear();
            SecondPlayerGraveyard.Clear();

            FirstPlayerRoster = DefaultCharacters();
            SecondPlayerRoster = DefaultCharacters();

            FirstPlayerCurrent = RemoveRandomCharacterFrom(FirstPlayerRoster);
            SecondPlayerCurrent = RemoveRandomCharacterFrom(SecondPlayerRoster);

            GameOver = false;
            GameStarted = !GameOver;
        }

        internal void OnFirstPlayerWin(object obj)
        {
            FirstPlayerWinners.Add(FirstPlayerCurrent);
            SecondPlayerGraveyard.Add(SecondPlayerCurrent);

            if (CheckForWinCondition())
            {
                FirstPlayerCurrent = Character.NoCharacter;
                return;
            }
            UpdateRoster();
        }

        internal void OnSecondPlayerWin(object obj)
        {
            FirstPlayerGraveyard.Add(FirstPlayerCurrent);
            SecondPlayerWinners.Add(SecondPlayerCurrent);

            if (CheckForWinCondition())
            {
                SecondPlayerCurrent = Character.NoCharacter;
                return;
            }
            UpdateRoster();
        }

        private bool CheckForWinCondition()
        {
            if (FirstPlayerWinners.Count == MAXCHARACTERS || SecondPlayerWinners.Count == MAXCHARACTERS)
            {
                GameOver = true;
                GameStarted = !GameOver;
            }

            return GameOver;
        }

        private void UpdateRoster()
        {
            if (FirstPlayerRoster.Count == 0)
            {
                foreach (var character in FirstPlayerGraveyard)
                {
                    FirstPlayerRoster.Add(character);
                }
                FirstPlayerGraveyard.Clear();
            }

            if (SecondPlayerRoster.Count == 0)
            {
                foreach (var character in SecondPlayerGraveyard)
                {
                    SecondPlayerRoster.Add(character);
                }
                SecondPlayerGraveyard.Clear();
            }

            if (FirstPlayerRoster.Any())
                FirstPlayerCurrent = RemoveRandomCharacterFrom(FirstPlayerRoster);
            if (SecondPlayerRoster.Any())
                SecondPlayerCurrent = RemoveRandomCharacterFrom(SecondPlayerRoster);
        }

        private Character RemoveRandomCharacterFrom(ObservableCollection<Character> characters)
        {
            var value = Random.Next(0, characters.Count);

            var character = characters.ElementAt(value);
            characters.RemoveAt(value);

            return character;
        }

        public static ObservableCollection<Character> DefaultCharacters()
        {
            return new ObservableCollection<Character>
            {
                Character.Grave,
                Character.Jaina,
                Character.Geiger,
                Character.Argagarg,
                Character.Setsuki,
                Character.Valerie,
                Character.Rook,
                Character.Midori,
                Character.Lum,
                Character.DeGrey
            };
        }
    }
}
