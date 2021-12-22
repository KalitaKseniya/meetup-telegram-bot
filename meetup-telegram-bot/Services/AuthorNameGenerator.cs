namespace meetup_telegram_bot.Services
{
    public static class AuthorNameGenerator
    {
        private static List<string> nouns = new List<string>() 
        {
            "Джун",
            "Сеньор",
            "Стажер",
            "Баг",
            "Новичок",
            "Архитектор",
            "Девопс",
            "Энтузиаст",
            "Тестировщик",
            "Жавист",
            "Сын маминой подруги"
        };
        private static List<string> adjectives = new List<string>() 
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
            Random rnd = new Random();
            //var  = rnd.Next();
            return "s";

        }
    }
}
