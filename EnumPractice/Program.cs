using System.ComponentModel;
using System.Reflection;

namespace EnumPractice
{
    /// <summary>
    /// 性別タイプ
    /// </summary>
    public enum Seibetsu
    {
        [Description("男性")]
        Male = 1,
        [Description("女性")]
        Female,
        [Description("その他")]
        Other
    }

    /// <summary>
    /// 専攻タイプ
    /// </summary>
    [Flags]
    public enum Major
    {
        None = 0b_0000,
        Math = 0b_0001,
        Physics = 0b_0010,
        Chemistry = 0b_0100,
        Biology = 0b_1000,
    }

    public static class EnumExtensions
    {
        /// <summary>
        /// Enumの説明文取得
        /// </summary>
        /// <param name="value">Enumの値</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descAttribute?.Description ?? value.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            UseEnumAsBitFlag();
        }

        #region Enum説明文の列挙
        /// <summary>
        /// Enum説明文の列挙
        /// </summary>
        public static void EnumerateEnumDesc()
        {
            foreach (var seibetsu in Enum.GetValues<Seibetsu>())
            {
                Console.WriteLine(seibetsu.GetDescription());
            }
        }
        #endregion

        #region 列挙型と数値の相互変換
        /// <summary>
        /// 列挙型と数値の相互変換
        /// </summary>
        public static void ConvertEnumAndInt()
        {
            Seibetsu seiEnum = (Seibetsu)2;
            Console.WriteLine(seiEnum);

            int seiInt = (int)seiEnum;
            Console.WriteLine(seiInt);
        }
        #endregion

        #region 列挙型と文字列の相互変換
        /// <summary>
        /// 列挙型と文字列の相互変換
        /// </summary>
        public static void ConvertEnumAndString()
        {
            var seiEnum = Seibetsu.Male;
            var name1 = Enum.GetName(typeof(Seibetsu), seiEnum);
            var name2 = seiEnum.ToString();
            Console.WriteLine(name1);
            Console.WriteLine(name2);


            if (Enum.TryParse<Seibetsu>(name1, out var result))
            {
                if (result == Seibetsu.Male)
                {
                    Console.WriteLine("文字列=>列挙型の変換成功");
                }
            }
        }
        #endregion

        #region 指定した値が列挙型に存在するかチェック
        /// <summary>
        /// 指定した値が列挙型に存在するかチェック
        /// </summary>
        public static void CheckValueInEnum()
        {
            var exists0 = Enum.IsDefined(typeof(Seibetsu), 2);
            var exists4 = Enum.IsDefined(typeof(Seibetsu), 4);
            var existsFemale = Enum.IsDefined(typeof(Seibetsu), "Female");
            Console.WriteLine(exists0);
            Console.WriteLine(exists4);
            Console.WriteLine(existsFemale);

        }
        #endregion

        #region 列挙型をビットフラグとして扱う
        /// <summary>
        /// 列挙型をビットフラグとして扱う
        /// </summary>
        public static void UseEnumAsBitFlag()
        {
            var student1 = Major.Physics | Major.Math;
            var student2 = Major.None;
            if ((student1 & Major.Physics) == Major.Physics)
            {
                Console.WriteLine("student1は、物理を専攻しています。");
            }

            if (student2 == Major.None)
            {
                Console.WriteLine("student2の専攻はありません");
            }

            if ((student1 & (Major.Physics | Major.Math)) == (Major.Physics | Major.Math))
            {
                Console.WriteLine("student1は、物理と数学を専攻しています。");
            }

        }
        #endregion
    }
}
