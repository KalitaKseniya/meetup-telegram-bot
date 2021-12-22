namespace meetup_telegram_bot.Services
{
    public static class AuthorNameGenerator
    {
        private static readonly List<string> nouns = new() 
        {
            "джун",
            "сеньор",
            "стажер",
            "баг",
            "новичок",
            "архитектор",
            "девопс",
            "энтузиаст",
            "тестировщик",
            "жавист",
            "сын маминой подруги"
        };
        private static readonly List<string> adjectives = new() 
        { 
            "Прекрасный",
            "Изнемогающий",
            "Загадочный",
            "Безумный",
            "Жаждущий",
            "Затаившийся",
            "Богатый",
            "Задумчивый",
            "Радостный",
            "Взволнованный",
            "Мстительный",
        };

        public static string Generate()
        {
            Random random = new();

            return $"{adjectives[random.Next(adjectives.Count)]} {nouns[random.Next(nouns.Count)]}";
        }
    }
}
